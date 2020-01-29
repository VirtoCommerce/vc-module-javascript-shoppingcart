cartModule.service('virtoCommerce.cartModule.authService', ['$http', '$rootScope', '$cookieStore', '$interpolate', '$q', 'virtoCommerce.cartModule.authDataStorage', function ($http, $rootScope, $cookieStore, $interpolate, $q, authDataStorage) {
	var platformEndPoint;
	var platformApiKey;
	var serviceBase = 'api/platform/security/';
	var context = {
		userId: null,
		memberId: null,
		userLogin: null,
		fullName: null,
		permissions: null,
		isAuthenticated: false
	};

	function getPlatformEndpoint() {
		if (!platformEndPoint) {
			platformEndPoint = authDataStorage.getPlatformUrl();
		}
		return platformEndPoint;
	}

	function getPlatformApiKey() {
		if (!platformApiKey) {
			platformApiKey = authDataStorage.getPlatformKey();
		}
		return platformApiKey;
	}

	function guid() {
		function s4() {
			return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
		}

		return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
	}

	context.fillAuthData = function() {
        var url = `${getPlatformEndpoint()}jscart/api/security/currentuser`;
        return $http.get(url).then(
			function (results) {
				changeAuth(results.data);
			});
	};

	context.login = function (email, password) {
		var encodedEmail = encodeURIComponent(email);
		var encodedURI = encodeURIComponent(password);
		var requestData = 'grant_type=password&username=' + encodedEmail + '&password=' + encodedURI;
		var url = getPlatformEndpoint() + 'token';
		var headers = { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } };

		return $http.post(url, requestData, headers).then(
			function (response) {
				var authData = {
					token: response.data.access_token,
					userName: email,
					expiresAt: getCurrentDateWithOffset(response.data.expires_in),
					refreshToken: response.data.refresh_token
				};
				authDataStorage.storeAuthData(authData);

				return context.fillAuthData().then(function () {
					return response.data;
				});
			}, function (error) {
				context.logout();
				return $q.reject(error);
			});
	};

	context.refreshToken = function () {
		var authData = authDataStorage.getStoredData();
		if (authData) {
			var data = 'grant_type=refresh_token&refresh_token=' + encodeURIComponent(authData.refreshToken);

			// This method is called by the HTTP interceptor if the access token in the local storage expired.
			// So we clean the storage before sending the HTTP request - otherwise, the HTTP interceptor will
			// detect expired token and call this method again, causing the infinite loop.
			authDataStorage.clearStoredData();

            var url = getPlatformEndpoint() + 'token';
            var headers = { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } };
			return $http.post(url, data, headers).then(
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
					context.logout();
					return $q.reject(err);
				});
		} else {
			return $q.reject();
		}
	};

	function getCurrentDateWithOffset(offsetInSeconds) {
		return Date.now() + offsetInSeconds * 1000;
	}

    context.requestpasswordreset = function (data) {
        var url = getPlatformEndpoint() + serviceBase + 'users/' + data.userName + '/requestpasswordreset/';
		return $http.post(url).then(
			function (results) {
				return results.data;
			});
	};

    context.signUp = function (data) {
        var url = getPlatformEndpoint() + 'jscart/api/security/registerUser';
        var headers = { headers: { 'Content-Type': 'application/json' } };
		return $http.post(url, data, headers).then(
			function (response) {
				return response;
			}, function (error) {
				return $q.reject(error);
			});

	};

	context.validatePassword = function(data) {
        var url = getPlatformEndpoint() + 'jscart/api/security/validatepassword';
        var headers = { headers: { 'Content-Type': 'application/json' } };
        return $http.post(url, data, headers).then(
			function (response) {
				return response;
			}, function (error) {
				return $q.reject(error);
			});

	};

    context.validatepasswordresettoken = function (data) {
        var url = getPlatformEndpoint() +
            serviceBase +
            'users/' +
            data.userId +
            '/validatepasswordresettoken?api_key=' +
            getPlatformApiKey();

        return $http.post(url, { token: data.code }).then(
			function (results) {
				return results.data;
			});
	};

	context.resetpassword = function (data) {
        var url = getPlatformEndpoint() +
            serviceBase +
            'users/' +
            data.userId +
            '/resetpasswordconfirm?api_key=' +
            getPlatformApiKey();

        return $http.post(url, { token: data.code, newPassword: data.newPassword }).then(
			function (results) {
				return results.data;
			});
	};

	context.logout = function () {
		authDataStorage.clearStoredData();
        var url = getPlatformEndpoint() + serviceBase + 'logout?api_key=' + getPlatformApiKey();
        $http.post(url);
		changeAuth({});
		$rootScope.$broadcast('userLoggedOut');
	};

	context.checkPermission = function (permission, securityScopes) {
		// at first check admin permission
		var isAdministrator = context.isAdministrator;

		if (!isAdministrator && permission) {
			permission = permission.trim();

			// at second check global permissions
			isAdministrator = $.inArray(permission, context.permissions) > -1;
			if (!isAdministrator && securityScopes) {
				if (typeof securityScopes === 'string' || angular.isArray(securityScopes)) {
					securityScopes = angular.isArray(securityScopes) ? securityScopes : securityScopes.split(',');

					// check permissions in scope
					isAdministrator = _.some(securityScopes, function (x) {
						var permissionWithScope = permission + ":" + x;
						var result = $.inArray(permissionWithScope, context.permissions) > -1;
						
						return result;
					});
				}
			}
		}
		return isAdministrator;
	};

	function changeAuth(user) {
		if (user) {
			angular.extend(context, user);
			context.userLogin = user.userName;
			context.fullName = user.userLogin;
			context.isAuthenticated = user.userName !== null;
			context.userId = user.id;
			context.memberId = user.memberId;
		} else {
			context.userLogin = undefined;
			context.memberId = undefined;
			context.isAuthenticated = false;

			if (!context.userId) {
				var existentUserId = authDataStorage.getUserId();
				if (existentUserId) {
					context.userId = existentUserId;
				} else {
					context.userId = guid();
					authDataStorage.storeUserId(context.userId);
				}
			}
		}

		// Interpolate permissions to replace some template to real value
		if (context.permissions) {
			context.permissions = _.map(context.permissions, function (x) {
				return $interpolate(x)(context);
			});
		}

			$rootScope.$broadcast('loginStatusChanged', context);
	}
	return context;
}]);
