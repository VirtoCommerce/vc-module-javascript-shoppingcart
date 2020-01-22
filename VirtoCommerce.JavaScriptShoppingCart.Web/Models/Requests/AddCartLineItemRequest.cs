using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Models.Requests
{
    public class AddCartLineItemRequest
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
    }
}
