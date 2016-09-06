var cartModule = angular.module('virtoCommerce.cartModule');

cartModule.service('virtoCommerce.cartModule.currenciesService', function () {
	this.currencies = [
		{
			"code": "USD",
			"name": "US dollar",
			"isPrimary": true,
			"exchangeRate": 1.0000,
			"symbol": "$"
		},
		{
			"code": "EUR",
			"name": "Euro",
			"isPrimary": false,
			"exchangeRate": 1.2400,
			"symbol": "€"
		},
		{
			"code": "XPT",
			"name": "Points",
			"isPrimary": false,
			"exchangeRate": 1.0000,
			"symbol": "Pt.",
			"customFormatting": "0 Pt."
		}
	];
});