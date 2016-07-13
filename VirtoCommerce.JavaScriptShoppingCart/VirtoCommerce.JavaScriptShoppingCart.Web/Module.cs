using System;
using System.Linq;
using System.Web.Optimization;
using VirtoCommerce.ContentModule.Data.Services;
using VirtoCommerce.JavaScriptShoppingCart.Web.Budnles;
using VirtoCommerce.Platform.Core.Modularity;
using WebGrease.Css;

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
			//#region Demo

			//var bundle = new Bundle("~/javaScriptShoppingCart.js", new JsMinify());
			//bundle.IncludeDirectory("~/Modules/JavaScriptShoppingCart/Scripts/buyButton/", "*.js", true);
			//BundleTable.Bundles.Add(bundle);

			var cssBundle = new Bundle("~/checkout.css", new CssMinify());
			cssBundle.IncludeDirectory("~/Modules/JavaScriptShoppingCart/Scripts/buyButton/", "*.css", true);
			BundleTable.Bundles.Add(cssBundle);

			var partialBundle = new JavaScriptShoppingCartBundle("storefrontApp", "~/javaScriptShoppingCart.js")
				.IncludeDirectory("~/Modules/JavaScriptShoppingCart/Scripts/buyButton/", "*.js", true)
				.IncludeDirectory("~/Modules/JavaScriptShoppingCart/Scripts/buyButton/", "*.tpl.html", true);
			BundleTable.Bundles.Add(partialBundle);

			//#endregion

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
				var cssBundle = new Bundle($"~/{folderName}/checkout.css", new CssMinify());
				cssBundle.IncludeDirectory($"~/App_Data/cms-content/Themes/{folderName}/checkout/", "*.css", true);
				BundleTable.Bundles.Add(cssBundle);

				var bundle = new JavaScriptShoppingCartBundle("storefrontApp", $"~/{folderName}/javaScriptShoppingCart.js")
				.IncludeDirectory($"~/App_Data/cms-content/Themes/{folderName}/checkout/", "*.js", true)
				.IncludeDirectory($"~/App_Data/cms-content/Themes/{folderName}/checkout/", "*.tpl.html", true);

				//var bundle = new Bundle($"~/javaScriptShoppingCart/{folderName}.js", new JsMinify());
				//bundle.IncludeDirectory($"~/App_Data/cms-content/Themes/{folderName}/checkout/", "*.js", true);

				BundleTable.Bundles.Add(bundle);
			}
		}
	}
}