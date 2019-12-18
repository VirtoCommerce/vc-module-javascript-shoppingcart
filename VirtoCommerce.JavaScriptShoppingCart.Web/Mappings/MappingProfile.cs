using AutoMapper;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;
using domain = VirtoCommerce.Domain.Cart.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<domain.ShoppingCart, ShoppingCart>();
		}
	}
}