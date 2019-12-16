using Microsoft.Practices.Unity;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Optimization;
using VirtoCommerce.JavaScriptShoppingCart.Web.Bundles;
using VirtoCommerce.JavaScriptShoppingCart.Web.Services;
using VirtoCommerce.Platform.Core.Modularity;

namespace VirtoCommerce.JavaScriptShoppingCart.Web
{
	public class Module : ModuleBase
	{
		private readonly IUnityContainer _container;

		public Module(IUnityContainer container)
		{
			_container = container;
		}

		#region IModule Members

		public override void Initialize()
		{
			base.Initialize();

			_container.RegisterType<ICartBuilder, CartBuilder>();
		}

		public override void PostInitialize()
		{
			var moduleCatalog = _container.Resolve<IModuleCatalog>();
			var javaScriptShoppingCartModule = moduleCatalog.Modules.OfType<ManifestModuleInfo>().FirstOrDefault(x => x.ModuleName == "VirtoCommerce.JavaScriptShoppingCart");
			if (javaScriptShoppingCartModule != null)
			{
				var moduleRelativePath = "~/Modules" + javaScriptShoppingCartModule.FullPhysicalPath.Replace(HostingEnvironment.MapPath("~/Modules"), string.Empty).Replace("\\", "/");
				var cssBundle = new Bundle("~/styles/vc-shopping-cart", new CssMinify())
									.IncludeDirectory(Path.Combine(moduleRelativePath, "Content"), "*.css", true);
				BundleTable.Bundles.Add(cssBundle);

				var partialBundle = new AngularJavaScriptBundle("virtoCommerce.cartModule", "~/scripts/vc-shopping-cart")
					.IncludeDirectory(Path.Combine(moduleRelativePath, "Scripts/cart"), "*.js", true)
					.IncludeDirectory(Path.Combine(moduleRelativePath, "Scripts/checkout"), "*.js", true)
					.IncludeDirectory(Path.Combine(moduleRelativePath, "Scripts/checkout-modal"), "*.tpl.html", true)
					.IncludeDirectory(Path.Combine(moduleRelativePath, "Scripts/checkout"), "*.tpl.html", true)
					.IncludeDirectory(Path.Combine(moduleRelativePath, "Scripts/cart"), "*.tpl.html", true)
					.Include(Path.Combine(moduleRelativePath, "Scripts/services/cartService.js"));
				BundleTable.Bundles.Add(partialBundle);
			}
		}

		#endregion
	}
}