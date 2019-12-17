namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common
{
	public interface IConvertible<T>
	{
		T ConvertTo(Currency currency);
	}
}