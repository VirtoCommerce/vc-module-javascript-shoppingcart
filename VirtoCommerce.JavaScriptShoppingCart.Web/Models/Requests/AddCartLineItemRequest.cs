using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using VirtoCommerce.JavaScriptShoppingCart.Crawling;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Models.Requests
{
    [CLSCompliant(false)]
    public sealed class AddCartLineItemRequest : IEquatable<CrawlingItem>
    {
        [Required]
        public string CatalogId { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public decimal ListPrice { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Sku { get; set; }

        public bool Equals(CrawlingItem other)
        {
            var price = ListPrice.ToString(CultureInfo.InvariantCulture) == other?.Price;
            var quantity = Quantity.ToString() == other?.Quantity;
            var sku = Sku == other?.Sku;
            return quantity && price && sku;
        }
    }
}
