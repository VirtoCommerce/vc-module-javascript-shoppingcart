using System.Collections.Generic;
using PlatformSecurity = VirtoCommerce.Platform.Core.Security;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Security
{
    public static class SecurityConstants
    {
        public static readonly PlatformSecurity.Role JsShoppingCartUser = new PlatformSecurity.Role
        {
            Id = "js-shopping-cart-user",
            Name = "JS Shopping cart user",
            Description = "This role allow to work with carts and create orders"
        };

        public static class Permissions
        {
            public const string CanReadCart = "cart:read";
            public const string CanCreateCart = "cart:create";
            public const string CanUpdateCart = "cart:update";
            public const string CanDeleteCart = "cart:delete";
            public const string CanCreateOrder = "order:create";
            public const string CanReadOrder = "order:read";
            public const string CanReadOrderPrices = "order:read_prices";
            public const string CanUpdateOrder = "order:update";

            public static readonly IEnumerable<string> AllPermissions = new[] { CanCreateCart, CanReadCart, CanUpdateCart, CanDeleteCart, CanCreateOrder, CanReadOrder, CanReadOrderPrices, CanUpdateOrder };
        }
    }
}