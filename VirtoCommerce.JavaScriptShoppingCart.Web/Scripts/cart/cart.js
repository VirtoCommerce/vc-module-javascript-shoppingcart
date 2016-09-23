var cartModule = angular.module('virtoCommerce.cartModule', ['ngAnimate', 'ui.bootstrap', 'ngCookies', 'pascalprecht.translate', 'angular.filter']);

cartModule.config(['$translateProvider', 'virtoCommerce.cartModule.translations', function ($translateProvider, translations) {
	$translateProvider.useSanitizeValueStrategy('sanitizeParameters');
	$translateProvider.translations('en', translations);
	$translateProvider.preferredLanguage('en');
}]);

cartModule.factory('virtoCommerce.cartModule.carts', [function () {
	return {};
}]);

cartModule.component('vcCart', {
	templateUrl: "",
	bindings: {
		name: '@',
		apiKey: '@',
		apiUrl: '@',
		storeId: '@',
		customerId: '@',
		currencyCode: '@',
		culture: '@'
	},
	controller: ['virtoCommerce.cartModule.carts', 'virtoCommerce.cartModule.api', 'virtoCommerce.cartModule.countriesService', 'virtoCommerce.cartModule.currenciesService', '$cookies', function (carts, cartApi, countriesService, currenciesService, $cookies) {
		var ctrl = this;
		carts[ctrl.name] = this;

		ctrl.currency = ctrl.currencyCode;
		ctrl.availCountries = [];

		this.reloadCart = function () {
			return wrapLoading(function () {
				return cartApi.getCart(ctrl).then(function (response) {
					angular.extend(ctrl, response.data);
					if (response.data.coupon) {
						ctrl.coupon = response.data.coupon;
						ctrl.coupon.isApplied = true;
					}
					return ctrl;
				}).then(function (cart) {
					ctrl.availCountries = countriesService.countries;
					ctrl.currencySymbol = _.find(currenciesService.currencies, function (x) { return x.code === cart.currencyCode; }).symbol;
					return cart;
				});
			});
		};

		this.addLineItem = function (lineItem) {
			return wrapLoading(function () {
				return cartApi.addLineItem(ctrl, lineItem).then(function () {
					return ctrl.reloadCart();
				});
			});
		}

		this.getAvailShippingMethods = function (shipment) {
			return cartApi.getAvailableShippingMethods(ctrl, shipment.id).then(function (response) {
				return response.data;
			});
		}

		this.getCountryRegions = function (country) {
			return cartApi.getCountryRegions(ctrl, country.code3).then(function (response) {
				return response.data;
			});
		};

		this.applyCoupon = function (coupon) {
			return cartApi.addCoupon(ctrl, coupon.code).then(function (response) {
				ctrl.reloadCart();
				return response.data;
			});
		};

		this.removeCoupon = function (coupon) {
			return cartApi.removeCoupon(ctrl).then(function () {
				return ctrl.reloadCart();
			});
		};

		this.addOrUpdateShipment = function (shipment) {
			shipment.currency = this.currency;
			return cartApi.addOrUpdateShipment(ctrl, shipment).then(function () {
				return ctrl.reloadCart();
			});
		}

		this.addOrUpdatePayment = function (payment) {
			payment.currency = this.currency;
			return cartApi.addOrUpdatePayment(ctrl, payment);
		}

		this.createOrder = function () {
			return cartApi.createOrder(ctrl);
		}

		this.removeCart = function () {
		    return cartApi.removeCart(ctrl);
		}

		this.getAvailPaymentMethods = function () {
			return cartApi.getAvailablePaymentMethods(ctrl).then(function (response) {
				return response.data;
			});
		}

		this.buyOnClick = function (product) {
			alert(product);
		};

		this.checkout = function () {
			if (this.checkout) {
				this.checkout.show();
			}
		};

		function wrapLoading(func) {
			ctrl.loading = true;
			return func().then(function (result) {
				ctrl.loading = false;
				return result;
			},
			function () {
				ctrl.loading = false;
			});
		}

		this.initializeUser = function () {
			this.userId = $cookies.get('virto-javascript-shoppingcart-user-id');
			if (!this.userId) {
				this.userId = guid();
				var expireDate = new Date();
				expireDate.setDate(expireDate.getDate() + 1);
				$cookies.put('virto-javascript-shoppingcart-user-id', this.userId, { 'expires': expireDate });
			}
		}

		function guid() {
			function s4() {
				return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
			}
			return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
		}

		this.initializeUser();

		this.reloadCart();
	}]
});

cartModule.controller('virtoCommerce.cartModule.cartController', ['$scope', '$uibModal', 'virtoCommerce.cartModule.carts', function ($scope, $uibModal, carts) {

	$scope.carts = carts;

	$scope.openCheckout = function () {
		$uibModal.open({
			animation: true,
			templateUrl: 'checkout-modal.tpl.html',
			controller: 'VirtoJavaScriptShoppingCartInstanceCtrl',
			resolve: {
				carts: function () {
					return $scope.carts;
				}
			}
		});
	};
}]);

cartModule.controller('VirtoJavaScriptShoppingCartInstanceCtrl', ['$scope', '$uibModalInstance', 'carts', function ($scope, $uibModalInstance, carts) {

	$scope.carts = carts;

	$scope.cancel = function () {
		$uibModalInstance.dismiss('cancel');
	};
}]);
