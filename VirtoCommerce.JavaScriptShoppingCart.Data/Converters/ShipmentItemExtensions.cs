using System.Linq;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;
using DomainCartModels = VirtoCommerce.Domain.Cart.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Converters
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
