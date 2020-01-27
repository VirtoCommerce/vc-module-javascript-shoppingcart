angular.module('virtoCommerce.cartModule')
    .service('virtoCommerce.cartModule.api', ['$http', 'virtoCommerce.cartModule.authDataStorage', 'virtoCommerce.cartModule.authService', function ($http, authDataStorage, authService) {        
        var jsCartOrdersApiPath = 'jscart/api/orders/';
        var jsCartApiPath = 'jscart/api/carts/';
        var separator = '/'; 
     
        
        function getJsCartApiBaseUrl(cart) {
            return cart.apiUrl + jsCartApiPath + cart.currencyCode + separator + cart.culture;
        }

        function getJsCartApiFullUrlWithCartSegment(cart) {
            return getJsCartApiBaseUrl(cart) + separator + cart.id;
        }        

        return {           

            createOrder: function (cart) {
                var url = cart.apiUrl + jsCartOrdersApiPath + cart.id;               
                return $http.post(url, cart);
            },

            processPayment: function (cart , orderId, paymentId) {
                var url = cart.apiUrl + jsCartOrdersApiPath + orderId + '/processPayment/' + paymentId;                
                return $http.post(url);                                
            },

            // using jscart API
            getOrCreateCart: function (cart) {
                var dt = new Date().getTime();
                var url = getJsCartApiBaseUrl(cart) + separator + cart.storeId + separator + cart.userId + separator + cart.name + '/current?t=' + dt;
                return $http.get(url);
            },

            getCartItemsCount: function (cart) {
                var dt = new Date().getTime();
                var url = getJsCartApiFullUrlWithCartSegment(cart) + '/itemscount?t=' + dt;                
                return $http.get(url);                
            },

            addLineItem: function (cart, lineItem) {
                var jsCartUrl = getJsCartApiFullUrlWithCartSegment(cart);
                var url = jsCartUrl + '/items';
                return $http.post(url, lineItem);
            },

            changeLineItem: function (cart, lineItemId, quantity) {
                var jsCartUrl = getJsCartApiFullUrlWithCartSegment(cart);
                var url = jsCartUrl + '/items';
                return $http.put(url, { lineItemId, quantity});                
            },

            removeLineItem: function (cart, lineItemId) {
                var jsCartUrl = getJsCartApiFullUrlWithCartSegment(cart);
                var url = jsCartUrl + '/items/' + lineItemId;               
                return $http.delete(url);               
            },

            clearCart: function (cart) {
                var jsCartUrl = getJsCartApiFullUrlWithCartSegment(cart);
                return $http.delete(jsCartUrl + '/items');                    
            },

            removeCart: function (cart) {
                return $http.delete(getJsCartApiFullUrlWithCartSegment(cart));               
            },

            //----
            addCoupon: function (cart, couponCode) {
                var url = getJsCartApiFullUrlWithCartSegment(cart) + '/coupons/' + couponCode;
                return $http.post(url);
            },

            removeCoupon: function (cart, couponCode) {
                var url = getJsCartApiFullUrlWithCartSegment(cart) + '/coupons' + (angular.isDefined(couponCode) ? separator + couponCode : '');
                return $http.delete(url);
            },

            validateCoupon: function (cart, coupon) {
                var url = getJsCartApiFullUrlWithCartSegment(cart) + '/coupons/validate';
                return $http.delete(url, coupon);
            },

            //---- shiping and pays methods
            addOrUpdateShipment: function (cart, shipment) {
                var cartUrl = getJsCartApiFullUrlWithCartSegment(cart);
                return $http.post(cartUrl + '/shipments', shipment);
            },

            addOrUpdatePayment: function (cart, payment) {
                var cartUrl = getJsCartApiFullUrlWithCartSegment(cart);                
                return $http.post(cartUrl + '/payments', payment);                
            },

            getAvailableShippingMethods: function (cart) {
                var dt = new Date().getTime();
                var cartUrl = getJsCartApiFullUrlWithCartSegment(cart);                
                return $http.get(cartUrl + '/shippingmethods?t=' + dt);
            },

            getAvailablePaymentMethods: function (cart) {
                var cartUrl = getJsCartApiFullUrlWithCartSegment(cart);
                
                return $http.get(cartUrl + '/paymentmethods');
            }

            // checkout process            

            //createOrder: function (cart) {
            //    var url = cart.apiUrl + customerOrdersPath + cart.id;
            //    if (authService.isAuthenticated) {
            //        return $http.post(url, cart);
            //    }

            //    return $http.post(url + '?api_key=' + cart.apiKey, cart);
            //},

            //processPayment: function (cart, orderId, paymentId) {
            //    var url = cart.apiUrl + customerOrdersPath + orderId + '/processPayment/' + paymentId;
            //    if (authService.isAuthenticated) {
            //        return $http.post(url);
            //    }

            //    return $http.post(url + '?api_key=' + cart.apiKey);
            //},

        };
    }]);
