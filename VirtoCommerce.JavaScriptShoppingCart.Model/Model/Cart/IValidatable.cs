using System.Collections.Generic;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart
{
    public interface IValidatable
    {
        bool IsValid { get; set; }
        IList<ValidationError> ValidationErrors { get; }
    }
}
