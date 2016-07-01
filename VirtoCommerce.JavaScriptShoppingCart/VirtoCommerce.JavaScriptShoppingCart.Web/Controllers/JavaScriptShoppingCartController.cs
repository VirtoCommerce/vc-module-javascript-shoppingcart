using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using VirtoCommerce.Domain.Cart.Model;
using VirtoCommerce.Domain.Cart.Services;
using VirtoCommerce.Domain.Order.Services;
using VirtoCommerce.Domain.Pricing.Model;
using VirtoCommerce.JavaScriptShoppingCart.Web.Models;
using VirtoCommerce.JavaScriptShoppingCart.Web.Security;
using VirtoCommerce.Platform.Core.Web.Security;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Controllers
{
	[RoutePrefix("api/javascriptshoppingcart")]
	public class JavaScriptShoppingCartController : ApiController
	{
		private readonly IShoppingCartService _shoppingCartService;
		private readonly IShoppingCartSearchService _shoppingCartSearchService;
		private readonly ICustomerOrderService _customerOrderService;

		public JavaScriptShoppingCartController(IShoppingCartService shoppingCartService, IShoppingCartSearchService shoppingCartSearchService, ICustomerOrderService customerOrderService)
		{
			_shoppingCartService = shoppingCartService;
			_shoppingCartSearchService = shoppingCartSearchService;
			_customerOrderService = customerOrderService;
		}

		[HttpPost]
		[ResponseType(typeof(AddItemToCartResponse))]
		[Route("additemtocart")]
		[CheckPermission(Permission = JavaScriptShoppingCartPredefinedPermissions.AddItemToCart)]
		public IHttpActionResult AddItemToCart(AddItemToCartRequest request)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			ShoppingCart cart = null;

			var shoppingCartSearchResult = _shoppingCartSearchService.Search(new SearchCriteria()
			{
				StoreId = request.StoreId,
				CustomerId = request.UserId,
				Count = 1
			});

			if (shoppingCartSearchResult.TotalCount > 0)
			{
				cart = _shoppingCartService.GetById(shoppingCartSearchResult.ShopingCarts.First().Id);
			}

			if (cart == null)
			{
				cart = _shoppingCartService.Create(new ShoppingCart()
				{
					StoreId = request.StoreId,
					CustomerId = request.UserId,
					Currency = request.Currency
				});
			}

			cart.Items.Add(new LineItem()
			{
				CatalogId = request.CatalogId,
				ProductId = request.ProductId,
				Sku = request.ProductSku,
				Name = request.ProductName,
				Price = new Price()
				{
					Sale = request.Price,
					List = request.Price,
					Currency = request.Currency
				},
				Currency = request.Currency,
				Quantity = 1
			});

			_shoppingCartService.Update(new ShoppingCart[] { cart });

			var result = new AddItemToCartResponse()
			{
				Cart = cart
			};

			return Ok(result);
		}

		[HttpPost]
		[ResponseType(typeof(CreateOrderResponse))]
		[Route("createorder")]
		[CheckPermission(Permission = JavaScriptShoppingCartPredefinedPermissions.AddItemToCart)]
		public IHttpActionResult CreateOrder(CreateOrderRequest request)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			var order = _customerOrderService.CreateByShoppingCart(request.CartId);

			_shoppingCartService.Delete(new[] { request.CartId });

			var result = new CreateOrderResponse()
			{
				Order = order
			};

			return Ok(result);
		}

		//[HttpPost]
		//[ResponseType(typeof (GetCartResponse))]
		//[Route("cart")]
		//[CheckPermission(Permission = JavaScriptShoppingCartPredefinedPermissions.AddItemToCart)]
		//public IHttpActionResult GetCart(GetCartRequest request)
		//{
		//	ShoppingCart cart = null;
		//	if (!string.IsNullOrEmpty(request.CartId))
		//	{
		//		cart = _shoppingCartService.GetById(request.CartId);
		//	}

		//	var result = new GetCartResponse()
		//	{
		//		Template = "<div><h1>Cart</h1><div id=\"virto-javascript-shoppingcart-id\">Id: </div><ul id=\"virto-javascript-shoppingcart\"></ul></div>",
		//		Cart = cart
		//	};

		//	return Ok(result);
		//}
	}
}