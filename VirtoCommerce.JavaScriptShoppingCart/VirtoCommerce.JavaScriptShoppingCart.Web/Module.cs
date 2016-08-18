using System;
using System.Linq;
using System.Web.Optimization;
using VirtoCommerce.ContentModule.Data.Services;
using VirtoCommerce.JavaScriptShoppingCart.Web.Budnles;
using VirtoCommerce.Platform.Core.Modularity;

namespace VirtoCommerce.JavaScriptShoppingCart.Web
{
	public class Module : ModuleBase
	{
		private readonly Func<string, IContentBlobStorageProvider> _contentStorageProviderFactory;

		public Module(Func<string, IContentBlobStorageProvider> contentStorageProviderFactory)
		{
			_contentStorageProviderFactory = contentStorageProviderFactory;
		}

		public override void SetupDatabase()
		{
		}

		public override void Initialize()
		{

		}

		public override void PostInitialize()
		{
			var cssBundle = new Bundle("~/checkout.css", new CssMinify())
				//todo: solve the problem with loading of fonts in bootstrap
				//.Include("~/Modules/JavaScriptShoppingCart/Scripts/libraries/bootstrap-3.3.6.min.css")
				.IncludeDirectory("~/Modules/JavaScriptShoppingCart/Scripts/buyButton/", "*.css", true);
			BundleTable.Bundles.Add(cssBundle);

			var partialBundle = new JavaScriptShoppingCartBundle("storefrontApp", "~/javaScriptShoppingCart.js")
				.Include("~/Modules/JavaScriptShoppingCart/Scripts/libraries/angularjs-1.5.7.min.js")
				.Include("~/Modules/JavaScriptShoppingCart/Scripts/libraries/angular-animate-1.5.7.min.js")
				.Include("~/Modules/JavaScriptShoppingCart/Scripts/libraries/angular-cookies-1.5.7.min.js")
				.Include("~/Modules/JavaScriptShoppingCart/Scripts/libraries/angular-filter-0.5.8.js")
				.Include("~/Modules/JavaScriptShoppingCart/Scripts/libraries/ui-bootstrap-tpls-1.3.3.js")
				.Include("~/Modules/JavaScriptShoppingCart/Scripts/libraries/underscore-min-1.8.3.js")
				.Include("~/Modules/JavaScriptShoppingCart/Scripts/libraries/angular-credit-cards-3.0.1.js")
				.IncludeDirectory("~/Modules/JavaScriptShoppingCart/Scripts/buyButton/", "*.js", true)
				.IncludeDirectory("~/Modules/JavaScriptShoppingCart/Scripts/buyButton/", "*.tpl.html", true);
			BundleTable.Bundles.Add(partialBundle);

			var storageProvider = _contentStorageProviderFactory("Themes/");

			var blobSearchResult = storageProvider.Search("", null);
			foreach (var folder in blobSearchResult.Folders)
			{
				if (folder.Name == "default")
				{
					CreateCheckoutBundle(storageProvider, "default");
				}
				else
				{
					var storeThemes = storageProvider.Search(folder.Name, null);
					foreach (var storeThemeFolder in storeThemes.Folders)
					{
						CreateCheckoutBundle(storageProvider, $"{folder.Name}/{storeThemeFolder.Name}");
					}
				}
			}
		}

		private void CreateCheckoutBundle(IContentBlobStorageProvider storageProvider, string folderName)
		{
			var themeFolders = storageProvider.Search(folderName, null);
			var checkoutThemeFolder = themeFolders.Folders.FirstOrDefault(f => f.Name == "checkout");
			if (checkoutThemeFolder != null)
			{
				var cssBundle = new Bundle($"~/{folderName}/checkout.css", new CssMinify())
					//.Include("~/Modules/JavaScriptShoppingCart/Scripts/libraries/bootstrap-3.3.6.min.css")
					.IncludeDirectory($"~/App_Data/cms-content/Themes/{folderName}/checkout/", "*.css", true);
				BundleTable.Bundles.Add(cssBundle);

				var bundle = new JavaScriptShoppingCartBundle("storefrontApp", $"~/{folderName}/javaScriptShoppingCart.js")
					.Include("~/Modules/JavaScriptShoppingCart/Scripts/libraries/angularjs-1.5.7.min.js")
					.Include("~/Modules/JavaScriptShoppingCart/Scripts/libraries/angular-animate-1.5.7.min.js")
					.Include("~/Modules/JavaScriptShoppingCart/Scripts/libraries/angular-cookies-1.5.7.min.js")
					.Include("~/Modules/JavaScriptShoppingCart/Scripts/libraries/angular-filter-0.5.8.js")
					.Include("~/Modules/JavaScriptShoppingCart/Scripts/libraries/ui-bootstrap-tpls-1.3.3.js")
					.Include("~/Modules/JavaScriptShoppingCart/Scripts/libraries/underscore-min-1.8.3.js")
					.Include("~/Modules/JavaScriptShoppingCart/Scripts/libraries/angular-credit-cards-3.0.1.js")
					.IncludeDirectory($"~/App_Data/cms-content/Themes/{folderName}/checkout/", "*.js", true)
					.IncludeDirectory($"~/App_Data/cms-content/Themes/{folderName}/checkout/", "*.tpl.html", true);

				BundleTable.Bundles.Add(bundle);
			}
		}
	}
}