var storefrontApp = angular.module('storefrontApp');

storefrontApp.component('vcCheckoutCart', {
	templateUrl: "checkout-cart.tpl.html",
	require: {
		checkoutStep: '^vcCheckoutStep'
	},
	bindings: {
		cart: '='
	},
	controller: [function () {
		var ctrl = this;

		this.$onInit = function () {
			ctrl.checkoutStep.addComponent(this);
		};

		this.$onDestroy = function () {
			ctrl.checkoutStep.removeComponent(this);
		};
	
		ctrl.validate = function () {
			return true;
		}

	}]
});
