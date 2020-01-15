var cartModule = angular.module('virtoCommerce.cartModule', ['ngAnimate', 'ui.bootstrap', 'ngCookies', 'pascalprecht.translate', 'angular.filter', 'credit-cards', 'LocalStorageModule']);

cartModule.config(['$translateProvider', 'virtoCommerce.cartModule.translations', '$httpProvider', function ($translateProvider, translations, $httpProvider) {
	$translateProvider.useSanitizeValueStrategy('sanitizeParameters');
	$translateProvider.translations('en', translations);
    $translateProvider.preferredLanguage('en');
    //Add interceptor
    $httpProvider.interceptors.push('virtoCommerce.cartModule.httpErrorInterceptor');

}]);

cartModule.factory('virtoCommerce.cartModule.httpErrorInterceptor', ['$q', '$rootScope', '$injector', 'virtoCommerce.cartModule.authDataStorage', function ($q, $rootScope, $injector, authDataStorage) {
    var httpErrorInterceptor = {};
    const unauthorized = 401;

    httpErrorInterceptor.request = function (config) {
        config.headers = config.headers || {};

        return extractAuthData()
            .then(function (authData) {
                if (authData) {
                    config.headers.Authorization = 'Bearer ' + authData.token;
                }

                return config;
            }).finally(function () {
                if (!config.cache) {
                    $rootScope.$broadcast('httpRequestSuccess', config);
                }
            });
    };

    function extractAuthData() {
        var authData = authDataStorage.getStoredData();
        if (!authData) {
            return $q.resolve();
        }

        if (Date.now() < authData.expiresAt) {
            return $q.resolve(authData);
        }

        var authService = $injector.get('virtoCommerce.cartModule.authService');
        return authService.refreshToken();
    }

    httpErrorInterceptor.responseError = function (rejection) {
        if (rejection.status === unauthorized) {
            $rootScope.$broadcast('unauthorized', rejection);
        } else {
            $rootScope.$broadcast('httpError', rejection);
        }
        return $q.reject(rejection);
    };

    httpErrorInterceptor.requestError = function (rejection) {
        $rootScope.$broadcast('httpError', rejection);
        return $q.reject(rejection);
    };

    return httpErrorInterceptor;
}]);


cartModule.factory('virtoCommerce.cartModule.carts', [function () {
	return {};
}]);

cartModule.component('vcCart', {
	templateUrl: "cart.tpl.html",
	bindings: {
		name: '@',
		apiKey: '@',
		apiUrl: '@',
		storeId: '@',
		customerId: '@',
		currencyCode: '@',
		culture: '@'
	},
    controller: ['virtoCommerce.cartModule.carts', 'virtoCommerce.cartModule.api', 'virtoCommerce.cartModule.countriesService', 'virtoCommerce.cartModule.currenciesService', '$cookies', '$timeout', '$rootScope', '$scope', 'virtoCommerce.cartModule.authDataStorage', 'virtoCommerce.cartModule.authService', 
        function (carts, cartApi, countriesService, currenciesService, $cookies, $timeout, $rootScope, $scope, authDataStorage, authService) {
		var timer;
		var ctrl = this;
		carts[ctrl.name] = this;

		ctrl.currency = ctrl.currencyCode;
		ctrl.availCountries = [];

		this.cartIsUpdating = false;

        authDataStorage.setPlatformKey(ctrl.apiKey);
        authDataStorage.setPlatformUrl(ctrl.apiUrl);

		$scope.$on('cartItemsChanged', function (event, data) {
			ctrl.getCartItemsCount();
		});

		this.reloadCart = function () {
            return wrapLoading(function () {
                    return cartApi.getCart(ctrl).then(function(response) {
                        angular.merge(ctrl, response.data);
                        if (response.data.coupon) {
                            ctrl.coupon = response.data.coupon;
                            ctrl.coupon.isApplied = true;
                        }
                        ctrl.getCartItemsCount();
                        ctrl.cartIsUpdating = false;
                        return ctrl;
                    }).then(function(cart) {
                        ctrl.availCountries = countriesService.countries;
                        ctrl.currencySymbol = _.find(currenciesService.currencies,
                            function(x) { return x.code === cart.currencyCode; }).symbol;
                        return cart;
                    });
                });
		};

        this.addLineItem = function(lineItem) {
            return wrapLoading(function() {
                return cartApi.addLineItem(ctrl, lineItem).then(function() {
                    $rootScope.$broadcast('cartItemsChanged');
                    return ctrl.reloadCart();
                });
            });
        };

        this.getAvailShippingMethods = function(shipment) {
            return cartApi.getAvailableShippingMethods(ctrl, shipment.id).then(function(response) {
                return response.data;
            });
        };

		this.getCountryRegions = function (country) {
			return cartApi.getCountryRegions(ctrl, country.code3).then(function (response) {
				return response.data;
			});
		};

		this.applyCoupon = function (coupon) {
			return cartApi.addCouponNew(ctrl, coupon.code).then(function (response) {
				ctrl.reloadCart();
				return response.data;
			});
		};

		this.removeCoupon = function (coupon) {
			return cartApi.removeCouponNew(ctrl, coupon ? coupon.code : undefined).then(function () {
				return ctrl.reloadCart();
			});
			};


		this.validateCoupon = function (coupon) {
			coupon.processing = true;
			return cartApi.validateCouponNew(ctrl, coupon).then(function (result) {
				coupon.processing = false;
				return angular.extend(coupon, result.data);
			}, function () {
				coupon.processing = false;
			});
		}

        this.removeLineItem = function(lineItemId) {
            var lineItem = _.find(this.items, function(i) { return i.id === lineItemId; });
            if (!lineItem || this.cartIsUpdating) {
                return;
            }
            this.cartIsUpdating = true;

            cartApi.removeLineItem(ctrl, lineItemId).then(function(response) {
                ctrl.reloadCart();
                $rootScope.$broadcast('cartItemsChanged');
            });

            this.cartIsUpdating = false;
        };

        this.changeLineItemQuantity = function(lineItemId, quantity) {
            var lineItem = _.find(this.items, function(i) { return i.id === lineItemId; });
            if (!lineItem || quantity < 1 || this.cartIsUpdating) {
                return;
            }
            var initialQuantity = lineItem.quantity;
            lineItem.quantity = quantity;
            $timeout.cancel(timer);

            timer = $timeout(function () {
                    ctrl.cartIsUpdating = true;
                    cartApi.changeLineItem(ctrl, lineItemId, quantity).then(function(response) {
                            ctrl.reloadCart();
                            $rootScope.$broadcast('cartItemsChanged');
                        },
                        function(response) {
                            lineItem.quantity = initialQuantity;
                            ctrl.cartIsUpdating = false;
                        });
                },
                300);
        };
	

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
            return cartApi.createOrder(ctrl).then(function(response) {
                let order = response.data;
                if (order && order.inPayments.length) {
                    let paymentId = order.inPayments[0].id;
                    cartApi.processPayment(ctrl, order.id, paymentId);
                }
                return response;
            });
		};

		this.removeCart = function () {
          return cartApi.removeCart(ctrl).then(function() {
				$rootScope.$broadcast('cartItemsChanged');
			});
		};

		this.clearCart = function () {
            return wrapLoading(function () {
                return cartApi.clearCart(ctrl).then(function () {
                    ctrl.items = [];
                    ctrl.getCartItemsCount();
                    ctrl.reloadCart();
					$rootScope.$broadcast('cartItemsChanged');
				});
			});
		};

		this.getAvailPaymentMethods = function () {
			return cartApi.getAvailablePaymentMethods(ctrl).then(function (response) {
				return response.data;
			});
		};

		this.checkout = function () {
			if (this.checkout) {
				this.checkout.show();
			}
		};

		this.openCart = function () {
			$rootScope.$broadcast('needOpenCart');
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

        this.getCartItemsCount = function() {
            if (ctrl.id && ctrl.items) {
                var itemsQuantity = 0;
                for (var index in ctrl.items) {
                    itemsQuantity += ctrl.items[index].quantity;
                }
                ctrl.cartItemsCount = itemsQuantity;
            } else {
                ctrl.cartItemsCount = 0;
            }
        };

		this.initializeUser = function(){
			authService.fillAuthData().then(function() {
				ctrl.reloadCart();
			});
		};

		this.initializeUser();

		this.getCartItemsCount();
	}]
});

cartModule.directive('vcBuyButton', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            element.bind('click', function () {
                console.log('clicked');
                console.log(attrs);
                scope.addLineItem({
                    name: attrs.itemName,
                    quantity: attrs.quantity,
                    listPrice: attrs.listPrice,
                    currency: attrs.currency,
                    productId: attrs.productId,
                    sku: attrs.sku,
                    catalogId: attrs.catalogId,
                    imageUrl: attrs.imageUrl
                });
            });
        }
    }
});

cartModule.controller('virtoCommerce.cartModule.cartController', ['$scope', '$uibModal', 'virtoCommerce.cartModule.carts', function ($scope, $uibModal, carts) {

	$scope.carts = carts;

	$scope.$on('needOpenCart', function (event, data) {
		$scope.openCart();
	});


	$scope.openCheckout = function (cart) {
		
		$scope.cart =  !cart ? $scope.carts['default'] : cart;

		$uibModal.open({
			animation: true,
			templateUrl: 'checkout-modal.tpl.html',
			controller: 'virtoCommerce.cartModule.checkoutController',
			windowClass: 'cart-modal-window',
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

		$scope.cart.addLineItem($scope.lineItem).then(function () {
			$uibModal.open({
				animation: true,
				templateUrl: 'recently-added-cart-item-dialog.tpl.html',
				controller: 'virtoCommerce.cartModule.addItemViewController',
				size:'lg',
				resolve: { 
					cart : function () {
						return $scope.cart;
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
		$scope.cart.reloadCart().then(function () {
			$uibModal.open({
				animation: true,
				templateUrl: 'shoppingCart.tpl.html',
				controller: 'virtoCommerce.cartModule.cartViewController',
				size: 'lg',
				resolve: { 
					cart : function () {
						return $scope.cart;
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
	$scope.callback = callback;

	$scope.cancel = function () {
		$uibModalInstance.dismiss('cancel');
	};

	$scope.ok = function () {
		$scope.callback();
		$uibModalInstance.dismiss('cancel');
	};
}]);

cartModule.controller('virtoCommerce.cartModule.checkoutController', ['$scope', '$uibModalInstance', 'cart' , function ($scope, $uibModalInstance, cart) {
	$scope.cart = cart;
	$scope.cancel = function () {
		$uibModalInstance.dismiss('cancel');
    };

    $scope.$on('userLoggedIn', function (event, data) {
		$scope.cart.reloadCart();
        $scope.cancel();
    });

    $scope.$on('userLoggedOut', function (event, data) {
        $scope.cart.reloadCart();
        $scope.cancel();
    });

}]);

cartModule.controller('virtoCommerce.cartModule.clearCartPopUpController', ['$scope', '$uibModalInstance', function ($scope, $uibModalInstance) {
	$scope.ok = function(){
		$uibModalInstance.close(true);
	};

	$scope.cancel = function () {
		$uibModalInstance.close(false);
	};
}]);

cartModule.controller('virtoCommerce.cartModule.cartViewController', ['$scope', '$uibModalInstance', 'cart', 'callback', '$uibModal', function ($scope, $uibModalInstance, cart, callback, $uibModal) {

	$scope.cart = cart;
	$scope.callback = callback;

	$scope.cancel = function () {
		$uibModalInstance.dismiss('cancel');
	};

	$scope.clearCart = function () {
		var modalInstance =  $uibModal.open({
			animation: true,
			templateUrl: 'clear-cart-modal.tpl.html',
			controller: 'virtoCommerce.cartModule.clearCartPopUpController',
		});

		modalInstance.result.then(function (shouldClear) {
			if(shouldClear){
                cart.clearCart(cart);
                $uibModalInstance.dismiss('cancel');
			}
        });
	};

    $scope.removeLineItem = function(lineItemId) {
        cart.removeLineItem(lineItemId);
    };

    $scope.changeLineItemQuantity = function(lineItemId, quantity) {
        cart.changeLineItemQuantity(lineItemId, quantity);
    };

	$scope.ok = function(){
        $scope.callback();
		$uibModalInstance.dismiss('cancel');
	};
}]);
