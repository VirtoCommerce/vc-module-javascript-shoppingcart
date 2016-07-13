using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using VirtoCommerce.ContentModule.Data.Services;
using VirtoCommerce.Domain.Cart.Model;
using VirtoCommerce.Domain.Cart.Services;
using VirtoCommerce.Domain.Commerce.Model;
using VirtoCommerce.Domain.Order.Services;
using VirtoCommerce.Domain.Pricing.Model;
using VirtoCommerce.Domain.Store.Model;
using VirtoCommerce.Domain.Store.Services;
using VirtoCommerce.JavaScriptShoppingCart.Web.Models;
using VirtoCommerce.JavaScriptShoppingCart.Web.Security;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Web.Security;
using SearchCriteria = VirtoCommerce.Domain.Cart.Model.SearchCriteria;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Controllers
{
	[RoutePrefix("api/javascriptshoppingcart")]
	public class JavaScriptShoppingCartController : ApiController
	{
		private readonly IShoppingCartService _shoppingCartService;
		private readonly IShoppingCartSearchService _shoppingCartSearchService;
		private readonly ICustomerOrderService _customerOrderService;
		private readonly IStoreService _storeService;
		private readonly Func<string, IContentBlobStorageProvider> _contentStorageProviderFactory;

		public JavaScriptShoppingCartController(IShoppingCartService shoppingCartService, IShoppingCartSearchService shoppingCartSearchService, ICustomerOrderService customerOrderService, IStoreService storeService, Func<string, IContentBlobStorageProvider> contentStorageProviderFactory)
		{
			_shoppingCartService = shoppingCartService;
			_shoppingCartSearchService = shoppingCartSearchService;
			_customerOrderService = customerOrderService;
			_storeService = storeService;
			_contentStorageProviderFactory = contentStorageProviderFactory;
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
				ImageUrl = "https://virtocommercedemo2.blob.core.windows.net/catalog/1432753636000_1148740.jpg",
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

			if (cart.Shipments == null || cart.Shipments.Count == 0)
			{
				cart.Shipments = new[]
				{
					new Shipment()
					{
						Id = "1",
						ShipmentMethodCode = "ShipmentMethodCode",
						DeliveryAddress = new Address()
						{
							AddressType = AddressType.Shipping,
							City = "New York",
							CountryCode = "USA",
							CountryName = "United States",
							Email = "email@email.com",
							FirstName = "Name",
							LastName = "LastName",
							RegionName = "New York",
							Line1 = "Address"
						}
					}
				};
			}

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

		[HttpGet]
		[ResponseType(typeof(string))]
		[Route("countries")]
		[CheckPermission(Permission = JavaScriptShoppingCartPredefinedPermissions.AddItemToCart)]
		public IHttpActionResult GetCountries()
		{
			var countries = new[]
			{
				new Country()
				{
					Name = "United States",
					Code2 = "US",
					Code3 = "USA",
					RegionType = "State",
					Regions = new[]
					{
						new CountryRegion()
						{
							Name = "Alabama",
							Code = "Al"
						},
						new CountryRegion()
						{
							Name = "Alaska",
							Code = "AK"
						}
					}
				},
				new Country()
				{
					Name = "Canada",
					Code2 = "CA",
					Code3 = "CAN"
				}
			};

			return Ok(countries);
		}
	}
}