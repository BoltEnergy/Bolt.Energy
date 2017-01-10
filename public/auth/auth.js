angular.module('auth', ["ngAnimate", "ngTouch", "ui.router", "ngResource","toastr","bolt"])
.service('LoginService', ['$http', '$q', '$window', 'ConfigService', function ($http, $q, $window, ConfigService) {
  return {
    login: function (email, password) {
      var $return = $q.defer();
      $return.promise = $http({
        method: 'POST',
        url: ConfigService.appRoot() + '/data/authenticate',
        data: {
          email: email,
          password: password
        }
      }).then(function (responseData) {
        $http.defaults.headers.common.Authorization = responseData.data.token;
        $window.localStorage.setItem('BoltToken', responseData.data.token);
        $window.localStorage.setItem('BoltUser', responseData.data.user._id);
        $return.resolve({
          token: responseData.data.token,
          user: responseData.data.user
        });
      }, function (httpError) {
        $return.reject({
          status: httpError.status
        })
      })
      return $return.promise;
    },
    logout: function () {
      try {
        $http.defaults.headers.common.Authorization = null;
        $window.localStorage.removeItem('BoltToken');
        $window.localStorage.removeItem('BoltUser');
      }
      catch (e) {
        console.log(e);
      }
    },
    authenticated: function () {
      if (($window.localStorage.getItem('BoltToken') != 'undefined') && ($window.localStorage.getItem('BoltToken') != null)) {
        return true;
      }
      else {
        return false;
      }
    },
    registerUser: function (firstname, lastname, email, password, accountType) {
      //Register a new user account
      var $return = $q.defer();
      $return.promise = $http({
        method: 'POST',
        url: ConfigService.appRoot() + '/data/register',
        data: {
          firstName: firstname,
          lastName: lastname,
          email: email,
          password: password,
          accountType: accountType
        }
      }).then(function (responseData) {
        $http.defaults.headers.common.Authorization = responseData.data.token;
        $window.localStorage.setItem('BoltToken', responseData.data.token);
        $window.localStorage.setItem('BoltUser', responseData.data.user._id);
        $return.resolve({
            token: responseData.data.token,
            user: responseData.data.user
        });
      }, function (status) {
        $return.reject({
            status: status.status
        });
      })
      return $return.promise;
    },
    registerProducer: function(producer) {
      var $return = $q.defer();
      $return.promise = $http({
        method: 'POST',
        url: ConfigService.appRoot() + ConfigService.epProducer(),
        data: producer
      }).then(function (responseData) {
        $return.resolve(responseData.producer);
      }, function (status) {
        $return.reject({
            status: status.status
        });
      })
      return $return.promise;
    }
  }
}])
.controller('LoginController', function($rootScope, $scope, LoginService, $state, toastr, $http, $window) {
  $rootScope.authenticated = LoginService.authenticated();
  $scope.login = function () {
		var result = LoginService.login($scope.email, $scope.password);
		result.then(function (responseData) {
			$rootScope.authenticated = LoginService.authenticated();
			$rootScope.currentUserId = responseData.user._id;
			$state.go('home');
			toastr.success("Welcome to Bolt, " + responseData.user.firstName, "Login Successful");
		}, function (status) {
			toastr.error("Error logging in.", "Login Failed");
			$state.go('login');
		})
  }
  $scope.logout = function () {
    var result = LoginService.logout();
    $rootScope.authenticated = false;
    $rootScope.currentUserId = '';
    $state.go('home');
  }
})
.controller('RegisterController', ['$rootScope','$scope','LoginService','$state','toastr','$window',function($rootScope,$scope,LoginService,$state,toastr,$window) {
	$scope.register = 	function() {
	  var arr = [];
	  switch($scope.accountType) {
	    case 'Producer':
	      LoginService.registerUser($scope.firstName, $scope.lastName, $scope.email, $scope.password, $scope.accountType).then(function(responseData) {
          $rootScope.authenticated = LoginService.authenticated();
			    $rootScope.currentUserId = responseData.user._id;
			    var firstName = responseData.user.firstName;
	        LoginService.registerProducer($scope.producer).then(function(responseData) {
	          $state.go('home', {}, { reload: true });
	          $window.location.reload();
	          toastr.success('Welcome to Bolt, ' + firstName, "Registration Successful");
	        });
	      });
	      break;
      case 'Consumer':
	      LoginService.registerUser($scope.firstName, $scope.lastName, $scope.email, $scope.password, $scope.accountType).then(function(responseData) {
          $rootScope.authenticated = LoginService.authenticated();
			    $rootScope.currentUserId = responseData.user._id;
	        $state.go('home', {}, { reload: true });
	        var firstName = responseData.user.firstName;
          $window.location.reload();
          toastr.success('Welcome to Bolt, ' + firstName, "Registration Successful");
	      });
	      break;
      default:
        break;
	  }
	}
}])