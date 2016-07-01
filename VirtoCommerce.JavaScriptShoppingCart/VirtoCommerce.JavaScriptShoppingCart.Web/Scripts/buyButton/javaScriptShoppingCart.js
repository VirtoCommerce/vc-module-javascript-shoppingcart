angular.module('virtojavascriptshoppingcart', ['ngAnimate', 'ui.bootstrap', 'ngCookies']);

angular.module('virtojavascriptshoppingcart').controller('VirtoJavaScriptShoppingCartCtrl', function ($scope, $uibModal, $log, $cookies, $http) {

	$scope.apiKey = null;
	$scope.storeId = null;
	$scope.userId = null;
	$scope.cart = null;
	$scope.order = null;
	$scope.baseUrl = null;

	$scope.settings = {
		orderNumber: ""
	};

	$scope.open = function () {

		initialize();

		var itemId = event.target.attributes["data-item-id"].value;
		var catalogId = event.target.attributes["data-item-catalog-id"].value;
		var itemName = event.target.attributes["data-item-name"].value;
		var itemSku = event.target.attributes["data-item-sku"].value;
		var itemPrice = event.target.attributes["data-item-price"].value;
		var itemCurrency = event.target.attributes["data-item-currency"].value;

		$http({
			method: 'POST',
			url: $scope.baseUrl + 'additemtocart?api_key=' + $scope.apiKey,
			data: {
				storeId: $scope.storeId,
				userId: $scope.userId,
				productId: itemId,
				catalogId: catalogId,
				productName: itemName,
				productSku: itemSku,
				price: itemPrice,
				currency: itemCurrency
			}
		}).then(function (response) {
			if (response && response.data && response.data.cart) {
				$scope.cart = response.data.cart;
				$uibModal.open({
					animation: true,
					templateUrl: '/admin/Modules/JavaScriptShoppingCart/Scripts/buyButton/virtoJavaScriptShoppingCartTemplate.html',
					controller: 'VirtoJavaScriptShoppingCartInstanceCtrl',
					size: 'lg',
					resolve: {
						cart: function () {
							return $scope.cart;
						},
						createOrder: function () {
							return $scope.createOrder;
						},
						order: function () {
							return $scope.order;
						},
						settings: function () {
							return $scope.settings;
						}
					}
				});
			}
		});
	};

	$scope.createOrder = function () {
		$http({
			method: 'POST',
			url: $scope.baseUrl + 'createorder?api_key=' + $scope.apiKey,
			data: {
				cartId: $scope.cart.id
			}
		}).then(function (response) {
			if (response && response.data && response.data.order) {
				$scope.order = response.data.order;
				$scope.cart = null;
				$scope.settings.orderNumber = response.data.order.number;
				$cookies.put('virto-javascript-shoppingcart-cart-id', null);
			}
		});
	};

	function initialize() {
		$scope.apiKey = angular.element(document.querySelector('#javaScriptShoppingCart'))[0].attributes['data-api-key'].value;
		$scope.storeId = angular.element(document.querySelector('#javaScriptShoppingCart'))[0].attributes['data-store-id'].value;
		$scope.baseUrl = angular.element(document.querySelector('#javaScriptShoppingCart'))[0].attributes['data-base-url'].value;
		$scope.userId = $cookies.get('virto-javascript-shoppingcart-user-id');
		if (!$scope.userId) {
			$scope.userId = guid();
			var expireDate = new Date();
			expireDate.setDate(expireDate.getDate() + 1);
			$cookies.put('virto-javascript-shoppingcart-user-id', $scope.userId, { 'expires': expireDate });
		}
	}

	function guid() {
		function s4() {
			return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
		}
		return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
	}
});

angular.module('virtojavascriptshoppingcart').controller('VirtoJavaScriptShoppingCartInstanceCtrl', function ($scope, $uibModalInstance, cart, createOrder, order, settings) {
	$scope.cart = cart;
	$scope.createOrder = createOrder;
	$scope.order = order;
	$scope.settings = settings;
	$scope.cancel = function () {
		$uibModalInstance.dismiss('cancel');
	};
});