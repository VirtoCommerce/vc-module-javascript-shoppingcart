using System.Collections.Generic;
using System.Linq;
using VirtoCommerce.Domain.Marketing.Services;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Marketing;
using VirtoCommerce.JavaScriptShoppingCart.Data.Converters;
using marketing = VirtoCommerce.Domain.Marketing.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Services
{
    public class PromotionEvaluator
    {
        private readonly IMarketingPromoEvaluator _marketingPromoEvaluator;

        public PromotionEvaluator(IMarketingPromoEvaluator marketingPromoEvaluator)
        {
            _marketingPromoEvaluator = marketingPromoEvaluator;
        }

        public virtual void EvaluateDiscounts(marketing.PromotionEvaluationContext context, IEnumerable<IDiscountable> owners)
        {
            var rewards = _marketingPromoEvaluator.EvaluatePromotion(context).Rewards.ToList();
            ApplyRewards(rewards, owners);
        }


        protected virtual void ApplyRewards(IList<marketing.PromotionReward> rewards, IEnumerable<IDiscountable> owners)
        {
            if (rewards != null)
            {
                var rewardsMap = owners.Select(x => x.Currency).Distinct().ToDictionary(x => x, x => rewards.Select(r => r.ToPromotionReward(x)).ToArray());

                foreach (var owner in owners)
                {
                    owner.ApplyRewards(rewardsMap[owner.Currency]);
                }
            }
        }
    }
}
