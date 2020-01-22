namespace VirtoCommerce.JavaScriptShoppingCart.Web.Models.Requests
{
    using System.ComponentModel.DataAnnotations;

    public class LineItemRequest
    {
        [Required]
        public string CatalogId { get; set; }

        public string CurrencySymbol { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string ListPrice { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Sku { get; set; }
    }
}
