using System.Linq;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;
using VirtoCommerce.Platform.Core.Common;
using DomainCartModels = VirtoCommerce.Domain.Cart.Model;
using Shipment = VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart.Shipment;
using ShoppingCart = VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart.ShoppingCart;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Extensions
{
    public static class ShipmentExtensions
    {
        public static Shipment ToShipment(this DomainCartModels.Shipment shipment, ShoppingCart cart)
        {
            var result = new Shipment(cart.Currency)
            {
                Id = shipment.Id,
                MeasureUnit = shipment.MeasureUnit,
                ShipmentMethodCode = shipment.ShipmentMethodCode,
                ShipmentMethodOption = shipment.ShipmentMethodOption,
                WeightUnit = shipment.WeightUnit,
                Height = (double?)shipment.Height,
                Weight = (double?)shipment.Weight,
                Width = (double?)shipment.Width,
                Length = (double?)shipment.Length,
                Currency = cart.Currency,
                Price = new Money(shipment.Price, cart.Currency),
                PriceWithTax = new Money(shipment.PriceWithTax, cart.Currency),
                DiscountAmount = new Money(shipment.DiscountAmount, cart.Currency),
                Total = new Money(shipment.Total, cart.Currency),
                TotalWithTax = new Money(shipment.TotalWithTax, cart.Currency),
                DiscountAmountWithTax = new Money(shipment.DiscountAmountWithTax, cart.Currency),
                TaxTotal = new Money(shipment.TaxTotal, cart.Currency),
                TaxPercentRate = (decimal?)shipment.TaxPercentRate ?? 0m,
            };

            if (shipment.DeliveryAddress != null)
            {
                result.DeliveryAddress = shipment.DeliveryAddress.ToAddress();
            }

            if (shipment.Items != null)
            {
                result.Items = shipment.Items.Select(shipmentItem => shipmentItem.ToShipmentItem(cart)).ToList();
            }

            if (shipment.TaxDetails != null)
            {
                result.TaxDetails = shipment.TaxDetails.Select(taxDetail => taxDetail.ToTaxDetail(cart.Currency)).ToList();
            }

            if (!shipment.Discounts.IsNullOrEmpty())
            {
                result.Discounts.AddRange(shipment.Discounts.Select(discount => discount.ToDiscount(new[] { cart.Currency }, cart.Language)));
            }

            return result;
        }
    }
}
