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
	[RoutePrefix("jscart/api/cart/{storeId}/{customerId}/{cartName}/{currency}/{cultureName}")]
	public class CartController : ApiController
	{
		private readonly IShoppingCartService _shoppingCartService;
		private readonly ICartBuilder _cartBuilder;

		public CartController(IShoppingCartService shoppingCartService, ICartBuilder cartBuilder)
		{
			_shoppingCartService = shoppingCartService;
			_cartBuilder = cartBuilder;
		}

		[HttpPost]
		[Route("coupons/{couponCode}")]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> AddCartCoupon([FromUri]string storeId,
			[FromUri]string customerId,
			[FromUri]string cartId,
			[FromUri]string currency,
			[FromUri]string cultureName,
			[FromUri]string couponCode)
		{
			using (await AsyncLock.GetLockByKey(GetAsyncLockCartKey(storeId, customerId, cartId, currency)).LockAsync())
			{
				var cartBuilder = await LoadOrCreateCartAsync(storeId, customerId, cartId, currency, cultureName);

				await cartBuilder.AddCouponAsync(couponCode);

				await cartBuilder.SaveAsync();

				return Ok();
			}
		}

		// POST: storefrontapi/cart/coupons/validate
		[HttpPost]
		[Route("coupons/validate")]
		[ResponseType(typeof(string))]
		public async Task<IHttpActionResult> ValidateCoupon([FromUri]string storeId,
			[FromUri]string customerId,
			[FromUri]string cartId,
			[FromUri]string currency,
			[FromUri]string cultureName,
			[FromUri]string coupon)
		{
			using (await AsyncLock.GetLockByKey(GetAsyncLockCartKey(storeId, customerId, cartId, currency)).LockAsync())
			{
				// Non functional now: Need to get the cart from platform cart service, then convert to js cart model. Probably need to use automapper for conversation
				ShoppingCart cartModel = new ShoppingCart(new Core.Model.Common.Currency(new Core.Model.Common.Language("en-US"), "USD"), new Core.Model.Common.Language("en-US"));

				await _cartBuilder.TakeCartAsync(cartModel.Clone() as ShoppingCart);
				_cartBuilder.Cart.Coupons = new[]
				{
					new Coupon() { Code = coupon }
				};
				await _cartBuilder.EvaluatePromotionsAsync();

				return Ok(coupon);
			}
		}


		// DELETE: storefrontapi/cart/coupons
		[HttpDelete]
		[Route("coupons")]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> RemoveCartCoupon([FromUri]string storeId,
			[FromUri]string customerId,
			[FromUri]string cartId,
			[FromUri]string currency,
			[FromUri]string cultureName,
			[FromUri]string couponCode = null)
		{
			using (await AsyncLock.GetLockByKey(GetAsyncLockCartKey(storeId, customerId, cartId, currency)).LockAsync())
			{
				var cartBuilder = await LoadOrCreateCartAsync(storeId, customerId, cartId, currency, cultureName);
				await cartBuilder.RemoveCouponAsync(couponCode);
				await cartBuilder.SaveAsync();
			}

			return Ok();
		}

		private async Task<ICartBuilder> LoadOrCreateCartAsync(
			string storeId,
			string customerId,
			string cartId,
			string currency,
			string cultureName
			)
		{
			//Need to try load fresh cart from cache or service to prevent parallel update conflict
			//because WorkContext.CurrentCart may contains old cart
			await _cartBuilder.LoadOrCreateNewTransientCartAsync(cartId, storeId, customerId, cultureName, currency);
			return _cartBuilder;
		}

		private static string GetAsyncLockCartKey(string storeId, string customerId, string cartName, string currency) =>
			string.Join(":", storeId, customerId, cartName, currency);
	}
}
