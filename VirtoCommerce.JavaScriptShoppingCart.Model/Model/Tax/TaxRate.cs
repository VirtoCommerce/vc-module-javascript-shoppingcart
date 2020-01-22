using System;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Tax
{
    public partial class TaxRate
    {
        public TaxRate(Currency currency)
        {
            Rate = new Money(currency);
        }

        public Money Rate { get; set; }

        public decimal PercentRate { get; set; }

        public TaxLine Line { get; set; }

        public static decimal TaxPercentRound(decimal percent)
        {
            return Math.Round(percent, 4, MidpointRounding.AwayFromZero);
        }
    }
}
