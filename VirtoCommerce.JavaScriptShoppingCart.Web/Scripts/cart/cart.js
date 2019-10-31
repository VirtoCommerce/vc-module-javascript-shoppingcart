var cartModule = angular.module('virtoCommerce.cartModule', ['ngAnimate', 'ui.bootstrap', 'ngCookies', 'pascalprecht.translate', 'angular.filter', 'credit-cards']);

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
	controller: ['virtoCommerce.cartModule.carts', 'virtoCommerce.cartModule.api', 'virtoCommerce.cartModule.countriesService', 'virtoCommerce.cartModule.currenciesService', '$cookies', '$timeout', '$rootScope', 
	function (carts, cartApi, countriesService, currenciesService, $cookies, $timeout, $rootScope) {
		var timer;
		var ctrl = this;
		carts[ctrl.name] = this;

		ctrl.currency = ctrl.currencyCode;
		ctrl.availCountries = [];

		this.cartIsUpdating = false;

		this.reloadCart = function () {
			return wrapLoading(function () {
				return cartApi.getCart(ctrl).then(function (response) {
					this.cartIsUpdating = false;
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
					$rootScope.$broadcast('cartItemsChanged');
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

		this.removeLineItem = function (lineItemId){
			var lineItem = _.find(this.items, function (i) { return i.id == lineItemId });
			if (!lineItem || this.cartIsUpdating) {
				return;
			}
			this.cartIsUpdating = true;
			
			cartApi.removeLineItem(ctrl, lineItemId).then(function (response) {
				ctrl.reloadCart();
				$rootScope.$broadcast('cartItemsChanged');
			});

			this.cartIsUpdating = false;
		}

		this.changeLineItemQuantity = function (lineItemId, quantity) {
			var lineItem = _.find(this.items, function (i) { return i.id == lineItemId });
			if (!lineItem || quantity < 1 || this.cartIsUpdating) {
				return;
			}
			var initialQuantity = lineItem.quantity;
			lineItem.quantity = quantity;
			$timeout.cancel(timer);
			timer = $timeout(function () {
				this.cartIsUpdating = true;
				cartApi.changeLineItem(ctrl, lineItemId, quantity).then(function (response) {
					this.reloadCart()
					$rootScope.$broadcast('cartItemsChanged');
				}, function (response) {
					lineItem.quantity = initialQuantity;
					this.cartIsUpdating = false;
				});
			}, 300);
		}
	

		this.addOrUpdateShipment = function (shipment) {
			shipment.currency = this.currency;
			return cartApi.addOrUpdateShipment(ctrl, shipment).then(function () {
				return ctrl.reloadCart();
			});
		};

		this.addOrUpdatePayment = function (payment) {
			payment.currency = this.currency;
			return cartApi.addOrUpdatePayment(ctrl, payment);
		};

		this.createOrder = function () {
			return cartApi.createOrder(ctrl);
		};

		this.removeCart = function () {
		    return cartApi.removeCart(ctrl).then(function() {
				$rootScope.$broadcast('cartItemsChanged');
			});
		};

		this.clearCart = function () {
		    return cartApi.clearCart(ctrl).then(function() {
				ctrl.reloadCart();
				$rootScope.$broadcast('cartItemsChanged');
			});
		};

		this.getAvailPaymentMethods = function () {
			return cartApi.getAvailablePaymentMethods(ctrl).then(function (response) {
				return response.data;
			});
		};

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

	$scope.openCheckout = function (cart) {
		
		$scope.cart =  !cart ? $scope.carts['default'] : cart;

		$uibModal.open({
			animation: true,
			templateUrl: 'checkout-modal.tpl.html',
			controller: 'virtoCommerce.cartModule.checkoutController',
			resolve: { 
				cart : function () {
					return $scope.cart;
				  }
	    	}
		});
	};

	$scope.addLineItem = function(lineItem, cart) {

		$scope.cart =  !cart ? $scope.carts['default'] : cart;
		$scope.lineItem = lineItem;
		$scope.lineItem.currencySymbol = $scope.cart.currencySymbol;

		$scope.cart.addLineItem($scope.lineItem).then(function (cart) {
			$uibModal.open({
				animation: true,
				templateUrl: 'recently-added-cart-item-dialog.tpl.html',
				controller: 'virtoCommerce.cartModule.addItemViewController',
				resolve: { 
					cart : function () {
						return cart;
					  },
					lineItem: function () {
						return $scope.lineItem;
					},
					callback: function () {
						return $scope.openCart;
					}
				}
			});
		});

	};

	$scope.openCart = function(cart) {

		$scope.cart =  !cart ? $scope.carts['default'] : cart;
		$scope.cart.reloadCart().then(function (cart) {
			$uibModal.open({
				animation: true,
				templateUrl: 'shoppingCart.tpl.html',
				controller: 'virtoCommerce.cartModule.cartViewController',
				resolve: { 
					cart : function () {
						return cart;
					  },					
					callback: function () {
						return $scope.openCheckout;
					}
				}
			}); 
	
		});
	};
}]);

cartModule.controller('virtoCommerce.cartModule.addItemViewController', ['$scope', '$uibModalInstance', 'cart',  'lineItem', 'callback', function ($scope, $uibModalInstance, cart, lineItem, callback) {

	$scope.cart = cart;
	$scope.lineItem = lineItem;
	$scope.callbackFunc = callback;

	$scope.cancel = function () {
		$uibModalInstance.dismiss('cancel');
	};

	$scope.ok = function(){
        $scope.callbackFunc();
	};
}]);


cartModule.controller('virtoCommerce.cartModule.checkoutController', ['$scope', '$uibModalInstance', 'cart' , function ($scope, $uibModalInstance, cart) {
	$scope.cart = cart;
	$scope.cancel = function () {
		$uibModalInstance.dismiss('cancel');
	};
}]);


cartModule.controller('virtoCommerce.cartModule.cartViewController', ['$scope', '$uibModalInstance', 'cart', 'callback', function ($scope, $uibModalInstance, cart, callback) {

	$scope.cart = cart;
	$scope.callbackFunc = callback;

	$scope.cancel = function () {
		$uibModalInstance.dismiss('cancel');
	};

	$scope.clearCart = function () {
		cart.clearCart(cart);
	};

	//TODO: ui loader when action not finished yet
	$scope.removeLineItem = function(lineItemId) {
		cart.removeLineItem(lineItemId);
	}

	//TODO: ui loader when action not finished yet
	$scope.changeLineItemQuantity = function (lineItemId, quantity) {
		cart.changeLineItemQuantity(lineItemId, quantity);
	}

	$scope.ok = function(){
        $scope.callbackFunc();
	};
}]);
