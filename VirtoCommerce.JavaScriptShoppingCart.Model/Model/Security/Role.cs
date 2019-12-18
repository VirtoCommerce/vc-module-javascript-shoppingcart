using System.Collections.Generic;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Security
{
    public class Role : CloneableEntity
    {
        public string Name { get; set; }
        public IList<string> Permissions { get; set; } = new List<string>();
    }
}