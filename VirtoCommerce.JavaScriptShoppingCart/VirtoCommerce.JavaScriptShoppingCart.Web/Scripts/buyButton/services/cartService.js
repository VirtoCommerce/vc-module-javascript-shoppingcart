angular.module('storefrontApp').service('cartService', ['$http', 'config', function ($http, config) {
	return {
		getCart: function (cartContext) {
			return $http.post(config.apiUrl + 'api/checkout/carts/current', cartContext);
		},
		//getCartItemsCount: function () {
		//	return $http.get('storefrontapi/cart/itemscount?t=' + new Date().getTime());
		//},
		addLineItem: function (addItemModel) {
			return $http.post(config.apiUrl + 'api/checkout/cart/items', addItemModel);
		},
		//changeLineItemQuantity: function (lineItemId, quantity) {
		//	return $http.put('storefrontapi/cart/items', { lineItemId: lineItemId, quantity: quantity });
		//},
		//removeLineItem: function (lineItemId) {
		//	return $http.delete('storefrontapi/cart/items?lineItemId=' + lineItemId);
		//},
		//clearCart: function () {
		//	return $http.post('storefrontapi/cart/clear');
		//},
		getCountries: function () {
			return $http.get(config.apiUrl + 'api/checkout/countries?t=' +new Date().getTime());
		},
		getCountryRegions: function (countryCode) {
			return $http.get(config.apiUrl + 'api/checkout/countries/' + countryCode + '/regions?t=' +new Date().getTime());
		},
		//addCoupon: function (couponCode) {
		//	return $http.post('storefrontapi/cart/coupons/' + couponCode);
		//},
		//removeCoupon: function () {
		//	return $http.delete('storefrontapi/cart/coupons');
		//},
		addOrUpdateShipment: function (shipment) {
			return $http.post(config.apiUrl + 'api/checkout/cart/shipments', shipment);
		},
		addOrUpdatePayment: function (payment) {
			return $http.post(config.apiUrl + 'api/checkout/cart/payments', payment);
		},
		getAvailableShippingMethods: function (cartContext, shipmentId) {
			return $http.post(config.apiUrl + 'api/checkout/cart/shipments/' + shipmentId + '/shippingmethods?t=' +new Date().getTime(), cartContext);
		},
		getAvailablePaymentMethods: function (cartContext) {
			return $http.post(config.apiUrl + 'api/checkout/cart/paymentmethods', cartContext);
		},
		createOrder: function (createOrderModel) {
			return $http.post(config.apiUrl + 'api/checkout/cart/createorder', createOrderModel);
		}
	}
}]);
