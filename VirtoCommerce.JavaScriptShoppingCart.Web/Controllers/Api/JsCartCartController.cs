using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VirtoCommerce.JavaScriptShoppingCart.Core.Extensions;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Model.Marketing;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Services;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Controllers.Api
{
    using System.Linq;
    using System.Net.Http;

    using VirtoCommerce.Domain.Order.Model;
    using VirtoCommerce.JavaScriptShoppingCart.Crawling;
    using VirtoCommerce.JavaScriptShoppingCart.Web.Models.Requests;
    using VirtoCommerce.Platform.Core.Settings;

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


        [HttpPost()]
        [Route("items")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> AddItemToCart([FromUri]string currency, [FromUri]string cultureName, [FromUri]string cartId, [FromUri] AddCartItemRequest cartItem)
        {
            using (await AsyncLock.GetLockByKey(CacheKey.With(typeof(ShoppingCart), cartId)).LockAsync())
            {
                _cartManager.LoadCart(cartId, currency, cultureName);
                _cartManager.AddItem(cartItem.ProductId, cartItem.Quantity, cartItem.Price);
                _cartManager.Save();
                return Ok();
            }
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
        [ResponseType(typeof(CustomerOrder))]
        [Route("{cartId}/item/")]
        public async Task<IHttpActionResult> AddItemToCart(string cartId, string apiKey, [FromBody]LineItemRequest lineItem)
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
                var singleProduct = crawlingResult.CrawlingItems.Single(item => item.ProductId == lineItem.ProductId);

                ValidateField(lineItem.ListPrice, singleProduct.Price);
                ValidateField(lineItem.Quantity.ToString(), singleProduct.Quantity);
                ValidateField(lineItem.Sku, singleProduct.Sku);
            }

            using (var client = new HttpClient())
            using (var stream = await Request.Content.ReadAsStreamAsync())
            {
                // this version is not working: await client.PostAsync(urlWithKey, this.Request.Content)
                // says: https://www.google.com/search?q=Cannot+close+stream+until+all+bytes+are+written.&oq=Cannot+close+stream+until+all+bytes+are+written.&aqs=chrome..69i57j0l7.635j0j7&sourceid=chrome&ie=UTF-8
                // so we are using this:
                stream.Position = 0;
                var content = new StreamContent(stream);
                content.Headers.ContentType = Request.Content.Headers.ContentType;

                var forwardingUri = BuildForwardingUri(cartId, apiKey);

                var result = await client.PostAsync(forwardingUri, content);

                return StatusCode(result.StatusCode);
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
            const string ParameterName = "JavaScriptShoppingCart.CrawlingTargetUrl";
            var value = _settingManager.GetValue(ParameterName, string.Empty);

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(ParameterName);
            }

            return new Uri(value);
        }

        private Uri BuildForwardingUri(string cartId, string apiKey)
        {
            var requestUri = Request.RequestUri;
            var hardCodedPath = $"/api/carts/{cartId}/items";
            var uriBuilder = new UriBuilder(
                                 requestUri.Scheme,
                                 requestUri.Host,
                                 requestUri.Port,
                                 hardCodedPath)
            {
                Query = $"api_key={apiKey}"
            };
            return uriBuilder.Uri;
        }
    }
}
