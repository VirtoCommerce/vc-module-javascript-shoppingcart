var cartModule = angular.module('virtoCommerce.cartModule');

cartModule.controller('virtoCommerce.cartModule.logInViewController', ['$scope', '$uibModalInstance', '$uibModal', 'virtoCommerce.cartModule.authService', 'cart', function ($scope, $uibModalInstance, $uibModal, authService, cart) {

	$scope.cart = cart;
	$scope.authError = null;
	$scope.authReason = false;
	$scope.loginProgress = false;


	$scope.cancel = function () {
		$uibModalInstance.dismiss('cancel');
	};

	$scope.ok = function () {
		// Clear any previous security errors
		$scope.authError = null;
		$scope.loginProgress = true;
		// Try to login
		authService.login($scope.customerEmail, $scope.customerPassword, false).then(
			function (loggedIn) {
				$scope.loginProgress = false;
				if (!loggedIn) {
					$scope.authError = 'invalidCredentials';
				} else {
					$uibModalInstance.dismiss('cancel');
				}
			},
			function (x) {
				$scope.loginProgress = false;
				if (angular.isDefined(x.status)) {
					if (x.status == 400 || x.status == 401) {
						$scope.authError = 'The login or password is incorrect.';
					} else {
						$scope.authError = 'Authentication error (code: ' + x.status + ').';
					}
				} else {
					$scope.authError = 'Authentication error ' + x;
				}
			});
	};


	$scope.resetPassword = function () {

	};

	$scope.showSignUpPopUp = function () {
		$uibModalInstance.dismiss('cancel');
		$uibModal.open({
			animation: true,
			templateUrl: 'sign-up-modal.tpl.html',
			controller: 'virtoCommerce.cartModule.signUpViewController',
			size: 's',
			resolve: {
				cart: function () {
					return $scope.cart;
				}
			}
		});

	};

	$scope.signUp = function () {

	};
}]);