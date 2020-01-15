namespace VirtoCommerce.JavaScriptShoppingCart.TesingTool
{
    using System;
    using System.Threading.Tasks;

    using VirtoCommerce.JavaScriptShoppingCart.Crawling;

    class Program
    {
        static async Task Main(string[] args)
        {
            var defaultCrawlingConfiguration = new DefaultCrawlingConfiguration("vc-product-id");
            var crawler = new DefaultCrawler(defaultCrawlingConfiguration);
            var result = await crawler.CrawlAsync(new Uri("http://localhost:44311/"), "61a553f315b0478aa7eb6295dad6cbb5");

            Console.ReadKey();
        }
    }
}