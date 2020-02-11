var cartModule = angular.module('virtoCommerce.cartModule');
cartModule.component('vcCheckoutCoupon', {
    templateUrl: 'checkout-coupon.tpl.html',
    bindings: {
        coupons: '=',
        onApplyCoupon: '&',
        onRemoveCoupon: '&',
        onValidateCoupon: '&'
    },
    controller: ['$scope', function ($scope) {
        var ctrl = this;

        ctrl.$onInit = function () {
            ctrl.coupon = { code: '', isValid: true };
        };
               
        ctrl.applyCoupon = function (coupon) {
            ctrl.onValidateCoupon(coupon).then(function (result) {				                
                if (result.appliedSuccessfully) {
                    ctrl.onApplyCoupon({ coupon: ctrl.coupon });
                    ctrl.coupon = {
                            code: '', isValid: true
                        };
                    } else {
                        ctrl.coupon.isValid = false;
                    }                                    
            });
        };
    }]
});
