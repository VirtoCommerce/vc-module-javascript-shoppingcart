angular.module('virtoCommerce.javaScriptShoppingCartModule')
.controller('virtoCommerce.javaScriptShoppingCartModule.javaScriptShoppingCartWidgetController',
    ['$scope', 'platformWebApp.bladeNavigationService', function ($scope, bladeNavigationService) {

    var blade = $scope.widget.blade;

	$scope.openBlade = function () {
		var newBlade = {
		    id: "storeJavaScriptShoppingCartBlade",
			store: blade.currentEntity,
			title: blade.title,
			subtitle: "JavaScript ShoppingCart",
			controller: 'virtoCommerce.javaScriptShoppingCartModule.javaScriptShoppingCartController',
			template: 'Modules/$(VirtoCommerce.JavaScriptShoppingCart)/Scripts/blades/javaScriptShoppingCart.tpl.html'
		};
		bladeNavigationService.showBlade(newBlade, blade);
	};
}]);