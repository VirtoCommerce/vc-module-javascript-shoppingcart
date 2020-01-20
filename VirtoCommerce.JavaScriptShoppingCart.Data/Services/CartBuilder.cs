using System;
using System.Collections.Generic;
using System.Linq;
using VirtoCommerce.Domain.Cart.Services;
using VirtoCommerce.Domain.Customer.Model;
using VirtoCommerce.Domain.Customer.Services;
using VirtoCommerce.Domain.Store.Services;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Services;
using VirtoCommerce.JavaScriptShoppingCart.Data.Converters;
using VirtoCommerce.Platform.Core.Common;
using domain = VirtoCommerce.Domain.Cart.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Services
{
    public class CartBuilder : ICartBuilder
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IShoppingCartSearchService _shoppingCartSearchService;
        private readonly IStoreService _storeService;
        private readonly IMemberService _memberService;

        public CartBuilder(
            IShoppingCartService shoppingCartService,
            IShoppingCartSearchService shoppingCartSearchService,
            IStoreService storeService,
            IMemberService memberService)
        {
            _shoppingCartService = shoppingCartService;
            _shoppingCartSearchService = shoppingCartSearchService;
            _storeService = storeService;
            _memberService = memberService;
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
            throw new NotImplementedException();
        }

        public bool AddItem(string productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public void AddOrUpdatePayment(Payment payment)
        {
            throw new NotImplementedException();
        }

        public void AddOrUpdateShipment(Shipment shipment)
        {
            throw new NotImplementedException();
        }

        public void ChangeItemQuantity(string lineItemId, int quantity)
        {
            throw new NotImplementedException();
        }

        public void ChangeItemQuantity(int lineItemIndex, int quantity)
        {
            throw new NotImplementedException();
        }

        public void ChangeItemsQuantities(int[] quantities)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void EvaluatePromotions()
        {
            // Needs implementation
        }

        public void EvaluateTaxes()
        {
            // Needs implementation
        }

        public IEnumerable<PaymentMethod> GetAvailablePaymentMethods()
        {
            throw new NotImplementedException();
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

        public void RemoveCoupon(string couponCode = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(string lineItemId)
        {
            throw new NotImplementedException();
        }

        public void RemoveShipment(string shipmentId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void TakeCart(ShoppingCart cart)
        {
            Cart = cart ?? throw new ArgumentNullException(nameof(cart));
        }

        public void UpdateCartComment(string comment)
        {
            throw new NotImplementedException();
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

    }
}
