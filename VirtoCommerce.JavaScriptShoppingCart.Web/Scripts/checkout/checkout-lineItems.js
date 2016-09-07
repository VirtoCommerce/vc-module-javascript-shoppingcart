var cartModule = angular.module('virtoCommerce.cartModule');

cartModule.component('vcCheckoutLineItems', {
	templateUrl: "checkout-lineItems.tpl.html",
	bindings: {
		items: '=',
		currencySymbol: '<',
		showPricesWithTaxes: '='
	},
	controller: [function () {
		var ctrl = this;	
	}]
});
