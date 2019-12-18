﻿cartModule.service('virtoCommerce.cartModule.authDataStorage', ['localStorageService', function (localStorageService) {
        var service = {
            storeAuthData: function(dataObject) {
                localStorageService.set('authenticationData', dataObject);
            },
            getStoredData: function() {
                return localStorageService.get('authenticationData');
            },
            clearStoredData: function() {
                localStorageService.remove('authenticationData');
            },
            setPlatformKey: function(apiKey) {
                localStorageService.set('platformKey', apiKey);
            },
            setPlatformUrl: function (url) {
                localStorageService.set('platformUrl', url);
            },
            getPlatformKey: function() {
                return localStorageService.get('platformKey');
            },
            getPlatformUrl: function() {
                return localStorageService.get('platformUrl');
            }
        };

        return service;
}]);

