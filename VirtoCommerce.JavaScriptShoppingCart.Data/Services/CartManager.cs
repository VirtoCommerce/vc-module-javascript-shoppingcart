using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VirtoCommerce.Domain.Cart.Services;
using VirtoCommerce.Domain.Customer.Model;
using VirtoCommerce.Domain.Customer.Services;
using VirtoCommerce.Domain.Store.Services;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart.ValidationErrors;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Model.Marketing;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Services;
using VirtoCommerce.JavaScriptShoppingCart.Core.Services;
using VirtoCommerce.JavaScriptShoppingCart.Data.Converters;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Exceptions;
using domain_cart_model = VirtoCommerce.Domain.Cart.Model;
using domain_shipping_model = VirtoCommerce.Domain.Shipping.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Services
{
    public class CartManager : ICartManager
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IShoppingCartSearchService _shoppingCartSearchService;
        private readonly IStoreService _storeService;
        private readonly IMemberService _memberService;
        private readonly IPromotionEvaluator _promotionEvaluator;
        private readonly ITaxEvaluator _taxEvaluator;

        public CartManager(IShoppingCartService shoppingCartService,
            IShoppingCartSearchService shoppingCartSearchService,
            IStoreService storeService,
            IMemberService memberService,
            IPromotionEvaluator promotoinEvaluator,
            ITaxEvaluator taxEvaluator)
        {
            _shoppingCartService = shoppingCartService;
            _shoppingCartSearchService = shoppingCartSearchService;
            _storeService = storeService;
            _memberService = memberService;
            _promotionEvaluator = promotoinEvaluator;
            _taxEvaluator = taxEvaluator;
        }

        public virtual ShoppingCart Cart { get; protected set; }

        public void LoadOrCreateNewTransientCart(string cartName, string storeId, string userId, string languageCode, string currencyCode)
        {
            // TechDebt: Need to add caching

            var criteria = CreateCartSearchCriteria(cartName, storeId, userId, currencyCode);

            var cartSearchResult = _shoppingCartSearchService.Search(criteria);
            var cart = cartSearchResult.Results.FirstOrDefault() ?? CreateCart(cartName, storeId, userId, languageCode, currencyCode);
            var language = string.IsNullOrEmpty(languageCode) ? Language.InvariantLanguage : new Language(languageCode);
            var currency = new Currency(language, currencyCode);

            TakeCart(cart.ToShoppingCart(currency, language));

            EvaluatePromotions();
            EvaluateTaxes();
        }

        public void LoadCart(string cartId, string currencyCode, string languageCode)
        {
            var language = string.IsNullOrEmpty(languageCode) ? Language.InvariantLanguage : new Language(languageCode);
            var currency = new Currency(language, currencyCode);

            var cart = _shoppingCartService.GetByIds(new[] { cartId }).FirstOrDefault();
            Cart = cart.ToShoppingCart(currency, language) ?? throw new ArgumentException($"Cart with id \"{cartId}\" does not exist.");
        }

        public void AddCoupon(string couponCode)
        {
            EnsureCartExists();

            if (!Cart.Coupons.Any(c => c.Code.EqualsInvariant(couponCode)))
            {
                Cart.Coupons.Add(new Coupon { Code = couponCode });
            }
        }

        public void RemoveCoupon(string couponCode = null)
        {
            EnsureCartExists();
            if (string.IsNullOrEmpty(couponCode))
            {
                Cart.Coupons.Clear();
            }
            else
            {
                Cart.Coupons.Remove(Cart.Coupons.FirstOrDefault(c => c.Code.EqualsInvariant(couponCode)));
            }
        }

        public void AddItem(string productId, int quantity, decimal listPrice, string catalogId = null, string sku = null, string name = null, string imageUrl = null)
        {
            EnsureCartExists();

            var lineItem = new LineItem(Cart.Currency, Cart.Language)
            {
                ProductId = productId,
                Quantity = quantity,
                ListPrice = new Money(listPrice, Cart.Currency),
                CatalogId = catalogId,
                Sku = sku,
                Name = name,
                ImageUrl = imageUrl,
            };

            var existingLineItem = Cart.Items.FirstOrDefault(li => li.ProductId == lineItem.ProductId);
            if (existingLineItem != null)
            {
                ChangeItemQuantity(existingLineItem, existingLineItem.Quantity + Math.Max(1, lineItem.Quantity));
            }
            else
            {
                lineItem.Id = null;
                Cart.Items.Add(lineItem);
            }
        }

        public void AddOrUpdatePayment(Payment payment)
        {
            EnsureCartExists();
            RemoveExistingPayment(payment);

            if (payment.BillingAddress != null)
            {
                //Reset address key because it can equal a customer address from profile and if not do that it may cause
                //address primary key duplication error for multiple carts with the same address 
                payment.BillingAddress.Key = null;
            }

            Cart.Payments.Add(payment);

            if (!string.IsNullOrEmpty(payment.PaymentGatewayCode) && !Cart.IsTransient())
            {
                var availablePaymentMethods = GetAvailablePaymentMethods();
                var paymentMethod = availablePaymentMethods.FirstOrDefault(pm => string.Equals(pm.Code, payment.PaymentGatewayCode, StringComparison.InvariantCultureIgnoreCase));
                if (paymentMethod == null)
                {
                    throw new PlatformException("Unknown payment method " + payment.PaymentGatewayCode);
                }
            }
        }

        public void AddOrUpdateShipment(Shipment shipment)
        {
            EnsureCartExists();
            RemoveExistingShipmentAsync(shipment);
            shipment.Currency = Cart.Currency;
            if (shipment.DeliveryAddress != null)
            {
                //Reset address key because it can equal a customer address from profile and if not do that it may cause
                //address primary key duplication error for multiple carts with the same address 
                shipment.DeliveryAddress.Key = null;
            }
            Cart.Shipments.Add(shipment);

            if (!string.IsNullOrEmpty(shipment.ShipmentMethodCode) && !Cart.IsTransient())
            {
                var availableShippingMethods = GetAvailableShippingMethods();
                var shippingMethod = availableShippingMethods.FirstOrDefault(sm => shipment.ShipmentMethodCode.EqualsInvariant(sm.ShipmentMethodCode) && shipment.ShipmentMethodOption.EqualsInvariant(sm.OptionName));
                if (shippingMethod == null)
                {
                    throw new PlatformException(string.Format(CultureInfo.InvariantCulture, "Unknown shipment method: {0} with option: {1}", shipment.ShipmentMethodCode, shipment.ShipmentMethodOption));
                }
                shipment.Price = shippingMethod.Price;
                shipment.DiscountAmount = shippingMethod.DiscountAmount;
                shipment.TaxType = shippingMethod.TaxType;
            }
        }

        public void ChangeItemQuantity(string lineItemId, int quantity)
        {
            EnsureCartExists();

            var lineItem = Cart.Items.FirstOrDefault(i => i.Id == lineItemId);
            if (lineItem != null)
            {
                ChangeItemQuantity(lineItem, quantity);
            }
        }

        public void ChangeItemQuantity(int lineItemIndex, int quantity)
        {
            EnsureCartExists();

            var lineItem = Cart.Items.ElementAt(lineItemIndex);
            if (lineItem != null)
            {
                ChangeItemQuantity(lineItem, quantity);
            }
        }

        public void ChangeItemsQuantities(int[] quantities)
        {
            EnsureCartExists();

            for (var i = 0; i < quantities.Length; i++)
            {
                var lineItem = Cart.Items.ElementAt(i);
                if (lineItem != null && quantities[i] > 0)
                {
                    ChangeItemQuantity(lineItem, quantities[i]);
                }
            }
        }

        public void Clear()
        {
            EnsureCartExists();
            Cart.Items.Clear();
        }

        public void EvaluatePromotions()
        {
            EnsureCartExists();

            var isReadOnlyLineItems = Cart.Items.Any(i => i.IsReadOnly);
            if (!isReadOnlyLineItems)
            {
                //Get product inventory to fill InStockQuantity parameter of LineItem
                //required for some promotions evaluation

                //foreach (var lineItem in Cart.Items.Where(x => x.Product != null).ToList())
                //{
                //    lineItem.InStockQuantity = (int)lineItem.Product.AvailableQuantity;
                //}

                var evalContext = Cart.ToPromotionEvaluationContext();
                _promotionEvaluator.EvaluateDiscounts(evalContext, new[] { Cart });

            }
        }

        public void EvaluateTaxes()
        {
            EnsureCartExists();
            _taxEvaluator.EvaluateTaxes(Cart.ToTaxEvaluationContextDto(), new[] { Cart });
        }

        public IEnumerable<PaymentMethod> GetAvailablePaymentMethods()
        {
            EnsureCartExists();

            var store = _storeService.GetById(Cart.StoreId);
            var paymentMethods = store.PaymentMethods.Select(x => x.ToCartPaymentMethod(Cart));

            if (!paymentMethods.IsNullOrEmpty())
            {
                //Evaluate promotions cart and apply rewards for available shipping methods
                var promoEvalContext = Cart.ToPromotionEvaluationContext();
                _promotionEvaluator.EvaluateDiscounts(promoEvalContext, paymentMethods);

                //Evaluate taxes for available payments                 
                var taxEvalContext = Cart.ToTaxEvaluationContextDto();
                taxEvalContext.Lines.Clear();
                taxEvalContext.Lines.AddRange(paymentMethods.SelectMany(x => x.ToTaxLines()));
                _taxEvaluator.EvaluateTaxes(taxEvalContext, paymentMethods);
            }

            return paymentMethods;
        }

        public IEnumerable<ShippingMethod> GetAvailableShippingMethods()
        {
            EnsureCartExists();

            //Request available shipping rates 
            var store = _storeService.GetById(Cart.StoreId);

            var result = Enumerable.Empty<ShippingMethod>();
            if (!Cart.IsTransient())
            {
                var shippingRates = GetAvailableShippingRates();
                result = shippingRates.Select(x => x.ToShippingMethod(Cart.Currency, store.Currencies.Select(currencyCode => new Currency(Cart.Language, currencyCode)))).OrderBy(x => x.Priority).ToList();
            }

            if (!result.IsNullOrEmpty())
            {
                //Evaluate promotions cart and apply rewards for available shipping methods
                var promoEvalContext = Cart.ToPromotionEvaluationContext();
                _promotionEvaluator.EvaluateDiscounts(promoEvalContext, result);

                //Evaluate taxes for available shipping rates
                var taxEvalContext = Cart.ToTaxEvaluationContextDto();
                taxEvalContext.Lines.Clear();
                taxEvalContext.Lines.AddRange(result.SelectMany(x => x.ToTaxLines()));
                _taxEvaluator.EvaluateTaxes(taxEvalContext, result);
            }

            return result;
        }

        public void MergeWithCart(ShoppingCart cart)
        {
            throw new NotImplementedException();
        }

        public void RemoveCart()
        {
            EnsureCartExists();
            _shoppingCartService.Delete(new[] { Cart.Id });
            Cart = null;
        }

        public void RemoveItem(string lineItemId)
        {
            EnsureCartExists();

            var lineItem = Cart.Items.FirstOrDefault(x => x.Id == lineItemId);
            if (lineItem != null)
            {
                Cart.Items.Remove(lineItem);
            }
        }

        public void RemoveShipment(string shipmentId)
        {
            EnsureCartExists();

            var shipment = Cart.Shipments.FirstOrDefault(s => s.Id == shipmentId);
            if (shipment != null)
            {
                Cart.Shipments.Remove(shipment);
            }
        }

        public void Save()
        {
            EnsureCartExists();
            EvaluatePromotions();
            EvaluateTaxes();
            var cartDto = Cart.ToShopingCartDto();
            _shoppingCartService.SaveChanges(new[] { cartDto });
            LoadCart(Cart.Id, Cart.Currency.Code, Cart.Language.CultureName);
        }

        public void TakeCart(ShoppingCart cart)
        {
            Cart = cart ?? throw new ArgumentNullException(nameof(cart));
        }

        public void UpdateCartComment(string comment)
        {
            EnsureCartExists();
            Cart.Comment = comment;
        }

        public void Validate()
        {
            EnsureCartExists();
            ValidateCartItems();
            ValidateCartShipments();
            Cart.IsValid = Cart.Items.All(x => x.IsValid) && Cart.Shipments.All(x => x.IsValid);
        }

        protected virtual void ValidateCartItems()
        {
            foreach (var lineItem in Cart.Items.ToList())
            {
                lineItem.ValidationErrors.Clear();
                // Code validation here if it needed
                lineItem.IsValid = !lineItem.ValidationErrors.Any();
            }
        }

        protected virtual void ValidateCartShipments()
        {
            foreach (var shipment in Cart.Shipments.ToArray())
            {
                shipment.ValidationErrors.Clear();

                var availShippingmethods = GetAvailableShippingMethods();
                var shipmentShippingMethod = availShippingmethods.FirstOrDefault(sm => shipment.HasSameMethod(sm));
                if (shipmentShippingMethod == null)
                {
                    shipment.ValidationErrors.Add(new UnavailableError());
                }
                else if (shipmentShippingMethod.Price != shipment.Price)
                {
                    shipment.ValidationErrors.Add(new PriceError(shipment.Price, shipment.PriceWithTax, shipmentShippingMethod.Price, shipmentShippingMethod.PriceWithTax));
                }
            }
        }

        protected virtual domain_cart_model.ShoppingCartSearchCriteria CreateCartSearchCriteria(string cartName, string storeId, string userId, string currency)
        {
            var result = AbstractTypeFactory<domain_cart_model.ShoppingCartSearchCriteria>.TryCreateInstance();

            result.CustomerId = userId;
            result.StoreId = storeId;
            result.Name = cartName;
            result.Currency = currency;

            return result;
        }

        protected virtual domain_cart_model.ShoppingCart CreateCart(string cartName, string storeId, string userId, string languageCode, string currencyCode)
        {
            var result = AbstractTypeFactory<domain_cart_model.ShoppingCart>.TryCreateInstance();
            var customerContact = _memberService.GetByIds(new[] { userId }).OfType<Contact>().FirstOrDefault();

            result.Name = cartName;
            result.LanguageCode = languageCode;
            result.Currency = currencyCode;
            result.CustomerId = userId;
            result.CustomerName = customerContact != null ? customerContact.FullName : "Anonymous";
            result.IsAnonymous = customerContact == null;
            result.StoreId = storeId;

            _shoppingCartService.SaveChanges(new[] { result });

            result = _shoppingCartService.GetByIds(new[] { result.Id }).FirstOrDefault();

            return result;
        }

        protected virtual void EnsureCartExists()
        {
            if (Cart == null)
            {
                throw new PlatformException("Cart not loaded.");
            }
        }

        protected virtual void AddLineItem(LineItem lineItem)
        {
            var existingLineItem = Cart.Items.FirstOrDefault(li => li.ProductId == lineItem.ProductId);
            if (existingLineItem != null)
            {
                ChangeItemQuantity(existingLineItem, existingLineItem.Quantity + Math.Max(1, lineItem.Quantity));
            }
            else
            {
                lineItem.Id = null;
                Cart.Items.Add(lineItem);
            }
        }

        protected virtual void ChangeItemQuantity(LineItem lineItem, int quantity)
        {
            if (lineItem != null && !lineItem.IsReadOnly)
            {
                if (quantity > 0)
                {
                    lineItem.Quantity = quantity;
                }
                else
                {
                    Cart.Items.Remove(lineItem);
                }
            }
        }

        protected virtual void RemoveExistingPayment(Payment payment)
        {
            if (payment != null)
            {
                var existingPayment = !payment.IsTransient() ? Cart.Payments.FirstOrDefault(s => s.Id == payment.Id) : null;
                if (existingPayment != null)
                {
                    Cart.Payments.Remove(existingPayment);
                }
            }
        }

        protected virtual void RemoveExistingShipmentAsync(Shipment shipment)
        {
            if (shipment != null)
            {
                var existShipment = !shipment.IsTransient() ? Cart.Shipments.FirstOrDefault(s => s.Id == shipment.Id) : null;
                if (existShipment != null)
                {
                    Cart.Shipments.Remove(existShipment);
                }
            }
        }

        protected virtual ICollection<domain_shipping_model.ShippingRate> GetAvailableShippingRates()
        {
            // logic was taken from CartModule.CartBuilder.GetAvailableShippingRates
            var cart = Cart.ToShopingCartDto();
            var shippingEvaluationContext = new domain_shipping_model.ShippingEvaluationContext(cart);
            var store = _storeService.GetById(Cart.StoreId);
            var activeAvailableShippingMethods = store.ShippingMethods.Where(x => x.IsActive).ToList();

            var availableShippingRates = activeAvailableShippingMethods
                .SelectMany(x => x.CalculateRates(shippingEvaluationContext))
                .Where(x => x.ShippingMethod == null || x.ShippingMethod.IsActive)
                .ToArray();

            return availableShippingRates;
        }

    }
}
