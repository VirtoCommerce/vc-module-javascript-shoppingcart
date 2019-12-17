using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart
{
	public abstract class ValidationError : CloneableValueObject
	{
		protected ValidationError()
		{
			ErrorCode = GetType().Name;
		}

		public string ErrorCode { get; private set; }
	}
}