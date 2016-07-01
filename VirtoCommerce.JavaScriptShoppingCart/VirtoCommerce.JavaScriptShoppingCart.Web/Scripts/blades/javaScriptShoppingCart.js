angular.module('virtoCommerce.javaScriptShoppingCartModule')
.controller('virtoCommerce.javaScriptShoppingCartModule.javaScriptShoppingCartController', ['$scope', 'platformWebApp.bladeNavigationService', 'platformWebApp.dialogService', 'platformWebApp.settings', function ($scope, bladeNavigationService, dialogService, settings) {
    var blade = $scope.blade;
	blade.isLoading = false;
}]);