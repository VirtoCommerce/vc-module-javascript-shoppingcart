var cartModule = angular.module("virtoCommerce.cartModule");

cartModule.controller("virtoCommerce.cartModule.signUpViewController",
    [
        "$scope", "$uibModalInstance", "virtoCommerce.cartModule.authService", "cart",
        function ($scope, $uibModalInstance, authService, cart) {

            $scope.cart = cart;
            $scope.customer = {};
            $scope.signUpMode = false;
            $scope.errors = [];
            $scope.minPasswordLength = 0;

            $scope.cancel = () => {
                $uibModalInstance.dismiss("cancel");
            };

            $scope.signUp = (isValid) => {
                if (isValid) {
                    $scope.customer.userName = $scope.customer.email;
                    authService.validatePassword(JSON.stringify($scope.customer.password)).then((passwordValidationResult) => {
                        if (passwordValidationResult.data.passwordIsValid) {
                            authService.signUp($scope.customer).then(
                                () => authService.login($scope.customer.userName, $scope.customer.password, false).then(
                                    () => {
                                        $uibModalInstance.dismiss("cancel")
                                    },
                                    (error) => {
                                        showError(error);
                                    }),
                                (error) => {
                                    showError(error);
                                });
                        } else {
                            $scope.errors = [];
                            $scope.minPasswordLength = passwordValidationResult.data.minPasswordLength;
                            var filteredKeys = _.filter(Object.keys(passwordValidationResult.data),
                                function (key) {
                                    return !key.startsWith('$') && passwordValidationResult.data[key] === true;
                                });

                            filteredKeys.forEach(function (key) {
                                var resourceName = 'customer.register.validations.' + key;
                                $scope.errors.push(resourceName);
                            });
                        }
                    });
                }
            };


            function showError(error) {
                if (angular.isDefined(error.status)) {
                    if (error.status === 400 || error.status === 401 && error.data.errors.length) {
                        $scope.error = error.data.errors;
                    } else {
                        $scope.error = `Registration user error (code: ${error.status}).`;
                    }
                } else {
                    $scope.error = `Registration user error ${error}`;
                }
            }
        }
    ]);
