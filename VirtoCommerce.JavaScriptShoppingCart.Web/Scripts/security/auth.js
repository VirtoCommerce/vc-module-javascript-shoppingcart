cartModule.service('virtoCommerce.cartModule.authService', ['$http', '$rootScope', '$cookieStore', '$interpolate', '$q', 'virtoCommerce.cartModule.authDataStorage', function ($http, $rootScope, $cookieStore, $interpolate, $q, authDataStorage) {
    var platformEndPoint = authDataStorage.getPlatformUrl();
    var platrofmApiKey = authDataStorage.getPlatformKey();
    var serviceBase = 'api/platform/security/';
    var authContext = {
        userId: null,
        memberId: null,
        userLogin: null,
        fullName: null,
        permissions: null,
        isAuthenticated: false
    };

    function guid() {
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
        }

        return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
    }

    authContext.fillAuthData = function() {
        return $http.get(`${platformEndPoint}jscart/api/security/currentuser`).then(
            function (results) {
                changeAuth(results.data);
            });
    };

    authContext.login = function (email, password, remember) {
        var requestData = 'grant_type=password&username=' + encodeURIComponent(email) + '&password=' + encodeURIComponent(password);

        return $http.post(platformEndPoint + 'token', requestData, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(
            function (response) {
                var authData = {
                    token: response.data.access_token,
                    userName: email,
                    expiresAt: getCurrentDateWithOffset(response.data.expires_in),
                    refreshToken: response.data.refresh_token
                };
                authDataStorage.storeAuthData(authData);

                return authContext.fillAuthData().then(function () {
                    return response.data;
                });
            }, function (error) {
                authContext.logout();
                return $q.reject(error);
            });
    };

    authContext.refreshToken = function () {
        var authData = authDataStorage.getStoredData();
        if (authData) {
            var data = 'grant_type=refresh_token&refresh_token=' + encodeURIComponent(authData.refreshToken);

            // NOTE: this method is called by the HTTP interceptor if the access token in the local storage expired.
            //       So we clean the storage before sending the HTTP request - otherwise the HTTP interceptor will
            //       detect expired token and will call this method again, causing the infinite loop.
            authDataStorage.clearStoredData();

            return $http.post(platformEndPoint +'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(
                function (response) {
                    var newAuthData = {
                        token: response.data.access_token,
                        userName: response.data.userName,
                        expiresAt: getCurrentDateWithOffset(response.data.expires_in),
                        refreshToken: response.data.refresh_token
                    };
                    authDataStorage.storeAuthData(newAuthData);
                    return newAuthData;
                }, function (err) {
                    authContext.logout();
                    return $q.reject(err);
                });
        } else {
            return $q.reject();
        }
    };

    function getCurrentDateWithOffset(offsetInSeconds) {
        return Date.now() + offsetInSeconds * 1000;
    }

    authContext.requestpasswordreset = function (data) {
        return $http.post(platformEndPoint + serviceBase + 'users/' + data.userName + '/requestpasswordreset/').then(
            function (results) {
                return results.data;
            });
    };

    authContext.signUp = function(data) {
        return $http.post(platformEndPoint + 'jscart/api/security/registerUser', data, { headers: { 'Content-Type': 'application/json' } }).then(
            function (response) {
                return response;
            }, function (err) {
                return $q.reject(err);
            });

    };

    authContext.validatePassword = function(data) {
        return $http.post(platformEndPoint + 'jscart/api/security/validatepassword', data, { headers: { 'Content-Type': 'application/json' } }).then(
            function (response) {
                return response;
            }, function (err) {
                return $q.reject(err);
            });

    };

    authContext.validatepasswordresettoken = function (data) {
        return $http.post(platformEndPoint + serviceBase + 'users/' + data.userId + '/validatepasswordresettoken?api_key=' + platrofmApiKey, { token: data.code }).then(
            function (results) {
                return results.data;
            });
    };

    authContext.resetpassword = function (data) {
        return $http.post(platformEndPoint + serviceBase + 'users/' + data.userId + '/resetpasswordconfirm?api_key=' + platrofmApiKey, { token: data.code, newPassword: data.newPassword }).then(
            function (results) {
                return results.data;
            });
    };

    authContext.logout = function () {
        authDataStorage.clearStoredData();
        $http.post(platformEndPoint + serviceBase + 'logout?api_key=' + platrofmApiKey);
        changeAuth({});
        $rootScope.$broadcast('userLoggedOut');
    };

    authContext.checkPermission = function (permission, securityScopes) {
        //first check admin permission
        // var hasPermission = $.inArray('admin', authContext.permissions) > -1;
        var hasPermission = authContext.isAdministrator;
        if (!hasPermission && permission) {
            permission = permission.trim();
            //first check global permissions
            hasPermission = $.inArray(permission, authContext.permissions) > -1;
            if (!hasPermission && securityScopes) {
                if (typeof securityScopes === 'string' || angular.isArray(securityScopes)) {
                    securityScopes = angular.isArray(securityScopes) ? securityScopes : securityScopes.split(',');
                    //Check permissions in scope
                    hasPermission = _.some(securityScopes, function (x) {
                        var permissionWithScope = permission + ":" + x;
                        var retVal = $.inArray(permissionWithScope, authContext.permissions) > -1;
                        //console.log(permissionWithScope + "=" + retVal);
                        return retVal;
                    });
                }
            }
        }
        return hasPermission;
    };

    function changeAuth(user) {
        if (user) {
            angular.extend(authContext, user);
            authContext.userLogin = user.userName;
            authContext.fullName = user.userLogin;
            authContext.isAuthenticated = user.userName != null;
            authContext.userId = user.id;
            authContext.memberId = user.memberId;
        } else {
            authContext.userLogin = undefined;
            authContext.memberId = undefined;
            authContext.isAuthenticated = false;
            if (!authContext.userId) {
                const existUserId = authDataStorage.getUserId();
                if (existUserId) {
                    authContext.userId = existUserId;
                } else {
                    authContext.userId = guid();
                    authDataStorage.storeUserId(authContext.userId);
                }
            }
        }
        //Interpolate permissions to replace some template to real value
        if (authContext.permissions) {
            authContext.permissions = _.map(authContext.permissions, function (x) {
                return $interpolate(x)(authContext);
            });
        }

            $rootScope.$broadcast('loginStatusChanged', authContext);
    }
    return authContext;
}]);