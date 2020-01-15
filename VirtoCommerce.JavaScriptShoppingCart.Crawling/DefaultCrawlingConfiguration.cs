namespace VirtoCommerce.JavaScriptShoppingCart.Crawling
{
    public class DefaultCrawlingConfiguration : ICrawlingConfiguration
    {
        public DefaultCrawlingConfiguration(string productIdSelector)
        {
            MaxPagesToCrawl = 10;
            MinCrawlDelayPerDomainMilliSeconds = 3000;
            ProductIdSelector = productIdSelector;
        }

        public int MaxPagesToCrawl { get; set; }

        public int MinCrawlDelayPerDomainMilliSeconds { get; set; }

        public string ProductIdSelector { get; set; }
    }
}