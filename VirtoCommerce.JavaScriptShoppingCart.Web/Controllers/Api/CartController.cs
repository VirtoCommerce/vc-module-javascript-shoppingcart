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
	[RoutePrefix("jscart/api/cart/{currency}/{cultureName}")]
	[CLSCompliant(false)]
	public class CartController : ApiController
	{
		private readonly ICartBuilder _cartBuilder;

		public CartController(ICartBuilder cartBuilder)
		{
			_cartBuilder = cartBuilder;
		}

		[HttpGet]
		[Route("{storeId}/{customerId}/{cartName}/current")]
		[ResponseType(typeof(ShoppingCart))]
		public async Task<IHttpActionResult> GetCart(string currency, string cultureName, string storeId, string customerId, string cartName)
		{
			using (await AsyncLock.GetLockByKey(CacheKey.With(storeId, customerId, cartName, currency)).LockAsync())
			{
				_cartBuilder.LoadOrCreateNewTransientCart(cartName, storeId, customerId, cultureName, currency);
				return Ok(_cartBuilder.Cart);
			}
		}

		[HttpPost]
		[Route("{cartId}/coupons/{couponCode}")]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> AddCartCoupon([FromUri]string currency, [FromUri]string cultureName, [FromUri]string cartId, [FromUri]string couponCode)
		{
			using (await AsyncLock.GetLockByKey(CacheKey.With(typeof(ShoppingCart), cartId)).LockAsync())
			{
				_cartBuilder.LoadCart(cartId, currency, cultureName);
				_cartBuilder.AddCoupon(couponCode);
				_cartBuilder.Save();

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
				_cartBuilder.LoadCart(cartId, currency, cultureName);
				_cartBuilder.TakeCart(_cartBuilder.Cart.Clone() as ShoppingCart);
				_cartBuilder.Cart.Coupons = new[] { coupon };
				_cartBuilder.EvaluatePromotions();

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
				_cartBuilder.LoadCart(cartId, currency, cultureName);
				_cartBuilder.RemoveCoupon(couponCode);
				_cartBuilder.Save();
			}

			return Ok();
		}
	}
}
