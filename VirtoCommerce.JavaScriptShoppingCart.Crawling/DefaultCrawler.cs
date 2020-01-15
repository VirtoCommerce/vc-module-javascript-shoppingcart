namespace VirtoCommerce.JavaScriptShoppingCart.Crawling
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Abot2.Crawler;
    using Abot2.Poco;

    public class DefaultCrawler : ICrawler, IDisposable
    {
        private const string PriceAttributeName = "vc-price";

        private readonly PoliteWebCrawler _crawler;

        private readonly ICrawlingConfiguration _crawlingConfiguration;

        private CrawlingResult _crawlingResult;

        private string _productId;

        public DefaultCrawler(ICrawlingConfiguration crawlingConfiguration)
        {
            _crawlingConfiguration = crawlingConfiguration;

            var crawlConfiguration = new CrawlConfiguration
            {
                MaxPagesToCrawl = _crawlingConfiguration.MaxPagesToCrawl,
                MinCrawlDelayPerDomainMilliSeconds = _crawlingConfiguration
                                                 .MinCrawlDelayPerDomainMilliSeconds
            };

            _crawler = new PoliteWebCrawler(crawlConfiguration);
            _crawler.PageCrawlCompleted += PageCrawlCompleted;
            _crawler.PageCrawlDisallowed += PageCrawlDisallowed;
        }

        public async Task<CrawlingResult> CrawlAsync(Uri uri, string productId)
        {
            _productId = productId;
            var crawlResult = await _crawler.CrawlAsync(uri);

            if (crawlResult.ErrorOccurred)
            {
                return new CrawlingResult(crawlResult.ErrorException);
            }

            if (_crawlingResult == null)
            {
                throw new UnknownException();
            }

            return _crawlingResult;
        }

        public void Dispose()
        {
            _crawler?.Dispose();
        }

        private void PageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            var document = e.CrawledPage.AngleSharpHtmlDocument;

            var query = document.All.Where(t => t.HasAttribute(_crawlingConfiguration.ProductIdSelector))
                .Select(t => t.GetAttribute(PriceAttributeName));
            var price = query.FirstOrDefault();

            _crawlingResult = new CrawlingResult(_productId, price);
        }

        private void PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
        {
            _crawlingResult = new CrawlingResult(new DisallowedException(e.DisallowedReason));
        }
    }
}