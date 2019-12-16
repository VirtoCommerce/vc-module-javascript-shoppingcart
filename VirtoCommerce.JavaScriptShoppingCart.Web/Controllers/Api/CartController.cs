using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VirtoCommerce.Domain.Cart.Model;
using VirtoCommerce.Domain.Cart.Services;
using VirtoCommerce.JavaScriptShoppingCart.Web.Services;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Controllers.Api
{
	[RoutePrefix("jscart/api/cart")]
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
		[Route("{cartId}/coupons/{couponCode}")]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> AddCartCoupon([FromUri]string cartId, [FromUri]string couponCode)
		{
			using (await AsyncLock.GetLockByKey(GetAsyncLockCartKey(cartId)).LockAsync())
			{
				var cart = _shoppingCartService.GetByIds(new[] { cartId }).FirstOrDefault();
				_cartBuilder.TakeCart(cart)
					.AddCoupon(couponCode)
					.Save();

				return Ok();
			}
		}

		[HttpDelete]
		[Route("{cartId}/coupons/{couponCode}")]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> RemoveCartCoupon([FromUri]string cartId, [FromUri]string couponCode)
		{
			using (await AsyncLock.GetLockByKey(GetAsyncLockCartKey(cartId)).LockAsync())
			{
				var cart = _shoppingCartService.GetByIds(new[] { cartId }).FirstOrDefault();
				_cartBuilder.TakeCart(cart)
					.RemoveCoupon(couponCode)
					.Save();
			}
			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: storefrontapi/cart/coupons/validate
		[HttpPost]
		[Route("{cartId}/coupons/validate")]
		[ResponseType(typeof(string))]
		public async Task<IHttpActionResult> ValidateCoupon([FromUri] string cartId, [FromUri]string coupon)
		{

			using (await AsyncLock.GetLockByKey(GetAsyncLockCartKey(cartId)).LockAsync())
			{
				var cart = _shoppingCartService.GetByIds(new[] { cartId }).FirstOrDefault();

				// TechDebt: make clone by implementing ICloneable on cart in Core module
				var serializedCart = Newtonsoft.Json.JsonConvert.SerializeObject(cart);
				var cartClone = Newtonsoft.Json.JsonConvert.DeserializeObject<ShoppingCart>(serializedCart);

				_cartBuilder.TakeCart(cartClone).AddCoupon(coupon).EvaluatePromotions();
				return Ok(coupon);
			}
		}

		private static string GetAsyncLockCartKey(string cartId)
		{
			return "Cart:" + cartId;
		}
	}
}
