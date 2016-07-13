﻿var storefrontApp = angular.module('storefrontApp');

storefrontApp.component('vcCheckoutEmail', {
	templateUrl: "checkout-email.tpl.html",
	require: {
		checkoutStep: '^vcCheckoutStep'
	},
	bindings: {
		email: '='
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
			ctrl.form.$setSubmitted();
			return !ctrl.form.$invalid;
		}

	}]
});
