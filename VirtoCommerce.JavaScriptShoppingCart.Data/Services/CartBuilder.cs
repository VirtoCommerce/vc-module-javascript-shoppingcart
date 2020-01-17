using System;
using System.Collections.Generic;
using System.Linq;
using VirtoCommerce.Domain.Cart.Services;
using VirtoCommerce.Domain.Customer.Model;
using VirtoCommerce.Domain.Customer.Services;
using VirtoCommerce.Domain.Store.Services;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Model.Marketing;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Services;
using VirtoCommerce.JavaScriptShoppingCart.Core.Services;
using VirtoCommerce.JavaScriptShoppingCart.Data.Converters;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Exceptions;
using domain = VirtoCommerce.Domain.Cart.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Services
{
    public class CartBuilder : ICartBuilder
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IShoppingCartSearchService _shoppingCartSearchService;
        private readonly IStoreService _storeService;
        private readonly IMemberService _memberService;
        private readonly IPromotionEvaluator _promotionEvaluator;

        public CartBuilder(IShoppingCartService shoppingCartService,
            IShoppingCartSearchService shoppingCartSearchService,
            IStoreService storeService,
            IMemberService memberService,
            IPromotionEvaluator promotoinEvaluator)
        {
            _shoppingCartService = shoppingCartService;
            _shoppingCartSearchService = shoppingCartSearchService;
            _storeService = storeService;
            _memberService = memberService;
            _promotionEvaluator = promotoinEvaluator;
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
            // EvaluateTaxes(); // TODO
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

        public void AddItem(string productId, int quantity, decimal price)
        {
            EnsureCartExists();

            var lineItem = new LineItem(Cart.Currency, Cart.Language) { ProductId = productId, Quantity = quantity, ListPrice = new Money(price, Cart.Currency) };

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
            throw new NotImplementedException();
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
            // Needs implementation

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


                //var evalContext = Cart.ToPromotionEvaluationContext();
                // await _promotionEvaluator.EvaluateDiscountsAsync(evalContext, new IDiscountable[] { Cart });

                // JsCart realization
                var evalContext = Cart.ToPromotionEvaluationContext();
                _promotionEvaluator.EvaluateDiscounts(evalContext, new[] { Cart });

            }
        }

        public void EvaluateTaxes()
        {
            // Needs implementation
        }

        public IEnumerable<PaymentMethod> GetAvailablePaymentMethods()
        {
            EnsureCartExists();

            var store = _storeService.GetById(Cart.StoreId);
            var result = store.PaymentMethods.Select(x => x.ToCartPaymentMethod(Cart));
            if (!result.IsNullOrEmpty())
            {
                //Evaluate promotions cart and apply rewards for available shipping methods
                var promoEvalContext = Cart.ToPromotionEvaluationContext();
                _promotionEvaluator.EvaluateDiscounts(promoEvalContext, result);

                //Evaluate taxes for available payments 
                //var workContext = _workContextAccessor.WorkContext;
                //var taxEvalContext = Cart.ToTaxEvalContext(workContext.CurrentStore);
                //taxEvalContext.Lines.Clear();
                //taxEvalContext.Lines.AddRange(result.SelectMany(x => x.ToTaxLines()));
                //await _taxEvaluator.EvaluateTaxesAsync(taxEvalContext, result);
            }
            return result;
        }

        public IEnumerable<ShippingMethod> GetAvailableShippingMethods()
        {
            throw new NotImplementedException();
        }

        public void MergeWithCart(ShoppingCart cart)
        {
            throw new NotImplementedException();
        }

        public void RemoveCart()
        {
            throw new NotImplementedException();
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
            // await EvaluateTaxesAsync();
            var cartDto = Cart.ToShopingCartDto();
            _shoppingCartService.SaveChanges(new[] { cartDto });
            //Evict cart from cache
            //CartCacheRegion.ExpireCart(Cart); // storefront
            LoadCart(Cart.Id, Cart.Currency.Code, Cart.Language.CultureName);
            // TakeCart(cart); //storefront
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
            throw new NotImplementedException();
        }

        protected virtual domain.ShoppingCartSearchCriteria CreateCartSearchCriteria(string cartName, string storeId, string userId, string currency)
        {
            var result = AbstractTypeFactory<domain.ShoppingCartSearchCriteria>.TryCreateInstance();

            result.CustomerId = userId;
            result.StoreId = storeId;
            result.Name = cartName;
            result.Currency = currency;

            return result;
        }

        protected virtual domain.ShoppingCart CreateCart(string cartName, string storeId, string userId, string languageCode, string currencyCode)
        {
            var result = AbstractTypeFactory<domain.ShoppingCart>.TryCreateInstance();
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

    }
}