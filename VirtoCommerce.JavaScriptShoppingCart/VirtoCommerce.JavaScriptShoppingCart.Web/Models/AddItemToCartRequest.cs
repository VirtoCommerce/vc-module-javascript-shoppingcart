using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Models
{
	public class AddItemToCartRequest
	{
		public string UserId { get; set; }

		public string StoreId { get; set; }

		public string CatalogId { get; set; }

		public string ProductId { get; set; }

		public string ProductName { get; set; }

		public string ProductSku { get; set; }

		public decimal Price { get; set; }

		public string Currency { get; set; }
	}
}