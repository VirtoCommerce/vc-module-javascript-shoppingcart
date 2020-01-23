using System.Linq;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Tax;
using domain_tax_model = VirtoCommerce.Domain.Tax.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Converters
{
    public static class TaxConverter
    {
        public static TaxRate ToTaxRate(this domain_tax_model.TaxRate taxRateDto, Currency currency)
        {
            var result = new TaxRate(currency)
            {
                Rate = new Money(taxRateDto.Rate, currency),
                PercentRate = taxRateDto.PercentRate,
            };

            if (taxRateDto.Line != null)
            {
                result.Line = new TaxLine(currency)
                {
                    Code = taxRateDto.Line.Code,
                    Id = taxRateDto.Line.Id,
                    Name = taxRateDto.Line.Name,
                    Quantity = taxRateDto.Line.Quantity,
                    TaxType = taxRateDto.Line.TaxType,
                    TypeName = taxRateDto.Line.TypeName,

                    Amount = new Money(taxRateDto.Line.Amount, currency),
                    Price = new Money(taxRateDto.Line.Price, currency),
                };
                if (taxRateDto.TaxDetails != null)
                {
                    result.Line.TaxDetails = taxRateDto.TaxDetails.Select(x => x.ToTaxDetail(currency)).ToList();
                }
            }

            return result;
        }
    }
}
