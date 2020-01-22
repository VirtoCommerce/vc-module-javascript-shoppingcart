namespace VirtoCommerce.JavaScriptShoppingCart.Crawling.Mapping
{
    using System.Collections.Generic;

    public class PredefinedMappings
    {
        public static string DefaultSelector => "vc-buy-button";

        public static Dictionary<CrawlingAttributeType, string> DefaultMapping
        {
            get =>
                new Dictionary<CrawlingAttributeType, string>
                {
                    {
                        CrawlingAttributeType.BuyButtonSelector, DefaultSelector
                    },
                    {
                        CrawlingAttributeType.Price, "vc-list-price"
                    },
                    {
                        CrawlingAttributeType.Sku, "vc-sku"
                    },
                    {
                        CrawlingAttributeType.ProductId, "vc-product-id"
                    },
                    {
                        CrawlingAttributeType.Name, "vc-item-name"
                    },
                    {
                        CrawlingAttributeType.ImageUrl, "vc-image-url"
                    },
                    {
                        CrawlingAttributeType.CatalogId, "vc-catalog-id"
                    },
                    {
                        CrawlingAttributeType.Quantity, "vc-quantity"
                    },
                    {
                        CrawlingAttributeType.Currency, "vc-currency"
                    }
                };
        }

    }
}
