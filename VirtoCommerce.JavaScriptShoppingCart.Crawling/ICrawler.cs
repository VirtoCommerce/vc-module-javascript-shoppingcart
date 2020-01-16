namespace VirtoCommerce.JavaScriptShoppingCart.Crawling
{
    using System;
    using System.Threading.Tasks;

    public interface ICrawler
    {
        Task<CrawlingResult> CrawlAsync(Uri uri);
    }
}
