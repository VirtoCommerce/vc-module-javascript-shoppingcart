angular.module('virtoCommerce.cartModule')
    .service('virtoCommerce.cartModule.api', ['$http', 'virtoCommerce.cartModule.authDataStorage', 'virtoCommerce.cartModule.authService', function ($http, authDataStorage, authService) {
        //var cartsPath = 'api/carts/';
        var customerOrdersPath = 'api/order/customerOrders/';
        var jsCartPath = 'jscart/api/carts/';
        var separator = '/'; 

        //function getUrl(cart) {
        //    var userId = authService.userId;
        //    var memberId = authService.memberId;
        //    var url = cart.apiUrl + cartsPath + cart.storeId + separator + (memberId || userId) + separator + cart.name + separator + cart.currency + separator + cart.culture;
        //    return url;
        //}

        function getJsCartApiBaseUrl(cart) {
            return cart.apiUrl + jsCartPath + cart.currencyCode + separator + cart.culture;
        }

        function getJsCartApiFullUrlWithCartSegment(cart) {
            return getJsCartApiBaseUrl(cart) + separator + cart.id;
        }

        //function getCartUrl(cart) {
        //    return cart.apiUrl + cartsPath + cart.id;
        //}

        return {
            //getCart: function (cart) {
            //    var dt = new Date().getTime();
            //    if (authService.isAuthenticated) {
            //        return $http.get(getUrl(cart) + '/current?t=' + dt);
            //    }

            //    return $http.get(getUrl(cart) + '/current?api_key=' + cart.apiKey + '&t=' + dt);
            //},


            //getCartItemsCount: function (cart) {
            //    var dt = new Date().getTime();
            //    var cartUrl = getCartUrl(cart);
            //    if (authService.isAuthenticated) {
            //        return $http.get(cartUrl + '/itemscount?t=' + dt);
            //    }

            //    return $http.get(cartUrl + '/itemscount?api_key=' + cart.apiKey + '&t=' + dt);
            //},

            //addLineItem: function (cart, lineItem) {
            //    var jsCartUrl = getJsCartUrl(cart);
            //    var url = jsCartUrl + separator + cart.id + '/item' + '?apiKey=' + cart.apiKey;
            //    return $http.post(url, lineItem);
            //},

            // remove. It does not used
            //addProduct: function (cart, productId, quantity) {
            //    var cartUrl = getCartUrl(cart);

            //    if (authService.isAuthenticated) {
            //        return $http.post(cartUrl + '/product?productId=' + productId + '&quantity=' + quantity, lineItem);
            //    }

            //    return $http.post(cartUrl + '/product?api_key=' + cart.apiKey + '&productId=' + productId + '&quantity=' + quantity, lineItem);
            //},

            //changeLineItem: function (cart, lineItemId, quantity) {
            //    var cartUrl = getCartUrl(cart);
            //    var url = cartUrl + '/items?lineItemId=' + lineItemId + '&quantity=' + quantity;

            //    if (authService.isAuthenticated) {
            //        return $http.put(url);
            //    }

            //    return $http.put(url + '&api_key=' + cart.apiKey);
            //},

            //removeLineItem: function (cart, lineItemId) {
            //    var cartUrl = getCartUrl(cart);
            //    var url = cartUrl + '/items/' + lineItemId;
            //    if (authService.isAuthenticated) {
            //        return $http.delete(url);
            //    }

            //    return $http.delete(url + '?api_key=' + cart.apiKey);
            //},
            
            //clearCart: function (cart) {
            //    var cartUrl = getCartUrl(cart);

            //    if (authService.isAuthenticated) {
            //        return $http.delete(cartUrl + '/items');
            //    }

            //    return $http.delete(cartUrl + '/items?api_key=' + cart.apiKey);
            //},

            //addCoupon: function (cart, couponCode) {
            //    var cartUrl = getCartUrl(cart);

            //    return $http.post(cartUrl + '/coupons/' + couponCode + '?api_key=' + cart.apiKey);
            //},

            //removeCoupon: function (cart) {
            //    var cartUrl = getCartUrl(cart);

            //    return $http.delete(cartUrl + '/coupons?api_key=' + cart.apiKey);
            //},

            //addOrUpdateShipment: function (cart, shipment) {
            //    var cartUrl = getCartUrl(cart);

            //    if (authService.isAuthenticated) {
            //        return $http.post(cartUrl + '/shipments', shipment);
            //    }

            //    return $http.post(cartUrl + '/shipments?api_key=' + cart.apiKey, shipment);
            //},

            //addOrUpdatePayment: function (cart, payment) {
            //    var cartUrl = getCartUrl(cart);

            //    if (authService.isAuthenticated) {
            //        return $http.post(cartUrl + '/payments', payment);
            //    }

            //    return $http.post(cartUrl + '/payments?api_key=' + cart.apiKey, payment);
            //},

            //getAvailableShippingMethods: function (cart) {
            //    var dt = new Date().getTime();
            //    var cartUrl = getCartUrl(cart);

            //    if (authService.isAuthenticated) {
            //        return $http.get(cartUrl + '/availshippingrates?t=' + dt);
            //    }

            //    return $http.get(cartUrl + '/availshippingrates?api_key=' + cart.apiKey + '&t=' + dt);
            //},

            //getAvailablePaymentMethods: function (cart) {
            //    var cartUrl = getCartUrl(cart);

            //    if (authService.isAuthenticated) {
            //        return $http.get(cartUrl + '/availpaymentmethods');
            //    }

            //    return $http.get(cartUrl + '/availpaymentmethods?api_key=' + cart.apiKey);
            //},

            //removeCart: function (cart) {
            //    if (authService.isAuthenticated) {
            //        return $http.delete(cart.apiUrl + 'api/carts?ids=' + cart.id);
            //    }

            //    return $http.delete(cart.apiUrl + 'api/carts?api_key=' + cart.apiKey + '&ids=' + cart.id);
            //},

            createOrder: function (cart) {
                var url = cart.apiUrl + customerOrdersPath + cart.id;
                if (authService.isAuthenticated) {
                    return $http.post(url, cart);
                }

                return $http.post(url + '?api_key=' + cart.apiKey, cart);
            },

            processPayment: function (cart , orderId, paymentId) {
                var url = cart.apiUrl + customerOrdersPath + orderId + '/processPayment/' + paymentId;
                if (authService.isAuthenticated) {
                    return $http.post(url);
                }

                return $http.post(url + '?api_key=' + cart.apiKey);
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
