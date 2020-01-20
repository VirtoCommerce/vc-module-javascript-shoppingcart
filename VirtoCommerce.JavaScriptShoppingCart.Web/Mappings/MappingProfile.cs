using AutoMapper;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;
using domain = VirtoCommerce.Domain.Cart.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<domain.ShoppingCart, ShoppingCart>(MemberList.None)
                .ConstructUsing(x => new ShoppingCart(new Currency(new Language(x.LanguageCode), x.Currency), new Language(x.LanguageCode)));
        }
    }
}
