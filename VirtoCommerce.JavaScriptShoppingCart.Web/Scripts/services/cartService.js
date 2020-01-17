angular.module('virtoCommerce.cartModule')
    .service('virtoCommerce.cartModule.api', ['$http', 'virtoCommerce.cartModule.authDataStorage', 'virtoCommerce.cartModule.authService', function ($http, authDataStorage, authService) {
        function getUrl(cart) {
            let userId = authService.userId;
            let memberId = authService.memberId;

            return cart.apiUrl + 'api/carts/' + cart.storeId + '/' + (memberId || userId) + '/' + cart.name + '/' + cart.currency + '/' + cart.culture;
        }

        function getJsCartUrl(cart) {
            return cart.apiUrl + 'jscart/api/carts/' + cart.currency + '/' + cart.culture;
        }

        return {
         //   getCart: function (cart) {
         //       if (authService.isAuthenticated) 
            //        return $http.get(getUrl(cart) + '/current?t=' + new Date().getTime());
            //    return $http.get(getUrl(cart) + '/current?api_key=' + cart.apiKey + '&t=' + new Date().getTime());
            //},
            getCartItemsCount: function (cart) {
                if (authService.isAuthenticated) 
                    return $http.get(cart.apiUrl + 'api/carts/' + cart.id + '/itemscount?t=' + new Date().getTime());
                return $http.get(cart.apiUrl + 'api/carts/' + cart.id + '/itemscount?api_key=' + cart.apiKey + '&t=' + new Date().getTime());
            },
            addLineItem: function (cart, lineItem) {
                if (authService.isAuthenticated) 
                    return $http.post(cart.apiUrl + 'api/carts/' + cart.id + '/items', lineItem);
                return $http.post(cart.apiUrl + 'api/carts/' + cart.id + '/items?api_key=' + cart.apiKey, lineItem);
            },
            addProduct: function (cart, productId, quantity) {
                if (authService.isAuthenticated) 
                    return $http.post(cart.apiUrl + 'api/carts/' + cart.id + '/product?productId=' + productId + '&quantity=' + quantity, lineItem);
                return $http.post(cart.apiUrl + 'api/carts/' + cart.id + '/product?api_key=' + cart.apiKey + '&productId=' + productId + '&quantity=' + quantity, lineItem);
            },
            changeLineItem: function (cart, lineItemId, quantity) {
                if (authService.isAuthenticated) 
                    return $http.put(cart.apiUrl + 'api/carts/' + cart.id + '/items?lineItemId='+lineItemId+'&quantity='+quantity);
                return $http.put(cart.apiUrl + 'api/carts/' + cart.id + '/items?lineItemId='+lineItemId+'&quantity='+quantity+'&api_key=' + cart.apiKey);
            },
            removeLineItem: function (cart, lineItemId) {
                if (authService.isAuthenticated)
                    return $http.delete(cart.apiUrl + 'api/carts/' + cart.id + '/items/' + lineItemId);
                return $http.delete(cart.apiUrl + 'api/carts/' + cart.id + '/items/' + lineItemId + '?api_key=' + cart.apiKey);
            },
            clearCart: function (cart) {
                if (authService.isAuthenticated) 
                    return $http.delete(cart.apiUrl + 'api/carts/' + cart.id + '/items');
                return $http.delete(cart.apiUrl + 'api/carts/' + cart.id + '/items?api_key=' + cart.apiKey);
            },
            addCoupon: function (cart, couponCode) {
                return $http.post(cart.apiUrl + 'api/carts/' + cart.id + '/coupons/' + couponCode + '?api_key=' + cart.apiKey);
            },
            removeCoupon: function (cart) {
                return $http.delete(cart.apiUrl + 'api/carts/' + cart.id + '/coupons?api_key=' + cart.apiKey);
            },
            addOrUpdateShipment: function (cart, shipment) {
                if (authService.isAuthenticated) 
                    return $http.post(cart.apiUrl + 'api/carts/' + cart.id + '/shipments', shipment);
                return $http.post(cart.apiUrl + 'api/carts/' + cart.id + '/shipments?api_key=' + cart.apiKey, shipment);
            },
            addOrUpdatePayment: function (cart, payment) {
                if (authService.isAuthenticated) 
                    return $http.post(cart.apiUrl + 'api/carts/' + cart.id + '/payments', payment);
                return $http.post(cart.apiUrl + 'api/carts/' + cart.id + '/payments?api_key=' + cart.apiKey, payment);
            },
            getAvailableShippingMethods: function (cart, shipmentId) {
                if (authService.isAuthenticated) 
                    return $http.get(cart.apiUrl + 'api/carts/' + cart.id + '/availshippingrates?t=' + new Date().getTime());
                return $http.get(cart.apiUrl + 'api/carts/' + cart.id + '/availshippingrates?api_key=' + cart.apiKey + '&t=' + new Date().getTime());
            },
            getAvailablePaymentMethods: function (cart) {
                if (authService.isAuthenticated) 
                    return $http.get(cart.apiUrl + 'api/carts/' + cart.id + '/availpaymentmethods');
                return $http.get(cart.apiUrl + 'api/carts/' + cart.id + '/availpaymentmethods?api_key=' + cart.apiKey);
            },
            removeCart: function (cart) {
                if (authService.isAuthenticated) 
                    return $http.delete(cart.apiUrl + 'api/carts?ids=' + cart.id);
                return $http.delete(cart.apiUrl + 'api/carts?api_key=' + cart.apiKey + '&ids=' + cart.id);
            },
            createOrder: function (cart) {
                if (authService.isAuthenticated) 
                    return $http.post(cart.apiUrl + 'api/order/customerOrders/' + cart.id, cart);
                return $http.post(cart.apiUrl + 'api/order/customerOrders/' + cart.id + '?api_key=' + cart.apiKey, cart);
            },
            processPayment: function (cart , orderId, paymentId) {
                if (authService.isAuthenticated) 
                    return $http.post(cart.apiUrl + 'api/order/customerOrders/' + orderId +'/processPayment/' + paymentId);
                return $http.post(cart.apiUrl + 'api/order/customerOrders/' + orderId + '/processPayment/' + paymentId + '?api_key=' + cart.apiKey);
            },

            // using jscart API
            getOrCreateCart: function (cart) {
                return $http.get(getJsCartUrl(cart) + '/' + cart.storeId + '/' + cart.userId + '/' + cart.name + '/current?t=' + new Date().getTime())
            },
            addCouponNew: function (cart, couponCode) {
                return $http.post(getJsCartUrl(cart) + '/' + cart.id + '/coupons/' + couponCode);
            },
            removeCouponNew: function (cart, couponCode) {
                return $http.delete(getJsCartUrl(cart) + '/' + cart.id + '/coupons' + (angular.isDefined(couponCode) ? '/' + couponCode : ''));
            },
            validateCouponNew: function (cart, coupon) {
                return $http.delete(getJsCartUrl(cart) + '/' + cart.id + '/coupons/validate', coupon);
            }

        }
    }]);
