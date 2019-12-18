var cartModule = angular.module('virtoCommerce.cartModule');
cartModule.component('vcCheckoutCoupon', {
	templateUrl: "checkout-coupon.tpl.html",
	bindings: {
		coupon: '=',
		onApplyCoupon: '&',
		onRemoveCoupon: '&',
		onValidateCoupon: '&'
	},
	controller: ['$scope', function ($scope) {
		var ctrl = this;
		ctrl.coupon = { appliedSuccessfully: true };
		$scope.$watch("$ctrl.coupon", function () {
			if (!ctrl.coupon.code) {
				ctrl.coupon.appliedSuccessfully = true;
			}
		}, true);
		ctrl.applyCoupon = function (coupon) {
			ctrl.onValidateCoupon({ coupon: coupon }).then(function (result) {
				if (result.appliedSuccessfully) {
					ctrl.onApplyCoupon({ coupon: coupon });
					ctrl.coupon = {};
				}
			});
		};
	}]
});
