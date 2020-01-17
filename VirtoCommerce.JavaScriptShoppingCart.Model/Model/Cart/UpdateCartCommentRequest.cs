using Newtonsoft.Json;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart
{

    public class UpdateCartCommentRequest
    {
        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}
