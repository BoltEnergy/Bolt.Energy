angular.module('auth', ["ngAnimate", "ngTouch", "ui.router", "ngResource", "toastr", "bolt"])
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
                $window.localStorage.setItem('BoltUserType', responseData.data.user.accountType);
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
        fblogin: function (emailId, firstName, lastName) {
            var $return = $q.defer();
            $return.promise = $http({
                method: 'POST',
                url: ConfigService.appRoot() + '/data/fbauthenticate',
                data: {
                    email: emailId,
                    Fname: firstName,
                    Lname: lastName
                }
            }).then(function (responseData) {
                $http.defaults.headers.common.Authorization = responseData.data.token;
                $window.localStorage.setItem('BoltToken', responseData.data.token);
                $window.localStorage.setItem('BoltUser', responseData.data.user._id);
                $window.localStorage.setItem('BoltUserType', responseData.data.user.accountType);
                $return.resolve({
                    token: responseData.data.token,
                    user: responseData.data.user
                });
            }, function (httpError) {
                $window.localStorage.setItem('BoltSignupEmail', emailId);
                $window.localStorage.setItem('BoltSignupFirstName', firstName);
                $window.localStorage.setItem('BoltSignupLastName', lastName);
                $return.reject({

                    status: httpError.status
                })
                console.log(httpError);
            })
            return $return.promise;
        },
        logout: function () {
            try {
                $http.defaults.headers.common.Authorization = null;
                $window.localStorage.removeItem('BoltToken');
                $window.localStorage.removeItem('BoltUser');
                $window.localStorage.removeItem('BoltUserType');
                if (isFbLogin) {
                    FB.logout(function (response) {

                    });
                }
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
        isProducer: function () {
            if (($window.localStorage.getItem('BoltUserType') != 'undefined') && ($window.localStorage.getItem('BoltUserType') != null)) {
                if ($window.localStorage.getItem('BoltUserType') == "Producer") {
                    return true;
                }
                else { return false; }
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
                $window.localStorage.setItem('BoltUserType', responseData.data.user.accountType);
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
        registerProducer: function (producer) {
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
        },
        bindFBbutton: function () {
            if (FB) {
                FB.XFBML.parse();
            }
        }
    }
}])
.controller('LoginController', function ($rootScope, $scope, LoginService, $state, toastr, $http, $window) {
    $rootScope.authenticated = LoginService.authenticated();
    $rootScope.isProducer = LoginService.isProducer();
    try{
        $rootScope.bindFB = LoginService.bindFBbutton();
    }
    catch(err){}
    $scope.login = function () {
        var result = LoginService.login($scope.email, $scope.password);
        result.then(function (responseData) {
            $rootScope.authenticated = LoginService.authenticated();
            $rootScope.currentUserId = responseData.user._id;
            $rootScope.isProducer = LoginService.isProducer();
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
        $rootScope.isProducer = false;
        $rootScope.currentUserId = '';
        $state.go('home');
    }
    $scope.fblogin = function (emailId, FName, LName) {
        var result = LoginService.fblogin(emailId, FName, LName);
        result.then(function (responseData) {
            $rootScope.authenticated = LoginService.authenticated();
            $rootScope.currentUserId = responseData.user._id;
            $rootScope.isProducer = LoginService.isProducer();
            $state.go('home');
            toastr.success("Welcome to Bolt, " + responseData.user.firstName, "Login Successful");
        }, function (status) {
            if ($state.current.name == "register") {
                window.location.reload();
            }
            else {
                $state.go('register');
            }
        })
    }
})
.controller('RegisterController', ['$rootScope', '$scope', 'LoginService', '$state', 'toastr', '$window', function ($rootScope, $scope, LoginService, $state, toastr, $window) {
    if (($window.localStorage.getItem('BoltSignupEmail') != 'undefined') && ($window.localStorage.getItem('BoltSignupEmail') != null)) {
        $scope.email = $window.localStorage.getItem('BoltSignupEmail');
        $scope.firstName = $window.localStorage.getItem('BoltSignupFirstName');
        $scope.lastName = $window.localStorage.getItem('BoltSignupLastName');
    }
    
    $scope.register = function () {
        $window.localStorage.removeItem('BoltSignupEmail');
        $window.localStorage.removeItem('BoltSignupFirstName');
        $window.localStorage.removeItem('BoltSignupLastName');
        var arr = [];
        switch ($scope.accountType) {
            case 'Producer':
                LoginService.registerUser($scope.firstName, $scope.lastName, $scope.email, $scope.password, $scope.accountType).then(function (responseData) {
                    $rootScope.authenticated = LoginService.authenticated();
                    $rootScope.isProducer = LoginService.isProducer();
                    $rootScope.currentUserId = responseData.user._id;
                    var firstName = responseData.user.firstName;
                    LoginService.registerProducer($scope.producer).then(function (responseData) {
                        $state.go('home', {}, { reload: true });
                        $window.location.reload();
                        toastr.success('Welcome to Bolt, ' + firstName, "Registration Successful");
                    });
                });
                break;
            case 'Consumer':
                LoginService.registerUser($scope.firstName, $scope.lastName, $scope.email, $scope.password, $scope.accountType).then(function (responseData) {
                    $rootScope.authenticated = LoginService.authenticated();
                    $rootScope.isProducer = LoginService.isProducer();
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
    $scope.cancel = function () {
        $window.localStorage.removeItem('BoltSignupEmail');
        $window.localStorage.removeItem('BoltSignupFirstName');
        $window.localStorage.removeItem('BoltSignupLastName');
        $state.go('login');
    }
   
}])