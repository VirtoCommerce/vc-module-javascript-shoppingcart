namespace VirtoCommerce.JavaScriptShoppingCart.Crawling
{
    using System;

    public class CrawlingResult
    {
        public CrawlingResult(Exception exception)
        {
            IsSuccess = false;
            Exception = exception;
        }

        public CrawlingResult(string productId, string price)
        {
            IsSuccess = true;
            ProductId = productId;
            Price = price;
        }

        public string Price { get; }

        public string ProductId { get; }

        public bool IsSuccess { get; }

        public Exception Exception { get; }
    }
}