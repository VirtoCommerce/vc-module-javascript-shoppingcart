var cartModule = angular.module('virtoCommerce.cartModule');

cartModule.controller('virtoCommerce.cartModule.logInViewController', ['$scope', '$rootScope', '$uibModalInstance', '$uibModal', 'virtoCommerce.cartModule.authService', 'cart', function ($scope, $rootScope, $uibModalInstance, $uibModal, authService, cart) {

	$scope.cart = cart;
	$scope.authError = null;
	$scope.authReason = false;
    $scope.loginProgress = false;

    const badRequest = 400;
    const unauthorized = 401;


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
					$rootScope.$broadcast('userLoggedIn');
					$uibModalInstance.dismiss('cancel');
				}
			},
			function (response) {
				$scope.loginProgress = false;
                if (angular.isDefined(response.status)) {
                    if (response.status === badRequest || response.status === unauthorized) {
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
}]);
