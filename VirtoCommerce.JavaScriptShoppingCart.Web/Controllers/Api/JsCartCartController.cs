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
using VirtoCommerce.Platform.Core.Exceptions;
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
        [Route("items")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> AddItemToCart([FromUri]string currency, [FromUri]string cultureName, [FromUri]string cartId, [FromUri] AddCartLineItemRequest lineItemRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var crawlingUri = BuildCrawlingUri();
            var crawlingResult = await _crawler.CrawlAsync(crawlingUri);

            if (!crawlingResult.IsSuccess)
            {
                return BadRequest(crawlingResult.Exception?.Message);
            }
            else
            {
                var singleProduct = crawlingResult.CrawlingItems.Single(item => item.ProductId == lineItemRequest.ProductId);

                ValidateField(lineItemRequest.ListPrice.ToString(), singleProduct.Price);
                ValidateField(lineItemRequest.Quantity.ToString(), singleProduct.Quantity);
                ValidateField(lineItemRequest.Sku, singleProduct.Sku);
            }

            using (await AsyncLock.GetLockByKey(CacheKey.With(typeof(ShoppingCart), cartId)).LockAsync())
            {
                _cartManager.LoadCart(cartId, currency, cultureName);
                _cartManager.AddItem(lineItemRequest.ProductId, lineItemRequest.Quantity, lineItemRequest.ListPrice, lineItemRequest.CatalogId, lineItemRequest.Sku, lineItemRequest.Name, lineItemRequest.ImageUrl);
                _cartManager.Save();
                return Ok();
            }
        }

        private static void ValidateField(string requested, string crawled)
        {
            if (requested != crawled)
            {
                throw new Exception("The request has been hacked");
            }
        }

        private Uri BuildCrawlingUri()
        {
            const string settingName = "JavaScriptShoppingCart.CrawlingTargetUrl";
            var value = _settingManager.GetValue(settingName, string.Empty);

            if (string.IsNullOrEmpty(value))
            {
                throw new PlatformException($"Setting {settingName} is not setted.");
            }

            return new Uri(value);
        }
    }
}
