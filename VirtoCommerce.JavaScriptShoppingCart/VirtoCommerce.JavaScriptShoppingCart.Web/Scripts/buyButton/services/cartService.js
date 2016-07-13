angular.module('storefrontApp').service('cartService1', ['$http', '$q', function ($http, $q) {
	return {
		getCart: function (baseUrl, apiKey) {
			return $http.jsonp('api/javascriptshoppingcart/cart?callback=JSON_CALLBACK&t=' + new Date().getTime());
		},
		getCartItemsCount: function (baseUrl, apiKey) {
			return $http.jsonp('api/javascriptshoppingcart/itemscount?callback=JSON_CALLBACK&t=' + new Date().getTime());
		},
		addLineItem: function (baseUrl, apiKey, productId, quantity) {
			return $http.jsonp('api/javascriptshoppingcart/items?callback=JSON_CALLBACK', { id: productId, quantity: quantity });
		},
		changeLineItemQuantity: function (baseUrl, apiKey, lineItemId, quantity) {
			return $http.jsonp('api/javascriptshoppingcart/items?callback=JSON_CALLBACK', { lineItemId: lineItemId, quantity: quantity });
		},
		removeLineItem: function (baseUrl, apiKey, lineItemId) {
			return $http.jsonp('api/javascriptshoppingcart/items?callback=JSON_CALLBACK&lineItemId=' + lineItemId);
		},
		clearCart: function (baseUrl, apiKey) {
			return $http.jsonp('api/javascriptshoppingcart/clear?callback=JSON_CALLBACK');
		},
		getCountries: function (baseUrl, apiKey) {
			return $http.jsonp(baseUrl + 'api/javascriptshoppingcart/countries?api_key=' + apiKey + '&callback=JSON_CALLBACK&t=' + new Date().getTime());
		},
		getCountryRegions: function (baseUrl, apiKey, countryCode) {
			return $http.jsonp('api/javascriptshoppingcart/countries/' + countryCode + '/regions?callback=JSON_CALLBACK&t=' + new Date().getTime());
		},
		addCoupon: function (baseUrl, apiKey, couponCode) {
			return $http.jsonp('api/javascriptshoppingcart/coupons/' + couponCode + '?callback=JSON_CALLBACK');
		},
		removeCoupon: function (baseUrl, apiKey) {
			return $http.jsonp('api/javascriptshoppingcart/coupons?callback=JSON_CALLBACK');
		},
		addOrUpdateShipment: function (baseUrl, apiKey, shipment) {
			return $http.jsonp('api/javascriptshoppingcart/shipments?callback=JSON_CALLBACK', { shipment: shipment });
		},
		addOrUpdatePayment: function (baseUrl, apiKey, payment) {
			return $http.jsonp('api/javascriptshoppingcart/payments?callback=JSON_CALLBACK', { payment: payment });
		},
		getAvailableShippingMethods: function (baseUrl, apiKey, shipmentId) {
			return $http.jsonp('api/javascriptshoppingcart/shipments/' + shipmentId + '/shippingmethods?callback=JSON_CALLBACK&t=' + new Date().getTime());
		},
		getAvailablePaymentMethods: function (baseUrl, apiKey) {
			return $http.jsonp('api/javascriptshoppingcart/paymentmethods?callback=JSON_CALLBACK&t=' + new Date().getTime());
		},
		createOrder: function (baseUrl, apiKey, bankCardInfo) {
			return $http.jsonp('api/javascriptshoppingcart/createorder?callback=JSON_CALLBACK', { bankCardInfo: bankCardInfo });
		}
	}
}]);