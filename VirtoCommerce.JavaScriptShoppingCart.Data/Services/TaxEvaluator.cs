using System;
using System.Collections.Generic;
using System.Linq;
using VirtoCommerce.Domain.Commerce.Services;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Tax;
using VirtoCommerce.JavaScriptShoppingCart.Core.Services;
using VirtoCommerce.JavaScriptShoppingCart.Data.Converters;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Settings;
using domain_tax_model = VirtoCommerce.Domain.Tax.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Services
{
    public class TaxEvaluator : ITaxEvaluator
    {
        private readonly ICommerceService _commerceService;

        public TaxEvaluator(ICommerceService commerceApi)
        {
            _commerceService = commerceApi;
        }

        public virtual void EvaluateTaxes(domain_tax_model.TaxEvaluationContext context, IEnumerable<ITaxable> owners)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (owners == null)
            {
                throw new ArgumentNullException(nameof(owners));
            }

            IList<domain_tax_model.TaxRate> taxRates = new List<domain_tax_model.TaxRate>();

            var taxCalculationEnabled = context.Store.Settings.GetSettingValue("Stores.TaxCalculationEnabled", true);
            if (taxCalculationEnabled)
            {
                var fixedTaxRate = GetFixedTaxRate(context.Store.TaxProviders.ToList());

                // Do not execute platform API for tax evaluation if fixed tax rate is used
                if (fixedTaxRate != 0)
                {
                    foreach (var line in context.Lines ?? Enumerable.Empty<domain_tax_model.TaxLine>())
                    {
                        var rate = new domain_tax_model.TaxRate()
                        {
                            Rate = line.Amount * fixedTaxRate * 0.01m,
                            Currency = context.Currency,
                            Line = line,
                        };
                        taxRates.Add(rate);
                    }
                }
                else
                {
                    // taxRates = _commerceService. (context.StoreId, context.ToTaxEvaluationContextDto());
                    var activeTaxProvider = context.Store.TaxProviders.FirstOrDefault(x => x.IsActive);
                    if (activeTaxProvider != null)
                    {
                        taxRates.AddRange(activeTaxProvider.CalculateRates(context));
                    }
                }
            }

            ApplyTaxRates(taxRates, owners);
        }

        protected virtual decimal GetFixedTaxRate(IList<domain_tax_model.TaxProvider> taxProviders)
        {
            var result = 0m;
            var fixedTaxProvider = taxProviders.FirstOrDefault(x => x.IsActive && x.Code == "FixedRate");
            if (fixedTaxProvider != null && !fixedTaxProvider.Settings.IsNullOrEmpty())
            {
                result = fixedTaxProvider.Settings

                    // .Select(x => x.JsonConvert<Setting>().ToSettingEntry())
                    .GetSettingValue("VirtoCommerce.Core.FixedTaxRateProvider.Rate", 0.00m);
            }

            return result;
        }

        protected virtual void ApplyTaxRates(IList<domain_tax_model.TaxRate> taxRates, IEnumerable<ITaxable> owners)
        {
            if (taxRates == null)
            {
                return;
            }

            var taxRatesMap = owners.Select(x => x.Currency).Distinct().ToDictionary(x => x, x => taxRates.Select(r => r.ToTaxRate(x)).ToArray());

            foreach (var owner in owners)
            {
                owner.ApplyTaxRates(taxRatesMap[owner.Currency]);
            }
        }
    }
}
