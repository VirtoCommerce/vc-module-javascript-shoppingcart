using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtoCommerce.Domain.Order.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Models
{
	public class CreateOrderResponse
	{
		public CustomerOrder Order { get; set; }
	}
}