angular.module('virtoCommerce.cartModule')
	.service('virtoCommerce.cartModule.api', ['$http', function ($http) {

	    function getUrl(cart) {
	        return cart.apiUrl + 'api/carts/' + cart.storeId + '/' + cart.userId + '/' + cart.name + '/' + cart.currency + '/' + cart.culture;
	    }

	    return {
	        getCart: function (cart) {
	            return $http.get(getUrl(cart) + '/current?api_key=' + cart.apiKey + '&t=' + new Date().getTime());
	        },
	        getCartItemsCount: function (cart) {
	            return $http.get(cart.apiUrl + 'api/carts/' + cart.id + '/itemscount?api_key=' + cart.apiKey + '&t=' + new Date().getTime());
	        },
	        addLineItem: function (cart, lineItem) {
	            return $http.post(cart.apiUrl + 'api/carts/' + cart.id + '/items?api_key=' + cart.apiKey, lineItem);
	        },
	        addProduct: function (cart, productId, quantity) {
	            return $http.post(cart.apiUrl + 'api/carts/' + cart.id + '/product?api_key=' + cart.apiKey + '&productId=' + productId + '&quantity=' + quantity, lineItem);
	        },
	        changeLineItem: function (cart, lineItemId, quantity) {
	            return $http.put(cart.apiUrl + 'api/carts/' + cart.id + '/items?api_key=' + cart.apiKey, { lineItemId: lineItemId, quantity: quantity });
	        },
	        removeLineItem: function (cart, lineItemId) {
	            return $http.delete(cart.apiUrl + 'api/carts/' + cart.id + '/items?api_key=' + cart.apiKey + '&lineItemId=' + lineItemId);
	        },
	        clearCart: function (cart) {
	            return $http.post(cart.apiUrl + 'api/carts/' + cart.id + '/clear?api_key=' + cart.apiKey);
	        },
	        addCoupon: function (cart, couponCode) {
	            return $http.post(cart.apiUrl + 'api/carts/' + cart.id + '/coupons/' + couponCode + '?api_key=' + cart.apiKey);
	        },
	        removeCoupon: function (cart) {
	            return $http.delete(cart.apiUrl + 'api/carts/' + cart.id + '/coupons?api_key=' + cart.apiKey);
	        },
	        addOrUpdateShipment: function (cart, shipment) {
	            return $http.post(cart.apiUrl + 'api/carts/' + cart.id + '/shipments?api_key=' + cart.apiKey, shipment);
	        },
	        addOrUpdatePayment: function (cart, payment) {
	            return $http.post(cart.apiUrl + 'api/carts/' + cart.id + '/payments?api_key=' + cart.apiKey, payment);
	        },
	        getAvailableShippingMethods: function (cart, shipmentId) {
	            return $http.get(cart.apiUrl + 'api/carts/' + cart.id + '/availshippingrates?api_key=' + cart.apiKey + '&t=' + new Date().getTime());
	        },
	        getAvailablePaymentMethods: function (cart) {
	            return $http.get(cart.apiUrl + 'api/carts/' + cart.id + '/availpaymentmethods?api_key=' + cart.apiKey);
	        },
	        removeCart: function (cart) {
	            return $http.delete(cart.apiUrl + 'api/carts?api_key=' + cart.apiKey + '&ids=' + cart.id);
	        },
	        createOrder: function (cart) {
	            return $http.post(cart.apiUrl + 'api/order/customerOrders/' + cart.id + '?api_key=' + cart.apiKey, cart);
	        }
	    }
	}]);
