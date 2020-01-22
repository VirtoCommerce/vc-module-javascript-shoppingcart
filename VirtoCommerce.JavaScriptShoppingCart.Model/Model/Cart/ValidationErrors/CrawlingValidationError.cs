namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart.ValidationErrors
{
    public class CrawlingValidationError : ValidationError
    {
        public CrawlingValidationError(
            string lineItemPrice,
            string crawlingItemPrice,
            string lineItemQuantity,
            string crawlingItemQuantity,
            string lineItemSku,
            string crawlingItemSku)
        {
            LineItemPrice = lineItemPrice;
            CrawlingItemPrice = crawlingItemPrice;
            LineItemQuantity = lineItemQuantity;
            CrawlingItemQuantity = crawlingItemQuantity;
            LineItemSku = lineItemSku;
            CrawlingItemSku = crawlingItemSku;
        }

        public string LineItemPrice { get; private set; }

        public string CrawlingItemPrice { get; private set; }

        public string LineItemQuantity { get; private set; }

        public string CrawlingItemQuantity { get; private set; }

        public string LineItemSku { get; private set; }

        public string CrawlingItemSku { get; private set; }
    }
}
