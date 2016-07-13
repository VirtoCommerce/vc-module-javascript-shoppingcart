using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtoCommerce.Domain.Cart.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Models
{
	public class GetCartTemplateResponse
	{
		public string Template { get; set; }

		public ShoppingCart Cart { get; set; }
	}
}