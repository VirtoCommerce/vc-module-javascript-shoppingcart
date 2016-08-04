var app = angular.module('storefrontApp', ['ngAnimate', 'ui.bootstrap', 'ngCookies', 'storefront.checkout']);

angular.module('storefrontApp').controller('javaScriptShoppingCartCtrl', ['$scope', '$uibModal', '$log', '$cookies', '$http', 'cartService', function ($scope, $uibModal, $log, $cookies, $http, cartService) {

	$scope.javaScriptShoppingCart = {};

	initialize();

	function initialize() {
		$scope.javaScriptShoppingCart.apiKey = angular.element(document.querySelector('#javaScriptShoppingCart'))[0].attributes['data-api-key'].value;
		$scope.javaScriptShoppingCart.storeId = angular.element(document.querySelector('#javaScriptShoppingCart'))[0].attributes['data-store-id'].value;
		$scope.javaScriptShoppingCart.baseUrl = angular.element(document.querySelector('#javaScriptShoppingCart'))[0].attributes['data-base-url'].value;

		$scope.javaScriptShoppingCart.userId = $cookies.get('virto-javascript-shoppingcart-user-id');
		if (!$scope.javaScriptShoppingCart.userId) {
			$scope.javaScriptShoppingCart.userId = guid();
			var expireDate = new Date();
			expireDate.setDate(expireDate.getDate() + 1);
			$cookies.put('virto-javascript-shoppingcart-user-id', $scope.javaScriptShoppingCart.userId, { 'expires': expireDate });
		}
	}

	$scope.open = function () {

		var itemId = getAttributeValue(event.target, "data-item-id");
		var catalogId = getAttributeValue(event.target, "data-item-catalog-id");
		var itemName = getAttributeValue(event.target, "data-item-name");
		var itemSku = getAttributeValue(event.target, "data-item-sku");
		var itemPrice = getAttributeValue(event.target, "data-item-price");
		var itemCurrency = getAttributeValue(event.target, "data-item-currency");

		var lineItem = {
			storeId: $scope.javaScriptShoppingCart.storeId,
			userId: $scope.javaScriptShoppingCart.userId,
			productId: itemId || "itemId",
			catalogId: catalogId || "catalogId",
			productName: itemName,
			productSku: itemSku,
			price: itemPrice,
			currency: itemCurrency,
			includeCartTemplate: !$scope.javaScriptShoppingCart.cartTemplate
		};

		cartService.addLineItem(lineItem, $scope.javaScriptShoppingCart.baseUrl, $scope.javaScriptShoppingCart.apiKey).then(function (response) {
			$uibModal.open({
				animation: true,
				templateUrl: 'virtoJavaScriptShoppingCartTemplate.tpl.html',
				controller: 'VirtoJavaScriptShoppingCartInstanceCtrl',
				//size: 'lg',
				resolve: {
					javaScriptShoppingCart: function () {
						return $scope.javaScriptShoppingCart;
					}
				}
			});
		});
	};

	function getAttributeValue(value, attributeName) {
		var result = null;
		var attribute = value.attributes[attributeName];
		if (attribute) {
			result = attribute.value;
		}
		return result;
	}

	function guid() {
		function s4() {
			return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
		}
		return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
	}
}]);

angular.module('storefrontApp').controller('VirtoJavaScriptShoppingCartInstanceCtrl', ['$scope', '$uibModalInstance', 'javaScriptShoppingCart', function ($scope, $uibModalInstance, javaScriptShoppingCart) {

	$scope.javaScriptShoppingCart = javaScriptShoppingCart;

	$scope.cancel = function () {
		$uibModalInstance.dismiss('cancel');
	};
}]);

