using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VirtoCommerce.JavaScriptShoppingCart.Core.Extensions;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Model.Marketing;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Services;
using VirtoCommerce.JavaScriptShoppingCart.Crawling;
using VirtoCommerce.JavaScriptShoppingCart.Web.Models.Requests;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Settings;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Controllers.Api
{
    [AllowAnonymous]
    [RoutePrefix("jscart/api/carts/{currency}/{cultureName}")]
    [CLSCompliant(false)]
    public class JsCartCartController : ApiController
    {
        private readonly ICartManager _cartManager;

        private readonly ICrawler _crawler;

        private readonly ISettingsManager _settingManager;

        public JsCartCartController(ICartManager cartManager, ICrawler crawler, ISettingsManager settingManager)
        {
            _cartManager = cartManager;
            _crawler = crawler;
            _settingManager = settingManager;
        }

        [HttpGet]
        [Route("{storeId}/{customerId}/{cartName}/current")]
        [ResponseType(typeof(ShoppingCart))]
        public async Task<IHttpActionResult> GetCart(string currency, string cultureName, string storeId, string customerId, string cartName)
        {
            using (await AsyncLock.GetLockByKey(CacheKey.With(storeId, customerId, cartName, currency)).LockAsync())
            {
                _cartManager.LoadOrCreateNewTransientCart(cartName, storeId, customerId, cultureName, currency);
                _cartManager.Validate();
                return Ok(_cartManager.Cart);
            }
        }

        [HttpPut]
        [Route("{cartId}/comment")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateCartComment([FromUri]string currency, [FromUri]string cultureName, [FromUri]string cartId, [FromBody] UpdateCartCommentRequest commentRequest)
        {
            using (await AsyncLock.GetLockByKey(CacheKey.With(typeof(ShoppingCart), cartId)).LockAsync())
            {
                var comment = commentRequest?.Comment;
                _cartManager.LoadCart(cartId, currency, cultureName);

                _cartManager.UpdateCartComment(comment);
                _cartManager.Save();
            }

            return Ok();
        }

        [HttpGet]
        [Route("{cartId}/itemscount")]
        [ResponseType(typeof(int))]
        public int GetCartItemsCount([FromUri]string currency, [FromUri]string cultureName, [FromUri]string cartId)
        {
            _cartManager.LoadCart(cartId, currency, cultureName);
            return _cartManager.Cart.ItemsQuantity;
        }

        [HttpPost]
        [Route("{cartId}/coupons/{couponCode}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> AddCartCoupon([FromUri]string currency, [FromUri]string cultureName, [FromUri]string cartId, [FromUri]string couponCode)
        {
            using (await AsyncLock.GetLockByKey(CacheKey.With(typeof(ShoppingCart), cartId)).LockAsync())
            {
                _cartManager.LoadCart(cartId, currency, cultureName);
                _cartManager.AddCoupon(couponCode);
                _cartManager.Save();

                return Ok();
            }
        }

        [HttpPost]
        [Route("{cartId}/coupons/validate")]
        [ResponseType(typeof(Coupon))]
        public async Task<IHttpActionResult> ValidateCoupon([FromUri]string currency, [FromUri]string cultureName, [FromUri]string cartId, [FromBody]Coupon coupon)
        {
            using (await AsyncLock.GetLockByKey(CacheKey.With(typeof(ShoppingCart), cartId)).LockAsync())
            {
                _cartManager.LoadCart(cartId, currency, cultureName);
                _cartManager.TakeCart(_cartManager.Cart.Clone() as ShoppingCart);
                _cartManager.Cart.Coupons = new[] { coupon };
                _cartManager.EvaluatePromotions();

                return Ok(coupon);
            }
        }

        [HttpDelete]
        [Route("{cartId}/coupons/{couponCode?}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> RemoveCartCoupon([FromUri]string currency, [FromUri]string cultureName, [FromUri]string cartId, [FromUri]string couponCode = null)
        {
            using (await AsyncLock.GetLockByKey(CacheKey.With(typeof(ShoppingCart), cartId)).LockAsync())
            {
                _cartManager.LoadCart(cartId, currency, cultureName);
                _cartManager.RemoveCoupon(couponCode);
                _cartManager.Save();
            }

            return Ok();
        }

        [HttpPost]
        [Route("{cartId}/items")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> AddItemToCart([FromUri]string currency, [FromUri]string cultureName, [FromUri]string cartId, [FromBody] AddCartLineItemRequest lineItemRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Request.Headers.Referrer == null)
            {
                return BadRequest("Required header hasn't been provided");
            }

            if (!IsValidHost(Request.Headers.Referrer))
            {
                return BadRequest("Crawling URL is invalid");
            }

            var crawlingResult = await _crawler.CrawlAsync(Request.Headers.Referrer);

            if (!crawlingResult.IsSuccess)
            {
                return BadRequest(crawlingResult.Exception?.Message);
            }
            else
            {
                var crawlingItem = crawlingResult.CrawlingItems.Single(item => item.ProductId == lineItemRequest.ProductId);

                if (!lineItemRequest.Equals(crawlingItem))
                {
                    return BadRequest("The request has been hacked");
                }
            }

            using (await AsyncLock.GetLockByKey(CacheKey.With(typeof(ShoppingCart), cartId)).LockAsync())
            {
                _cartManager.LoadCart(cartId, currency, cultureName);
                _cartManager.AddItem(lineItemRequest.ProductId, lineItemRequest.Quantity, lineItemRequest.ListPrice, lineItemRequest.CatalogId, lineItemRequest.Sku, lineItemRequest.Name, lineItemRequest.ImageUrl);
                _cartManager.Save();
                return Ok();
            }
        }

        [HttpPut]
        [Route("{cartId}/items")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> ChangeCartItem([FromUri]string currency, [FromUri]string cultureName, [FromUri]string cartId, [FromBody] ChangeCartLineItemQtyRequest changeQty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (await AsyncLock.GetLockByKey(CacheKey.With(typeof(ShoppingCart), cartId)).LockAsync())
            {
                _cartManager.LoadCart(cartId, currency, cultureName);

                var lineItem = _cartManager.Cart.Items.FirstOrDefault(i => i.Id == changeQty.LineItemId);
                if (lineItem != null)
                {
                    _cartManager.ChangeItemQuantity(changeQty.LineItemId, changeQty.Quantity);
                    _cartManager.Save();
                }
            }

            return Ok();
        }


        [HttpDelete]
        [Route("{cartId}/items/{lineItemId?}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> RemoveCartItem([FromUri]string currency, [FromUri]string cultureName, [FromUri]string cartId, [FromUri]string lineItemId = null)
        {
            using (await AsyncLock.GetLockByKey(CacheKey.With(typeof(ShoppingCart), cartId)).LockAsync())
            {
                _cartManager.LoadCart(cartId, currency, cultureName);
                if (lineItemId.IsNullOrEmpty())
                {
                    _cartManager.Clear();
                }
                else
                {
                    _cartManager.RemoveItem(lineItemId);
                }

                _cartManager.Save();
                return Ok();
            }
        }

        [HttpGet]
        [Route("{cartId}/shippingmethods")]
        [ResponseType(typeof(ShippingMethod[]))]
        public ShippingMethod[] GetCartShipmentAvailShippingMethods([FromUri]string currency, [FromUri]string cultureName, [FromUri]string cartId)
        {
            _cartManager.LoadCart(cartId, currency, cultureName);

            var shippingMethods = _cartManager.GetAvailableShippingMethods();
            return shippingMethods.ToArray();
        }

        [HttpGet]
        [Route("{cartId}/paymentmethods")]
        [ResponseType(typeof(PaymentMethod[]))]
        public PaymentMethod[] GetCartAvailPaymentMethods([FromUri]string currency, [FromUri]string cultureName, [FromUri]string cartId)
        {
            _cartManager.LoadCart(cartId, currency, cultureName);

            var paymentMethods = _cartManager.GetAvailablePaymentMethods();
            return paymentMethods.ToArray();
        }

        [HttpPost]
        [Route("{cartId}/shipments")]
        public async Task<IHttpActionResult> AddOrUpdateCartShipment([FromUri]string currency, [FromUri]string cultureName, [FromUri]string cartId, [FromBody] Shipment shipment)
        {
            // Need lock to prevent concurrent access to same cart
            using (await AsyncLock.GetLockByKey(CacheKey.With(typeof(ShoppingCart), cartId)).LockAsync())
            {
                _cartManager.LoadCart(cartId, currency, cultureName);
                _cartManager.AddOrUpdateShipment(shipment);
                _cartManager.Save();
            }

            return Ok();
        }


        [HttpPost]
        [Route("{cartId}/payments")]
        public async Task<IHttpActionResult> AddOrUpdateCartPayment([FromUri]string currency, [FromUri]string cultureName, [FromUri]string cartId, [FromBody] Payment payment)
        {
            // Need lock to prevent concurrent access to same cart
            using (await AsyncLock.GetLockByKey(CacheKey.With(typeof(ShoppingCart), cartId)).LockAsync())
            {
                _cartManager.LoadCart(cartId, currency, cultureName);
                _cartManager.AddOrUpdatePayment(payment);
                _cartManager.Save();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{cartId}")]
        public async Task<IHttpActionResult> RemoveCart([FromUri]string currency, [FromUri]string cultureName, [FromUri]string cartId)
        {
            // Need lock to prevent concurrent access to same cart
            using (await AsyncLock.GetLockByKey(CacheKey.With(typeof(ShoppingCart), cartId)).LockAsync())
            {
                _cartManager.LoadCart(cartId, currency, cultureName);
                _cartManager.RemoveCart();
                _cartManager.Save();
            }

            return Ok();
        }

        private bool IsValidHost(Uri referrerUri)
        {
            const string ParameterName = "JavaScriptShoppingCart.CrawlingHostWhitelist";
            var flatCrawlingHostWhitelist = _settingManager.GetValue(ParameterName, string.Empty);

            if (string.IsNullOrEmpty(flatCrawlingHostWhitelist))
            {
                throw new ArgumentNullException(ParameterName);
            }

            var hosts = flatCrawlingHostWhitelist.Split(';', ',').Select(host => host.Trim());
            return hosts.Contains(referrerUri.Host);
        }
    }
}
