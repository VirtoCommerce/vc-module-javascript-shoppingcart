var cartModule = angular.module("virtoCommerce.cartModule");

cartModule.controller("virtoCommerce.cartModule.signUpViewController",
    [
        "$scope", "$uibModalInstance", "virtoCommerce.cartModule.authService", "cart",
        function($scope, $uibModalInstance, authService, cart) {

            $scope.cart = cart;
            $scope.customer = {};
            $scope.signUpMode = false;

            $scope.cancel = () => {
                $uibModalInstance.dismiss("cancel");
            };

            $scope.signUp = async () => {
                $scope.customer.userName = $scope.customer.email;
                try {
                    await authService.signUp($scope.customer);
                    $uibModalInstance.dismiss("cancel");
                } catch (error) {
                    if (angular.isDefined(error.status)) {
                        if (error.status === 400 || error.status === 401 && error.data.errors.length) {
                            $scope.error = error.data.errors[0];
                        } else {
                            $scope.error = `Registration user error (code: ${error.status}).`;
                        }
                    } else {
                        $scope.error = `Registration user error ${error}`;
                    }
                }
            };
        }
    ]);