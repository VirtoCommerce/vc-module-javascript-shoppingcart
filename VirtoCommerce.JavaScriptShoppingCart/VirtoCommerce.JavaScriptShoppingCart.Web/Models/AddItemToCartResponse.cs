using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtoCommerce.Domain.Cart.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Models
{
	public class AddItemToCartResponse
	{
		public ShoppingCart Cart { get; set; }
	}
}