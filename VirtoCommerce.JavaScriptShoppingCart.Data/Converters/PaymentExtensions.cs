using System.Linq;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;
using VirtoCommerce.Platform.Core.Common;
using DomainCartModels = VirtoCommerce.Domain.Cart.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Converters
{
    public static class PaymentExtensions
    {
        public static Payment ToPayment(this DomainCartModels.Payment payment, ShoppingCart cart)
        {
            var result = new Payment(cart.Currency)
            {
                Id = payment.Id,
                OuterId = payment.OuterId,
                PaymentGatewayCode = payment.PaymentGatewayCode,
                TaxType = payment.TaxType,

                Amount = new Money(payment.Amount, cart.Currency),
            };

            if (payment.BillingAddress != null)
            {
                result.BillingAddress = payment.BillingAddress.ToAddress();
            }

            result.Price = new Money(payment.Price, cart.Currency);
            result.DiscountAmount = new Money(payment.DiscountAmount, cart.Currency);
            result.PriceWithTax = new Money(payment.PriceWithTax, cart.Currency);
            result.DiscountAmountWithTax = new Money(payment.DiscountAmountWithTax, cart.Currency);
            result.Total = new Money(payment.Total, cart.Currency);
            result.TotalWithTax = new Money(payment.TotalWithTax, cart.Currency);
            result.TaxTotal = new Money(payment.TaxTotal, cart.Currency);
            result.TaxPercentRate = (decimal?)payment.TaxPercentRate ?? 0m;

            if (payment.TaxDetails != null)
            {
                result.TaxDetails = payment.TaxDetails.Select(taxDetail => taxDetail.ToTaxDetail(cart.Currency)).ToList();
            }

            if (!payment.Discounts.IsNullOrEmpty())
            {
                result.Discounts.AddRange(payment.Discounts.Select(discount => discount.ToDiscount(new[] { cart.Currency }, cart.Language)));
            }

            return result;
        }
    }
}
