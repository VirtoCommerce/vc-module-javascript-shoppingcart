using Newtonsoft.Json;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart
{
    public class AddCartItemRequest
    {

        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

    }
}
