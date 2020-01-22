namespace VirtoCommerce.JavaScriptShoppingCart.Crawling
{
    using System;
    using System.Collections.Generic;

    public class CrawlingResult
    {
        public CrawlingResult(Exception exception)
        {
            IsSuccess = false;
            Exception = exception;
        }

        public CrawlingResult(bool isSuccess, IList<CrawlingItem> crawlingItems)
        {
            IsSuccess = isSuccess;
            CrawlingItems = crawlingItems;
        }

        public bool IsSuccess { get; }

        public Exception Exception { get; }

        public IList<CrawlingItem> CrawlingItems { get; }
    }
}
