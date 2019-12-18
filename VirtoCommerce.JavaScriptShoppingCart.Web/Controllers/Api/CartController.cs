using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VirtoCommerce.Domain.Cart.Services;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Model.Marketing;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Services;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Controllers.Api
{
	[RoutePrefix("jscart/api/cart")]
	[CLSCompliant(false)]
	public class CartController : ApiController
	{
		private readonly IShoppingCartService _shoppingCartService;
		private readonly ICartBuilder _cartBuilder;

		public CartController(IShoppingCartService shoppingCartService, ICartBuilder cartBuilder)
		{
			_shoppingCartService = shoppingCartService;
			_cartBuilder = cartBuilder;
		}

		[HttpGet]
		[Route("{storeId}/{customerId}/{cartName}/{currency}/{cultureName}/current")]
		[ResponseType(typeof(ShoppingCart))]
		public async Task<IHttpActionResult> GetCart(string storeId, string customerId, string cartName, string currency, string cultureName)
		{
			using (await AsyncLock.GetLockByKey(GetAsyncLockCartKey(string.Join(":", storeId, customerId, cartName, currency))).LockAsync())
			{
				await _cartBuilder.LoadOrCreateNewTransientCartAsync(cartName, storeId, customerId, cultureName, currency);
				return Ok(_cartBuilder.Cart);
			}
		}

		[HttpPost]
		[Route("{cartId}/coupons/{couponCode}")]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> AddCartCoupon([FromUri]string cartId, [FromUri]string couponCode)
		{
			using (await AsyncLock.GetLockByKey(GetAsyncLockCartKey(cartId)).LockAsync())
			{
				await _cartBuilder.LoadCartAsync(cartId);
				await _cartBuilder.AddCouponAsync(couponCode);
				await _cartBuilder.SaveAsync();

				return Ok();
			}
		}

		// POST: storefrontapi/cart/coupons/validate
		[HttpPost]
		[Route("{cartId}/coupons/validate")]
		[ResponseType(typeof(Coupon))]
		public async Task<IHttpActionResult> ValidateCoupon([FromUri]string cartId, [FromBody]Coupon coupon)
		{
			using (await AsyncLock.GetLockByKey(GetAsyncLockCartKey(cartId)).LockAsync())
			{
				await _cartBuilder.LoadCartAsync(cartId);
				await _cartBuilder.TakeCartAsync(_cartBuilder.Cart.Clone() as ShoppingCart);
				_cartBuilder.Cart.Coupons = new[] { coupon };
				await _cartBuilder.EvaluatePromotionsAsync();

				return Ok(coupon);
			}
		}


		// DELETE: storefrontapi/cart/coupons
		[HttpDelete]
		[Route("{cartId}/coupons/{couponCode?}")]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> RemoveCartCoupon([FromUri]string cartId, [FromUri]string couponCode = null)
		{
			using (await AsyncLock.GetLockByKey(GetAsyncLockCartKey(cartId)).LockAsync())
			{
				await _cartBuilder.LoadCartAsync(cartId);
				await _cartBuilder.RemoveCouponAsync(couponCode);
				await _cartBuilder.SaveAsync();
			}

			return Ok();
		}

		private static string GetAsyncLockCartKey(string cartId) => "Cart: " + cartId;
	}
}
