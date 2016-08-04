var storefrontApp = angular.module('storefrontApp');

storefrontApp.component('vcCheckoutTotals', {
	templateUrl: "checkout-totals.tpl.html",
	bindings: {
		cart: '=',
		displayOnlyTotal: '<'
	},
	controller: [function () {
		var ctrl = this;
	
	}]
});
