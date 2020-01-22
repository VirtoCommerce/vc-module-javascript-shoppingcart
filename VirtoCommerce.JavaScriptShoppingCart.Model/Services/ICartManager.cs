using System.Collections.Generic;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Services
{
    /// <summary>
    /// Represent abstraction for working with customer shopping cart
    /// </summary>
    public interface ICartManager
    {
        ShoppingCart Cart { get; }

        /// <summary>
        ///  Capture cart and all next changes will be implemented on it
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        void TakeCart(ShoppingCart cart);

        /// <summary>
        /// Update shopping cart comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        void UpdateCartComment(string comment);

        /// <summary>
        /// Loads cart from the repository.
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="currencyCode"></param>
        /// <param name="languageCode"></param>
        /// <returns></returns>
        void LoadCart(string cartId, string currencyCode, string languageCode);

        /// <summary>
        /// Load or created new cart for specified parameters and capture it.  All next changes will be implemented on it
        /// </summary>
        /// <param name="cartName"></param>
        /// <param name="storeId"></param>
        /// <param name="userId"></param>
        /// <param name="languageCode"></param>
        /// <param name="currencyCode"></param>
        /// <returns></returns>
        void LoadOrCreateNewTransientCart(string cartName, string storeId, string userId, string languageCode, string currencyCode);

        /// <summary>
        /// Add product to cart
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <param name="listPrice"></param>
        /// <param name="catalogId"></param>
        /// <param name="sku"></param>
        /// <param name="name"></param>
        /// <param name="imageUrl"></param>
        void AddItem(string productId, int quantity, decimal listPrice, string catalogId, string sku, string name, string imageUrl);

        /// <summary>
        /// Change cart item qty by product index
        /// </summary>
        /// <param name="lineItemId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        void ChangeItemQuantity(string lineItemId, int quantity);

        /// <summary>
        /// Change cart item qty by item id
        /// </summary>
        /// <param name="lineItemIndex"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        void ChangeItemQuantity(int lineItemIndex, int quantity);

        void ChangeItemsQuantities(int[] quantities);

        /// <summary>
        /// Remove item from cart by id
        /// </summary>
        /// <param name="lineItemId"></param>
        /// <returns></returns>
        void RemoveItem(string lineItemId);

        /// <summary>
        /// Apply marketing coupon to captured cart
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        void AddCoupon(string couponCode);

        /// <summary>
        /// remove exist coupon from cart
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        void RemoveCoupon(string couponCode = null);

        /// <summary>
        /// Clear cart remove all items and shipments and payments
        /// </summary>
        /// <returns></returns>
        void Clear();

        /// <summary>
        /// Add or update shipment to cart
        /// </summary>
        /// <param name="shipment"></param>
        /// <returns></returns>
        void AddOrUpdateShipment(Shipment shipment);

        /// <summary>
        /// Remove exist shipment from cart
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <returns></returns>
        void RemoveShipment(string shipmentId);

        /// <summary>
        /// Add or update payment in cart
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        void AddOrUpdatePayment(Payment payment);

        /// <summary>
        /// Merge other cart with captured
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        void MergeWithCart(ShoppingCart cart);

        /// <summary>
        /// Remove cart from service
        /// </summary>
        /// <returns></returns>
        void RemoveCart();

        /// <summary>
        /// Returns all available shipment methods for current cart
        /// </summary>
        /// <returns></returns>
        IEnumerable<ShippingMethod> GetAvailableShippingMethods();

        /// <summary>
        /// Returns all available payment methods for current cart
        /// </summary>
        /// <returns></returns>
        IEnumerable<PaymentMethod> GetAvailablePaymentMethods();

        /// <summary>
        /// Evaluate marketing discounts for captured cart
        /// </summary>
        /// <returns></returns>
        void EvaluatePromotions();

        /// <summary>
        /// Evaluate taxes  for captured cart
        /// </summary>
        /// <returns></returns>
        void EvaluateTaxes();

        /// <summary>
        /// Validate cart.
        /// </summary>
        void Validate();

        /// <summary>
        /// Save cart.
        /// </summary>
        void Save();
    }
}
