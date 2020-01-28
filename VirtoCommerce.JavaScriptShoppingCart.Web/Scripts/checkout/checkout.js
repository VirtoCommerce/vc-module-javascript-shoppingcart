//Call this to register our module to main application
var cartModule = angular.module('virtoCommerce.cartModule');

cartModule.component('vcCheckout', {
	templateUrl: "checkout.tpl.html",
	bindings: {
		name: '=',
		cart: '=',
		showPricesWithTax: '@'
	},
    controller: ['$scope', '$rootScope', '$window', 'virtoCommerce.cartModule.api', '$uibModal', 'virtoCommerce.cartModule.authService', function ($scope, $rootScope, $window, cartService, $uibModal, authService) {
        var ctrl = this;

        $scope.$on('loginStatusChanged', function (event, authContext) {
            ctrl.isAuthenticated = authContext.isAuthenticated;
            ctrl.userName = authContext.userLogin;
        });

		this.checkout = {
			wizard: {},
			email: null,
			paymentMethod: {},
			shipment: {},
			payment: {},
			order: {},
			coupon: {},
			loading: false,
			isValid: false,
			isFinished: false
		};

		this.validateCheckout = function (checkout) {
			checkout.isValid = checkout.payment && angular.isDefined(checkout.payment.paymentGatewayCode);
			if (checkout.isValid && !checkout.billingAddressEqualsShipping) {
				checkout.isValid = checkout.payment.billingAddress && angular.isDefined(checkout.payment.billingAddress);
			}
			if (ctrl.cart && ctrl.cart.hasPhysicalProducts) {
				if (checkout.isValid) {
					checkout.isValid = checkout.shipment && angular.isDefined(checkout.shipment.shipmentMethodCode);
				}
				if (checkout.isValid && ctrl.cart.hasPhysicalProducts) {
					checkout.isValid = checkout.shipment.deliveryAddress && angular.isDefined(checkout.shipment.deliveryAddress);
				}
			}
		};

		this.reloadCart = function () {
			return ctrl.cart.reloadCart().then(function (cart) {
				cart.hasPhysicalProducts = true;
				ctrl.cart = cart;
				//ctrl.cart.coupon = cart.coupon || ctrl.checkout.coupon;
				if (cart.payments.length) {
					ctrl.checkout.payment = cart.payments[0];
					ctrl.checkout.paymentMethod.code = cart.payments[0].paymentGatewayCode;
				}
				if (cart.shipments.length) {
					ctrl.checkout.shipment = cart.shipments[0];
					if (ctrl.checkout.shipment.deliveryAddress) {
						ctrl.checkout.email = ctrl.checkout.shipment.deliveryAddress.email;
					}
				}
				ctrl.checkout.billingAddressEqualsShipping = !angular.isDefined(ctrl.checkout.payment.billingAddress);
				if (!cart.hasPhysicalProducts) {
					ctrl.checkout.billingAddressEqualsShipping = false;
				}
				ctrl.validateCheckout(ctrl.checkout);
				return cart;
			});
		};

		this.selectPaymentMethod = function (paymentMethod) {
			ctrl.checkout.payment.paymentGatewayCode = paymentMethod.code;
			ctrl.validateCheckout(ctrl.checkout);
		};

        this.selectShippingMethod = function (shippingMethod) {
            angular.merge(ctrl.checkout.shipment, shippingMethod);
			//if (shippingMethod) {
			//	ctrl.checkout.shipment.shipmentMethodCode = shippingMethod.shippingMethod.code;
			//	ctrl.checkout.shipment.shipmentMethodOption = shippingMethod.optionName;
			//} else {
			//	ctrl.checkout.shipment.shipmentMethodCode = undefined;
			//	ctrl.checkout.shipment.shipmentMethodOption = undefined;
			//}
            ctrl.cart.addOrUpdateShipment(ctrl.checkout.shipment);
            ctrl.validateCheckout(ctrl.checkout);
		};

        ctrl.createOrder = function() {
            ctrl.checkout.loading = true;
            updatePayment(ctrl.checkout.payment).then(function() {
                return ctrl.cart.createOrder({ bancCardInfo: ctrl.checkout.bankCardInfo });
            }).then(function (response) {
                $rootScope.$broadcast('orderCreated');
                ctrl.checkout.loading = false;
                var order = response.data;
                if (order) {
                    ctrl.checkout.order = order;
                    ctrl.cart.removeCart().then(function () {
                        ctrl.cart.items = [];
                        ctrl.cart.reloadCart();
                        ctrl.checkout.isFinished = true;
                    });
                }
            });
        };

		function updatePayment(payment) {
			if (ctrl.checkout.billingAddressEqualsShipping) {
				payment.billingAddress = undefined;
			}

			if (payment.billingAddress) {
				payment.billingAddress.email = ctrl.checkout.email;
				payment.billingAddress.type = 'Billing';
			}

			return ctrl.cart.addOrUpdatePayment(payment);
		}

		ctrl.initialize = function () {
            ctrl.reloadCart().then(function (cart) {
                ctrl.isAuthenticated = authService.isAuthenticated;
                ctrl.userName = authService.userLogin;
				ctrl.checkout.wizard.goToStep('shipping-address');
			});
        };


        ctrl.login = function() {
            $uibModal.open({
                animation: true,
                templateUrl: 'login-modal.tpl.html',
                controller: 'virtoCommerce.cartModule.logInViewController',
                size:'s',
                resolve: {
                    cart: function () {
                        return ctrl;
                    }
                }
            });
        };

        ctrl.logout = function () {
            authService.logout();
        };
    }]
});

