using VirtoCommerce.Domain.Cart.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Services
{
	/// <summary>
	/// Represent abstraction for working with customer shopping cart.
	/// </summary>
	public interface ICartBuilder
	{
		ShoppingCart Cart { get; }

		/// <summary>
		///  Capture cart and all next changes will be implemented on it.
		/// </summary>
		/// <param name="cart"></param>
		/// <returns></returns>
		ICartBuilder TakeCart(ShoppingCart cart);

		/// <summary>
		/// Apply marketing coupon to captured cart.
		/// </summary>
		/// <param name="couponCode"></param>
		/// <returns></returns>
		ICartBuilder AddCoupon(string couponCode);

		/// <summary>
		/// remove exist coupon from cart.
		/// </summary>
		/// <param name="couponCode"></param>
		/// <returns></returns>
		ICartBuilder RemoveCoupon(string couponCode = null);

		/// <summary>
		/// Evaluate marketing discounts for captured cart.
		/// </summary>
		/// <returns></returns>
		ICartBuilder EvaluatePromotions();

		/// <summary>
		/// Evaluate taxes  for captured cart.
		/// </summary>
		/// <returns></returns>
		ICartBuilder EvaluateTaxes();

		/// <summary>
		/// Validates cart content.
		/// </summary>
		/// <returns></returns>
		ICartBuilder Validate();

		/// <summary>
		/// Saves the cart.
		/// </summary>
		/// <returns></returns>
		ICartBuilder Save();
	}
}
