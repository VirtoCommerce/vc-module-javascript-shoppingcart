﻿var app = angular.module('storefrontApp', ['ngAnimate', 'ui.bootstrap', 'ngCookies', 'storefront.checkout']);

app.value('config', {
	apiUrl: ''
});

angular.module('storefrontApp').controller('javaScriptShoppingCartCtrl', ['$scope', '$uibModal', '$log', '$cookies', '$http', 'config', 'cartService', function ($scope, $uibModal, $log, $cookies, $http, config, cartService) {

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

		config.apiUrl = $scope.javaScriptShoppingCart.baseUrl;
	}

	$scope.open = function () {

		var itemId = getAttributeValue(event.target, "data-item-id");
		var catalogId = getAttributeValue(event.target, "data-item-catalog-id");
		var itemName = getAttributeValue(event.target, "data-item-name");
		var itemSku = getAttributeValue(event.target, "data-item-sku");
		var itemPrice = getAttributeValue(event.target, "data-item-price");
		var itemCurrency = getAttributeValue(event.target, "data-item-currency");
		var imageUrl = getAttributeValue(event.target, "data-item-image-url");
		var thumbnailImageUrl = getAttributeValue(event.target, "data-item-thumbnail-image-url");

		var cartContext = {
			storeId: $scope.javaScriptShoppingCart.storeId,
			cartName: "javaScriptShoppingCart",
			customerId: $scope.javaScriptShoppingCart.userId,
			customerName: "Anonymous",
			currency: itemCurrency,
			languageCode: "ENG"
		};

		var addItemModel = {
			cartContext: cartContext,
			productId: itemId || "itemId",
			CatalogId: catalogId || "catalogId",
			Sku: itemSku,
			Name: itemName,
			ImageUrl: imageUrl,
			ThumbnailImageUrl: thumbnailImageUrl,
			Quantity: 1,
			ListPrice: itemPrice,
			SalePrice: itemPrice,
			ExtendedPrice: itemPrice,
			DiscountTotal: 0,
			TaxTotal: 0
		};

		$scope.javaScriptShoppingCart.cartContext = cartContext;

		//$scope.javaScriptShoppingCart.baseUrl, $scope.javaScriptShoppingCart.apiKey

		cartService.addLineItem(addItemModel).then(function (response) {
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

