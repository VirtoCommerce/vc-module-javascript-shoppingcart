using System;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common
{
	[Flags]
	public enum AddressType
	{
		Billing = 1,
		Shipping = 2,
		BillingAndShipping = Billing | Shipping
	}
}
