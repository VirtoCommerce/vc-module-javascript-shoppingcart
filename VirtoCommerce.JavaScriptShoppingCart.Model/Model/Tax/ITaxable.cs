using System.Collections.Generic;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Tax
{
    /// <summary>
    /// It is an abstraction that represents a taxable entity.
    /// </summary>
    public interface ITaxable
    {
        Currency Currency { get; }

        Money TaxTotal { get; }

        decimal TaxPercentRate { get; }

        string TaxType { get; }

        IList<TaxDetail> TaxDetails { get; }

        void ApplyTaxRates(IEnumerable<TaxRate> taxRates);
    }
}
