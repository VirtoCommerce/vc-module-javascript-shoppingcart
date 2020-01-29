using System.Collections.Generic;
using System.Linq;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;
using Currency = VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common.Currency;
using Discount = VirtoCommerce.JavaScriptShoppingCart.Core.Model.Marketing.Discount;
using DomainCommerceModels = VirtoCommerce.Domain.Commerce.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Extensions
{
    public static class DiscountExtensions
    {
        public static Discount ToDiscount(this DomainCommerceModels.Discount discount, IEnumerable<Currency> currencies, Language language)
        {
            var defaultCurrency = currencies.FirstOrDefault(currency => currency.Equals(discount.Currency)) ?? new Currency(language, discount.Currency);

            var result = new Discount(defaultCurrency)
            {
                Coupon = discount.Coupon,
                Description = discount.Description,
                PromotionId = discount.PromotionId,
                Amount = new Money(discount.DiscountAmount, defaultCurrency),
            };

            return result;
        }
    }
}
