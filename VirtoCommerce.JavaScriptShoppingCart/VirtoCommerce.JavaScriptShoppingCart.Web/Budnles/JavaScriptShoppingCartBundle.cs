using System.Web.Optimization;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Budnles
{
	public class JavaScriptShoppingCartBundle : Bundle
	{
		public JavaScriptShoppingCartBundle(string moduleName, string virtualPath)
			: base(virtualPath, new JavaScriptShoppingCartTransform(moduleName))
		{
		}
	}
}