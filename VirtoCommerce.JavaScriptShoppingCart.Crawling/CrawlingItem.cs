namespace VirtoCommerce.JavaScriptShoppingCart.Crawling
{
    public class CrawlingItem
    {
        public CrawlingItem(
            string productId,
            string sku,
            string price,
            string currency,
            string quantity,
            string catalogId,
            string imageUrl,
            string name)
        {
            ProductId = productId;
            Sku = sku;
            Price = price;
            Currency = currency;
            Quantity = quantity;
            CatalogId = catalogId;
            ImageUrl = imageUrl;
            Name = name;
        }

        public string CatalogId { get; }

        public string Currency { get; }

        public string ImageUrl { get; }

        public string Name { get; }

        public string Price { get; }

        public string ProductId { get; }

        public string Quantity { get; }

        public string Sku { get; }
    }
}
