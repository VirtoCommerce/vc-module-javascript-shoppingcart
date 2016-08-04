var storefrontApp = angular.module('storefrontApp');

storefrontApp.component('vcCheckoutLineItems', {
	templateUrl: "checkout-lineItems.tpl.html",
	bindings: {
		items: '=',
	},
	controller: [function () {
		var ctrl = this;	
	}]
});
