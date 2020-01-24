using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Tax;
using DomainCommerceModels = VirtoCommerce.Domain.Commerce.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Converters
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
