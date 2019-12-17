using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart
{
	public partial class CartShipmentItem : CloneableEntity
	{
		public LineItem LineItem { get; set; }
		public int Quantity { get; set; }
		public override object Clone()
		{
			var result = base.Clone() as CartShipmentItem;

			result.LineItem = LineItem?.Clone() as LineItem;
			return result;
		}

	}
}
