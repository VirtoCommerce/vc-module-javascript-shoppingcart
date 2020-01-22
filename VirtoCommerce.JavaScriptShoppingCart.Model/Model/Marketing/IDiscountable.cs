using System.Collections.Generic;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Marketing
{
    public interface IDiscountable
    {
        Currency Currency { get; }

        IList<Discount> Discounts { get; }

        void ApplyRewards(IEnumerable<PromotionReward> rewards);
    }
}
