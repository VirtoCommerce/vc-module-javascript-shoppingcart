using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Models
{
	public class GetCartRequest
	{
		public string UserId { get; set; }

		public string CartId { get; set; }
	}
}