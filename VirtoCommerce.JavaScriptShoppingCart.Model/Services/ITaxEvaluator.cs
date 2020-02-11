using System.Collections.Generic;
using VirtoCommerce.Domain.Tax.Model;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Tax;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Services
{
    public interface ITaxEvaluator
    {
        void EvaluateTaxes(TaxEvaluationContext context, IEnumerable<ITaxable> owners);
    }
}
