namespace VirtoCommerce.JavaScriptShoppingCart.Crawling
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Abot2.Crawler;
    using Abot2.Poco;

    using VirtoCommerce.JavaScriptShoppingCart.Crawling.Exceptions;
    using VirtoCommerce.JavaScriptShoppingCart.Crawling.Mapping;

    public class DefaultCrawler : ICrawler, IDisposable
    {
        private readonly PoliteWebCrawler _crawler;

        private readonly ICrawlingConfiguration _crawlingConfiguration;

        private CrawlingResult _crawlingResult;

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

        public async Task<CrawlingResult> CrawlAsync(Uri uri)
        {
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

        private string GetCrawlingAttributeName(CrawlingAttributeType type)
        {
            if (!_crawlingConfiguration.Mapping.TryGetValue(type, out var attributeName))
            {
                var exception = new MappingException($"Can't find the \"{type}\".");

                _crawlingResult = new CrawlingResult(exception);

                throw exception;
            }

            return attributeName;
        }

        private void PageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            var document = e.CrawledPage.AngleSharpHtmlDocument;

            var selectorAttrName = GetCrawlingAttributeName(CrawlingAttributeType.BuyButtonSelector);
            var productIdAttrName = GetCrawlingAttributeName(CrawlingAttributeType.ProductId);
            var priceAttrName = GetCrawlingAttributeName(CrawlingAttributeType.Price);
            var skuAttrName = GetCrawlingAttributeName(CrawlingAttributeType.Sku);
            var currencyAttrName = GetCrawlingAttributeName(CrawlingAttributeType.Currency);
            var quantityAttrName = GetCrawlingAttributeName(CrawlingAttributeType.Quantity);
            var catalogIdAttrName = GetCrawlingAttributeName(CrawlingAttributeType.CatalogId);
            var imageUrlAttrName = GetCrawlingAttributeName(CrawlingAttributeType.ImageUrl);
            var nameAttrName = GetCrawlingAttributeName(CrawlingAttributeType.Name);

            var query = document.All.Where(element => element.HasAttribute(selectorAttrName)).Select(
                element => new CrawlingItem(
                    element.GetAttribute(productIdAttrName),
                    element.GetAttribute(skuAttrName),
                    element.GetAttribute(priceAttrName),
                    element.GetAttribute(currencyAttrName),
                    element.GetAttribute(quantityAttrName),
                    element.GetAttribute(catalogIdAttrName),
                    element.GetAttribute(imageUrlAttrName),
                    element.GetAttribute(nameAttrName))).ToList();

            _crawlingResult = new CrawlingResult(isSuccess: true, crawlingItems: query);
        }

        private void PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
        {
            _crawlingResult = new CrawlingResult(new DisallowedException(e.DisallowedReason));
        }
    }
}
