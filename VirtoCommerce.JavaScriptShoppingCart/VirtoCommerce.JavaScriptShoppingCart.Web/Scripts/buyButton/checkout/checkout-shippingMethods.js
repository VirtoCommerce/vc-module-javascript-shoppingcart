﻿var storefrontApp = angular.module('storefrontApp');

storefrontApp.component('vcCheckoutShippingMethods', {
	templateUrl: "checkout-shippingMethods.tpl.html",
	require: {
		checkoutStep: '^vcCheckoutStep'
	},
	bindings: {
		shipment: '=',
		getAvailShippingMethods: '&'
	},
	controller: [function () {

		var ctrl = this;
		ctrl.availShippingMethods = [];
		ctrl.selectedOption = {};
		this.$onInit = function () {
			ctrl.checkoutStep.addComponent(this);

			innerGetAvailShippingMethods().then(function (availMethods) {
				ctrl.availShippingMethods = availMethods;
			});
		};

		this.$onDestroy = function () {
			ctrl.checkoutStep.removeComponent(this);
		};

		function innerGetAvailShippingMethods() {
			return ctrl.getAvailShippingMethods(ctrl.shipment).then(function (response) {
				var availMethods = [];
				_.each(response.data, function (method) {
					var existMethod = _.find(availMethods, function (x) {
						return x.shipmentMethodCode = method.shipmentMethodCode;
					});
					if (!existMethod) {
						existMethod = {
							name: method.name,
							options: []
						};
						availMethods.push(existMethod);
					}
					method.id = getMethodId(method.shipmentMethodCode, method.optionName);
					method.name = method.optionName ? method.optionName : method.name;
					existMethod.options.push(method);

					if (ctrl.shipment.shipmentMethodCode == method.shipmentMethodCode && ctrl.shipment.shipmentMethodOption == method.optionName) {
						ctrl.selectedOption = method;
					}
				});
				return availMethods;
			});
		}

		function getMethodId(code, option) {
			var retVal = code;
			if (option) {
				retVal += ':' + option;
			}
			return retVal;
		}

		ctrl.selectMethodOption = function (option) {
			ctrl.shipment.shipmentMethodCode = option.shipmentMethodCode;
			ctrl.shipment.shipmentMethodOption = option.optionName;
		};

		ctrl.validate = function () {
			ctrl.form.$setSubmitted();
			return !ctrl.form.$invalid;
		}
	}]
});
