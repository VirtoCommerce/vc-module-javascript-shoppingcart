using System.Collections.Generic;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Marketing;
using marketing = VirtoCommerce.Domain.Marketing.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Services
{
    public interface IPromotionEvaluator
    {
        void EvaluateDiscounts(marketing.PromotionEvaluationContext context, IEnumerable<IDiscountable> owners);
    }
}
