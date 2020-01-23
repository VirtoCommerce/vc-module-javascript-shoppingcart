using System.ComponentModel.DataAnnotations;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Models.Requests
{
    public class ChangeCartLineItemQtyRequest
    {
        [Required]
        public string LineItemId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
