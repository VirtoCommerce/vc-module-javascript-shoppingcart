namespace VirtoCommerce.JavaScriptShoppingCart.Crawling
{
    using System.Collections.Generic;

    using VirtoCommerce.JavaScriptShoppingCart.Crawling.Mapping;

    public class DefaultCrawlingConfiguration : ICrawlingConfiguration
    {
        public DefaultCrawlingConfiguration(Dictionary<CrawlingAttributeType, string> mapping)
        {
            MaxPagesToCrawl = 10;
            MinCrawlDelayPerDomainMilliSeconds = 3000;
            Mapping = mapping;
        }

        public int MaxPagesToCrawl { get; set; }

        public int MinCrawlDelayPerDomainMilliSeconds { get; set; }

        public Dictionary<CrawlingAttributeType, string> Mapping { get; set; }
    }
}
