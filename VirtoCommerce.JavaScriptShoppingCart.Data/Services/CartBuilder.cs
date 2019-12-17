using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Services;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Services
{
	public class CartBuilder : ICartBuilder
	{
		public ShoppingCart Cart => throw new NotImplementedException();

		public Task AddCouponAsync(string couponCode)
		{
			throw new NotImplementedException();
		}

		public Task<bool> AddItemAsync(string productId, int quantity)
		{
			throw new NotImplementedException();
		}

		public Task AddOrUpdatePaymentAsync(Payment payment)
		{
			throw new NotImplementedException();
		}

		public Task AddOrUpdateShipmentAsync(Shipment shipment)
		{
			throw new NotImplementedException();
		}

		public Task ChangeItemQuantityAsync(string lineItemId, int quantity)
		{
			throw new NotImplementedException();
		}

		public Task ChangeItemQuantityAsync(int lineItemIndex, int quantity)
		{
			throw new NotImplementedException();
		}

		public Task ChangeItemsQuantitiesAsync(int[] quantities)
		{
			throw new NotImplementedException();
		}

		public Task ClearAsync()
		{
			throw new NotImplementedException();
		}

		public Task EvaluatePromotionsAsync()
		{
			throw new NotImplementedException();
		}

		public Task EvaluateTaxesAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<PaymentMethod>> GetAvailablePaymentMethodsAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<ShippingMethod>> GetAvailableShippingMethodsAsync()
		{
			throw new NotImplementedException();
		}

		public void LoadOrCreateNewTransientCart(string cartId, string storeId, string userId, string language, string currency)
		{
			throw new NotImplementedException();
		}

		public Task LoadOrCreateNewTransientCartAsync(string cartId, string storeId, string userId, string language, string currency)
		{
			throw new NotImplementedException();
		}

		public Task MergeWithCartAsync(ShoppingCart cart)
		{
			throw new NotImplementedException();
		}

		public Task RemoveCartAsync()
		{
			throw new NotImplementedException();
		}

		public Task RemoveCouponAsync(string couponCode = null)
		{
			throw new NotImplementedException();
		}

		public Task RemoveItemAsync(string lineItemId)
		{
			throw new NotImplementedException();
		}

		public Task RemoveShipmentAsync(string shipmentId)
		{
			throw new NotImplementedException();
		}

		public Task SaveAsync()
		{
			throw new NotImplementedException();
		}

		public Task TakeCartAsync(ShoppingCart cart)
		{
			throw new NotImplementedException();
		}

		public Task UpdateCartComment(string comment)
		{
			throw new NotImplementedException();
		}

		public Task ValidateAsync()
		{
			throw new NotImplementedException();
		}
	}
}