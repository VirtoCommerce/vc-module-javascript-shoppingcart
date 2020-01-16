namespace VirtoCommerce.JavaScriptShoppingCart.TesingTool
{
    using System;
    using System.Threading.Tasks;

    using VirtoCommerce.JavaScriptShoppingCart.Crawling;
    using VirtoCommerce.JavaScriptShoppingCart.Crawling.Mapping;

    class Program
    {
        static async Task Main(string[] args)
        {
            var defaultCrawlingConfiguration = new DefaultCrawlingConfiguration(PredefinedMappings.DefaultMapping);
            var crawler = new DefaultCrawler(defaultCrawlingConfiguration);
            var result = await crawler.CrawlAsync(new Uri("http://localhost:44311/"));

            Console.ReadKey();
        }
    }
}
