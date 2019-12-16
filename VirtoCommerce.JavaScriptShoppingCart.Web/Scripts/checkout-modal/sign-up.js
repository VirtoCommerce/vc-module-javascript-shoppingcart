var cartModule = angular.module('virtoCommerce.cartModule');

cartModule.controller('virtoCommerce.cartModule.signUpViewController', ['$scope', '$uibModalInstance', 'cart', function ($scope, $uibModalInstance, cart) {

	$scope.cart = cart;
	$scope.customer = {};
	$scope.signUpMode = false;

	$scope.cancel = function () {
		$uibModalInstance.dismiss('cancel');
	};

	$scope.signUp = function () {

	};
}]);