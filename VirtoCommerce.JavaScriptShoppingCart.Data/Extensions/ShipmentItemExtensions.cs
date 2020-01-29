using System.Linq;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;
using DomainCartModels = VirtoCommerce.Domain.Cart.Model;
using ShoppingCart = VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart.ShoppingCart;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Extensions
{
    public static class ShipmentItemExtensions
    {
        public static CartShipmentItem ToShipmentItem(this DomainCartModels.ShipmentItem shipmentItem, ShoppingCart cart)
        {
            var result = new CartShipmentItem
            {
                Id = shipmentItem.Id,
                Quantity = shipmentItem.Quantity,
                LineItem = cart.Items.FirstOrDefault(lineItem => lineItem.Id == shipmentItem.LineItemId),
            };

            return result;
        }
    }
}
