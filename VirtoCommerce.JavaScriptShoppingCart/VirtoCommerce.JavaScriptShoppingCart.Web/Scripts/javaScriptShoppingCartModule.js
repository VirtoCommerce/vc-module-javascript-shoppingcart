var moduleName = "virtoCommerce.javaScriptShoppingCartModule";

if (AppDependencies != undefined) {
	AppDependencies.push(moduleName);
}

angular.module(moduleName, [
		'ngSanitize'
	])
	.run(
	[
		'$rootScope', 'platformWebApp.mainMenuService', 'platformWebApp.widgetService', '$state', function($rootScope, mainMenuService, widgetService, $state) {
			widgetService.registerWidget({
				controller: 'virtoCommerce.javaScriptShoppingCartModule.javaScriptShoppingCartWidgetController',
				template: 'Modules/$(VirtoCommerce.JavaScriptShoppingCart)/Scripts/widgets/javaScriptShoppingCartWidget.tpl.html'
			}, 'storeDetail');
		}
	]);
