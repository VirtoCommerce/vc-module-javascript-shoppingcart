using System;
using VirtoCommerce.Domain.Cart.Model;
using VirtoCommerce.Domain.Cart.Services;
using VirtoCommerce.Domain.Catalog.Services;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Services
{
	public class CartBuilder : ICartBuilder
	{
		private readonly IShoppingCartService _shoppingCartService;
		private readonly ICartBuilder _cartBuilder;
		private readonly IItemService _itemService;

		public virtual ShoppingCart Cart { get; protected set; }

		public CartBuilder(IShoppingCartService shoppingCartService, ICartBuilder cartBuilder, IItemService itemService)
		{
			_shoppingCartService = shoppingCartService;
			_cartBuilder = cartBuilder;
			_itemService = itemService;
		}

		public ICartBuilder TakeCart(ShoppingCart cart)
		{
			throw new NotImplementedException();
		}

		public ICartBuilder AddCoupon(string couponCode)
		{
			throw new NotImplementedException();
		}

		public ICartBuilder RemoveCoupon(string couponCode = null)
		{
			throw new NotImplementedException();
		}

		public ICartBuilder EvaluatePromotions()
		{
			throw new NotImplementedException();
		}

		public ICartBuilder EvaluateTaxes()
		{
			throw new NotImplementedException();
		}

		public ICartBuilder Validate()
		{
			throw new NotImplementedException();
		}

		public ICartBuilder Save()
		{
			throw new NotImplementedException();
		}

		//public virtual async Task GetCartAsync(string cartId)
		//{
		//	var store = _workContextAccessor.WorkContext.AllStores.FirstOrDefault(x => x.Id.EqualsInvariant(cart.StoreId));
		//	if (store == null)
		//	{
		//		throw new StorefrontException($"{ nameof(cart.StoreId) } not found");
		//	}
		//	//Load cart dependencies
		//	await PrepareCartAsync(cart, store);

		//	Cart = cart;
		//}

		//public virtual async Task TakeCartAsync(ShoppingCart cart)
		//{
		//	var store = _workContextAccessor.WorkContext.AllStores.FirstOrDefault(x => x.Id.EqualsInvariant(cart.StoreId));
		//	if (store == null)
		//	{
		//		throw new StorefrontException($"{ nameof(cart.StoreId) } not found");
		//	}
		//	//Load cart dependencies
		//	await PrepareCartAsync(cart, store);

		//	Cart = cart;
		//}

		//public virtual Task AddCouponAsync(string couponCode)
		//{
		//	if (!Cart.Coupons.Any(x => x.EqualsInvariant(couponCode)))
		//	{
		//		Cart.Coupons.Add(couponCode);
		//	}

		//	return Task.FromResult(this);
		//}

		//public virtual Task RemoveCouponAsync(string couponCode = null)
		//{
		//	EnsureCartExists();
		//	if (string.IsNullOrEmpty(couponCode))
		//	{
		//		Cart.Coupons.Clear();
		//	}
		//	else
		//	{
		//		Cart.Coupons.Remove(Cart.Coupons.FirstOrDefault(c => c.Code.EqualsInvariant(couponCode)));
		//	}
		//	return Task.FromResult((object)null);
		//}

		//public virtual async Task ValidateAsync()
		//{
		//	EnsureCartExists();
		//	await Task.WhenAll(ValidateCartItemsAsync(), ValidateCartShipmentsAsync());
		//	Cart.IsValid = Cart.Items.All(x => x.IsValid) && Cart.Shipments.All(x => x.IsValid);
		//}

		//public virtual async Task EvaluatePromotionsAsync()
		//{
		//	EnsureCartExists();

		//	var isReadOnlyLineItems = Cart.Items.Any(i => i.IsReadOnly);
		//	if (!isReadOnlyLineItems)
		//	{
		//		//Get product inventory to fill InStockQuantity parameter of LineItem
		//		//required for some promotions evaluation

		//		foreach (var lineItem in Cart.Items.Where(x => x.Product != null).ToList())
		//		{
		//			lineItem.InStockQuantity = (int)lineItem.Product.AvailableQuantity;
		//		}

		//		var evalContext = Cart.ToPromotionEvaluationContext();
		//		await _promotionEvaluator.EvaluateDiscountsAsync(evalContext, new IDiscountable[] { Cart });
		//	}
		//}

		//public virtual async Task EvaluateTaxesAsync()
		//{
		//	var workContext = _workContextAccessor.WorkContext;
		//	await _taxEvaluator.EvaluateTaxesAsync(Cart.ToTaxEvalContext(workContext.CurrentStore), new[] { Cart });
		//}

		//public virtual async Task SaveAsync()
		//{
		//	EnsureCartExists();

		//	await EvaluatePromotionsAsync();
		//	await EvaluateTaxesAsync();

		//	var cart = await _cartService.SaveChanges(Cart);
		//	//Evict cart from cache
		//	CartCacheRegion.ExpireCart(Cart);

		//	await TakeCartAsync(cart);
		//}

		//protected virtual async Task PrepareCartAsync(ShoppingCart cart, Store store)
		//{
		//	if (cart == null)
		//	{
		//		throw new ArgumentNullException(nameof(cart));
		//	}

		//	//Load products for cart line items
		//	if (cart.Items.Any())
		//	{
		//		var productIds = cart.Items.Select(i => i.ProductId).ToArray();
		//		var products = await _itemService.GetByIds(productIds, ItemResponseGroup.WithPrices | ItemResponseGroup.ItemWithDiscounts | ItemResponseGroup.Inventory | ItemResponseGroup.Outlines);
		//		foreach (var item in cart.Items)
		//		{
		//			item.Product = products.FirstOrDefault(x => x.Id.EqualsInvariant(item.ProductId));
		//		}
		//	}

		//	//Load cart payment plan with have same id
		//	if (store.SubscriptionEnabled)
		//	{
		//		var paymentPlanIds = new[] { cart.Id }.Concat(cart.Items.Select(x => x.ProductId).Distinct()).ToArray();
		//		var paymentPlans = await _subscriptionService.GetPaymentPlansByIdsAsync(paymentPlanIds);
		//		cart.PaymentPlan = paymentPlans.FirstOrDefault(x => x.Id == cart.Id);
		//		//Realize this code whith dictionary
		//		foreach (var lineItem in cart.Items)
		//		{
		//			lineItem.PaymentPlan = paymentPlans.FirstOrDefault(x => x.Id == lineItem.ProductId);
		//		}
		//	}
		//}

		//protected virtual Task ValidateCartItemsAsync()
		//{
		//	foreach (var lineItem in Cart.Items.ToList())
		//	{
		//		lineItem.ValidationErrors.Clear();

		//		if (lineItem.Product == null || !lineItem.Product.IsActive || !lineItem.Product.IsBuyable)
		//		{
		//			lineItem.ValidationErrors.Add(new UnavailableError());
		//		}
		//		else
		//		{
		//			var isProductAvailable = new ProductIsAvailableSpecification(lineItem.Product).IsSatisfiedBy(lineItem.Quantity);
		//			if (!isProductAvailable)
		//			{
		//				var availableQuantity = lineItem.Product.AvailableQuantity;
		//				lineItem.ValidationErrors.Add(new QuantityError(availableQuantity));
		//			}

		//			var tierPrice = lineItem.Product.Price.GetTierPrice(lineItem.Quantity);
		//			if (tierPrice.Price > lineItem.SalePrice)
		//			{
		//				lineItem.ValidationErrors.Add(new PriceError(lineItem.SalePrice, lineItem.SalePriceWithTax, tierPrice.Price, tierPrice.PriceWithTax));
		//			}
		//		}

		//		lineItem.IsValid = !lineItem.ValidationErrors.Any();
		//	}
		//	return Task.CompletedTask;
		//}

		//protected virtual async Task ValidateCartShipmentsAsync()
		//{
		//	foreach (var shipment in Cart.Shipments.ToArray())
		//	{
		//		shipment.ValidationErrors.Clear();

		//		var availShippingmethods = await GetAvailableShippingMethodsAsync();
		//		var shipmentShippingMethod = availShippingmethods.FirstOrDefault(sm => shipment.HasSameMethod(sm));
		//		if (shipmentShippingMethod == null)
		//		{
		//			shipment.ValidationErrors.Add(new UnavailableError());
		//		}
		//		else if (shipmentShippingMethod.Price != shipment.Price)
		//		{
		//			shipment.ValidationErrors.Add(new PriceError(shipment.Price, shipment.PriceWithTax, shipmentShippingMethod.Price, shipmentShippingMethod.PriceWithTax));
		//		}
		//	}
		//}
	}
}