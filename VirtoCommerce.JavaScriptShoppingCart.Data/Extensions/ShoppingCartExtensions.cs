using System.Linq;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Marketing;
using DomainCartModels = VirtoCommerce.Domain.Cart.Model;
using ShoppingCart = VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart.ShoppingCart;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Extensions
{
    // TechDebt: Need to use Automapper here where possible.
    // Current problem - pass constructor parameters to the child object.
    // Link to mitigate the problem using resolution context: http://codebuckets.com/2016/09/24/passing-parameters-with-automapper/
    public static class ShoppingCartExtensions
    {
        public static ShoppingCart ToShoppingCart(this DomainCartModels.ShoppingCart cart, Currency currency, Language language)
        {
            var result = new ShoppingCart(currency, language)
            {
                ChannelId = cart.ChannelId,
                Comment = cart.Comment,
                CustomerId = cart.CustomerId,
                CustomerName = cart.CustomerName,
                Id = cart.Id,
                Name = cart.Name,
                OrganizationId = cart.OrganizationId,
                Status = cart.Status,
                StoreId = cart.StoreId,
                Type = cart.Type,
                HasPhysicalProducts = true,
            };

            if (cart.Coupons != null)
            {
                result.Coupons = cart.Coupons.Select(coupon => new Coupon { Code = coupon, AppliedSuccessfully = !string.IsNullOrEmpty(coupon) }).ToList();
            }

            if (cart.Payments != null)
            {
                result.Payments = cart.Payments.Select(payment => payment.ToPayment(result)).ToList();
            }

            if (cart.Shipments != null)
            {
                result.Shipments = cart.Shipments.Select(shipment => shipment.ToShipment(result)).ToList();
            }

            if (cart.TaxDetails != null)
            {
                result.TaxDetails = cart.TaxDetails.Select(taxDetail => taxDetail.ToTaxDetail(currency)).ToList();
            }

            result.DiscountAmount = new Money(cart.DiscountAmount, currency);
            result.HandlingTotal = new Money(cart.HandlingTotal, currency);
            result.HandlingTotalWithTax = new Money(cart.HandlingTotalWithTax, currency);

            result.Total = new Money(cart.Total, currency);
            result.SubTotal = new Money(cart.SubTotal, currency);
            result.SubTotalWithTax = new Money(cart.SubTotalWithTax, currency);
            result.ShippingPrice = new Money(cart.ShippingSubTotal, currency);
            result.ShippingPriceWithTax = new Money(cart.ShippingSubTotalWithTax, currency);
            result.ShippingTotal = new Money(cart.ShippingTotal, currency);
            result.ShippingTotalWithTax = new Money(cart.ShippingTotalWithTax, currency);
            result.PaymentPrice = new Money(cart.PaymentSubTotal, currency);
            result.PaymentPriceWithTax = new Money(cart.PaymentSubTotalWithTax, currency);
            result.PaymentTotal = new Money(cart.PaymentTotal, currency);
            result.PaymentTotalWithTax = new Money(cart.PaymentTotalWithTax, currency);

            result.DiscountTotal = new Money(cart.DiscountTotal, currency);
            result.DiscountTotalWithTax = new Money(cart.DiscountTotalWithTax, currency);
            result.TaxTotal = new Money(cart.TaxTotal, currency);

            result.IsAnonymous = cart.IsAnonymous;
            result.IsRecuring = cart.IsRecuring == true;
            result.VolumetricWeight = cart.VolumetricWeight ?? 0;
            result.Weight = cart.Weight ?? 0;

            return result;
        }
    }
}
