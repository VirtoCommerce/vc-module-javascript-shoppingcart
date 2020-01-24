using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;
using Currency = VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common.Currency;
using DomainCommerceModels = VirtoCommerce.Domain.Commerce.Model;
using TaxDetail = VirtoCommerce.JavaScriptShoppingCart.Core.Model.Tax.TaxDetail;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Extensions
{
    public static class TaxDetailExtensions
    {
        public static TaxDetail ToTaxDetail(this DomainCommerceModels.TaxDetail taxDetail, Currency currency)
        {
            var result = new TaxDetail(currency)
            {
                Name = taxDetail.Name,
                Rate = new Money(taxDetail.Rate, currency),
                Amount = new Money(taxDetail.Amount, currency),
            };
            return result;
        }
    }
}
