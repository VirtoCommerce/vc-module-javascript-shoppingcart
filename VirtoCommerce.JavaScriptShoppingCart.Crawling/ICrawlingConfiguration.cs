namespace VirtoCommerce.JavaScriptShoppingCart.Crawling
{
    using System.Collections.Generic;

    using VirtoCommerce.JavaScriptShoppingCart.Crawling.Mapping;

    public interface ICrawlingConfiguration
    {
        int MaxPagesToCrawl { get; set; }

        int MinCrawlDelayPerDomainMilliSeconds { get; set; }

        Dictionary<CrawlingAttributeType, string> Mapping { get; set; }
    }
}
