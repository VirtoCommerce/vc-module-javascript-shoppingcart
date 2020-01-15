namespace VirtoCommerce.JavaScriptShoppingCart.Crawling
{
    public interface ICrawlingConfiguration
    {
        int MaxPagesToCrawl { get; set; }

        int MinCrawlDelayPerDomainMilliSeconds { get; set; }

        string ProductIdSelector { get; set; }
    }
}