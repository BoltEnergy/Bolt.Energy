angular.module('bolt', ["ngAnimate", "ngTouch", "ui.router", "ngResource", "toastr", "auth", "home", "search", "profile", "producer", 'djds4rce.angular-socialshare', "content"])
.run(function ($FB) {
    $FB.init('376721239380234');
})

.run(function ($state, $rootScope, toastr, LoginService) {
    $rootScope.$on('$stateChangeError', function (event, toState, toParams, fromState, fromParams, error) {
        event.preventDefault();
        if (error.status) {
            switch (error.status) {
                case 401:
                    LoginService.logout();
                    $rootScope.authenticated = false;
                    $rootScope.currentUserId = '';
                    $rootScope.isProducer = false;
                    toastr.error('Your session has expired, please login again.', 'Expired Session Error');
                    break;
                case 403:
                    toastr.error('', 'Unauthorized Access Attempt');
                    break;
                case 500:
                    toastr.error('A server error has occurred.', 'Error');
                    break;
                default:
                    if (error.message) {
                        toastr.error(error.message, 'Error');
                    } else {
                        toastr.error('An error has occurred.', 'Error');
                    };
                    break;
            }
        } else {
            toastr.error('An error has occurred.', 'Error');
        }
    });
    $rootScope.$on('$stateChangeSuccess', function () {
        document.body.scrollTop = document.documentElement.scrollTop = 0;
    });
})
.config(function ($stateProvider, $urlRouterProvider, $compileProvider) {

    //$compileProvider.debugInfoEnabled(false);

    $urlRouterProvider.otherwise('/home');

    $stateProvider.state('home', {
        // parent: 'root',
        url: '/home',
        templateUrl: 'home/home.html',
        controller: 'HomeController',
        resolve: {
            HomePageResults: function (HomePageFactory, $q) {
                var retVal = $q.defer();
                retVal.promise = HomePageFactory.getHomePageResults().then(function (responseData) {
                    retVal.resolve(responseData);
                }, function (err) {
                    retVal.reject(err);
                })
                return retVal.promise;
            }
        }
    })

    //Search
    $stateProvider.state('search', {
        // //parent: 'root',
        url: '/search',
        templateUrl: 'search/search.html',
        controller: 'SearchController'
    })

    //Auth Pages
    $stateProvider.state('login', {
        //parent: 'root',
        url: '/login',
        templateUrl: 'auth/login.html',
        controller: 'LoginController'
    })
    $stateProvider.state('register', {
        //parent: 'root',
        url: '/register',
        templateUrl: 'auth/register.html',
        controller: 'RegisterController'
    })

    //Profile Edit Pages
    $stateProvider.state('profile', {
        //parent: 'root',
        url: '/profile/:id',
        templateUrl: 'profile/profile.html',
        controller: 'ProfileController'
      ,
        resolve: {
            profileData: function (ProfileFactory, $stateParams, $q) {
                var retVal = $q.defer();
                if ($stateParams.id) {
                    retVal.promise = ProfileFactory.getUser($stateParams.id).then(function (responseData) {
                        retVal.resolve(responseData);
                    }, function (error) {
                        retVal.reject(error);
                    });
                } else {
                    retVal.reject();
                }
                return retVal.promise
            },
            producerData: function (profileData, ProfileFactory, $stateParams, $q) {
                var retVal = $q.defer();
                if (profileData.accountType == 'Producer') {
                    retVal.promise = ProfileFactory.findProducerByOwner($stateParams.id).then(function (responseData) {
                        retVal.resolve(responseData);
                    }, function (err) {
                        retVal.reject(err);
                    })
                } else {
                    retVal.resolve();
                }
                return retVal.promise;
            }
        }
    })
    $stateProvider.state('producer_edit', {
        //parent: 'root',
        url: '/producer_edit/:producerId',
        templateUrl: '/profile/producer.html',
        controller: 'ProducerEditController',
        resolve: {
            producerEdit: function (ProducerFactory, $stateParams, $q) {
                var retVal = $q.defer();
                if ($stateParams.producerId) {
                    retVal.promise = ProducerFactory.getProducer($stateParams.producerId).then(function (responseData) {
                        retVal.resolve(responseData);
                    }, function (error) {
                        retVal.reject(error);
                    })
                    return retVal.promise;
                } else {
                    return {
                        name: '',
                        desc: '',
                        status: '',
                        availability: [],
                        owner: '',
                        type: '',
                        energyType: '',
                        uploads: [],
                        address1: '',
                        address2: '',
                        state: '',
                        city: '',
                        zip: '',
                        approvalNumber: '',
                        certifications: [],
                        projects: [],
                        updates: [],
                        comments: []
                    }
                }
            }
        }
    })
    $stateProvider.state('project_edit', {
        //parent: 'root',
        url: '/producer/:producerId/project_edit/:projectId',
        templateUrl: '/profile/project.html',
        controller: 'ProjectEditController',
        resolve: {
            projectEdit: function (ProfileFactory, $stateParams, $q) {
                if ($stateParams.projectId) {
                    var retVal = $q.defer();
                    retVal.promise = ProfileFactory.getProject($stateParams.projectId).then(function (responseData) {
                        retVal.resolve(responseData);
                    }, function (error) {
                        retVal.reject(error);
                    })
                    return retVal.promise;
                } else {
                    var retVal = $q.defer();
                    retVal.promise = ProfileFactory.getProducerProject($stateParams.producerId).then(function (responseData) {
                        var responsearray = [];
                        responsearray.push(responseData.producer);
                        responsearray.push({
                            name: '',
                            desc: '',
                            status: '',
                            availability: [],
                            projectType: '',
                            uploads: [],
                            energyMix: '',
                            address1: '',
                            address2: '',
                            city: '',
                            state: '',
                            zip: '',
                            programCategory: '',
                            capacity: '',
                            utilityDistricts: [],
                            projectOwner: null,
                            featured: false
                        });
                        retVal.resolve(responsearray);
                    }, function (error) {
                        retVal.reject(error);
                    })
                    return retVal.promise;
                }
            }
        }
    })
    $stateProvider.state('update_edit', {
        //parent: 'root',
        url: '/producer/:producerId/update_edit/:updateId',
        templateUrl: '/profile/update.html',
        controller: 'UpdateEditController',
        resolve: {
            updateEdit: function (ProfileFactory, $stateParams, $q) {
                if ($stateParams.updateId) {
                    var retVal = $q.defer();
                    retVal.promise = ProfileFactory.getUpdate($stateParams.updateId).then(function (responseData) {
                        retVal.resolve(responseData);
                    }, function (error) {
                        retVal.reject(error);
                    })
                    return retVal.promise;
                } else {
                    var retVal = $q.defer();
                    retVal.promise = ProfileFactory.getProducerProject($stateParams.producerId).then(function (responseData) {
                        var responsearray = [];
                        responsearray.push(responseData.producer);
                        responsearray.push({
                            title: '',
                            body: '',
                            visible: true
                        });
                        retVal.resolve(responsearray);
                    }, function (error) {
                        retVal.reject(error);
                    })
                    return retVal.promise;
                }
            }
        }
    })

    //Profile View Pages
    $stateProvider.state('producer', {
        //parent: 'root',
        url: '/producer/:producerId/:activeTab',
        templateUrl: 'producer/producer.html',
        controller: 'ProducerController',
        resolve: {
            producer: function ($stateParams, ProducerFactory, $q) {
                var retVal = $q.defer();
                retVal.promise = ProducerFactory.getProducer($stateParams.producerId).then(function (responseData) {
                    retVal.resolve(responseData);
                }, function (error) {
                    retVal.reject(error);
                });
                return retVal.promise;
            }
        }
    })

    //Content for pages
    $stateProvider.state('content', {
        // parent: 'root',
        url: '/content/:vanityUrl',
        templateUrl: 'content/page.html',
        controller: 'ContentController',
        resolve: {
            contentData: function ($stateParams, ContentFactory, $q) {
                var retVal = $q.defer();
                retVal.promise = ContentFactory.getContent($stateParams.vanityUrl).then(function (responseData) {
                    retVal.resolve(responseData);
                }, function (err) {
                    retVal.reject(err);
                })
                return retVal.promise;
            }
        }
    })

    //Static Pages
    $stateProvider.state('about', {
        //parent: 'root',
        url: '/about',
        templateUrl: 'static/about.html',
        controller: 'StaticController'
    })
    $stateProvider.state('terms', {
        //parent: 'root',
        url: '/terms',
        templateUrl: 'static/terms.html',
        controller: 'StaticController'
    })
    $stateProvider.state('privacy', {
        //parent: 'root',
        url: '/privacy',
        templateUrl: 'static/privacy.html',
        controller: 'StaticController'
    })


})
.controller('StaticController', function () {
    //Static Page
})

.controller('HeaderController', function ($rootScope, $scope, LoginService, ProfileFactory, $state, $window, $http) {
    $http.defaults.headers.common.Authorization = $window.localStorage.getItem('BoltToken') || '';
    $rootScope.currentUserId = $window.localStorage.getItem('BoltUser') || '';
    $scope.currentUserId = $rootScope.currentUserId;
    $rootScope.authenticated = LoginService.authenticated();
    $rootScope.isProducer = LoginService.isProducer();

    $scope.logout = function () {
        LoginService.logout();
        $rootScope.authenticated = LoginService.authenticated();
        $rootScope.isProducer = LoginService.isProducer();
        $state.go('home');
    }
    $scope.profile = function () {
        $state.go('profile', { id: $rootScope.currentUserId }, {});
    }
    $scope.publicProfile = function () {
        if ($rootScope.isProducer) {
            ProfileFactory.findProducerByOwner($rootScope.currentUserId).then(function (responseData) {
                $state.go('producer', { producerId: responseData[0]._id }, {});
            }, function (err) {
                console.log(err);
            })
        }
    }
    $scope.community = function () {
        var forumUrl = "http://localhost:25010/";
        if ($rootScope.currentUserId != "") {
            ProfileFactory.getUser($rootScope.currentUserId).then(function (responseData) {
                forumUrl+= "prelogin.aspx?action=directlogin&";
                ////// Example : http://community.bolt.energy/? /////////////
                if (forumUrl.length < 10) {
                    alert("Forum URL required to Redirect.");
                }
                else {
                    window.location.href = forumUrl + "userid=" + responseData.email + "&fname=" + responseData.firstName + "&lname=" + responseData.lastName;
                }
            }, function (err) {
                console.log(err);
            })
        }
        else {
            window.location.href = forumUrl;
        }
    }
})
// .controller('RootController', function ($rootScope, $scope, LoginService, $state, $window, $http) {
//   $http.defaults.headers.common.Authorization = $window.localStorage.getItem('BoltToken') || '';
//   $rootScope.currentUserId = $window.localStorage.getItem('BoltUser') || '';
//   $scope.currentUserId = $rootScope.currentUserId;
//   $rootScope.authenticated = LoginService.authenticated();
//   $scope.logout = function () {
//     LoginService.logout();
//     $rootScope.authenticated = LoginService.authenticated();
//     $state.go('home');
//   }
//   $scope.profile = function() {
//     $state.go('profile', { id: $rootScope.currentUserId }, {});
//   }
// })
.service('ConfigService', function ($http, $q, $window) {
    return {
        appRoot: function () {
            //var url = 'http://devbolt.testsep.com:9182';
			var url = 'http://localhost:8080';
            //var url = 'https://bolt-test-sgannonumd.c9users.io';
            // var url = '';
            // if ($window.location.href.indexOf('c9users') > -1) {
            //   //url = 'https://bolt-test-sgannonumd.c9users.io';
            //   url = 'https://kfund-sgannonumd.c9users.io';
            // } else {
            //   url = 'http://localhost:8080';
            // }
            return url;
        },
        epUser: function () {
            return '/data/users';
        },
        epProducer: function () {
            return '/data/producer';
        },
        epProject: function () {
            return '/data/project';
        },
        epSearchProject: function () {
            return '/data/projectSearch';
        },
        epSearchProducer: function () {
            return '/data/producerSearch';
        },
        epUpdate: function () {
            return '/data/update';
        },
        epContent: function () {
            return '/data/content';
        }
    }
})
.directive('fileModel', ['$parse', '$compile', function ($parse, $compile) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files);
                });
            });
        }
    };
}])
.animation('.slide-animation', function () {
    return {
        addClass: function (element, className, done) {
            var scope = element.scope();
            element = $(element);

            if (className == 'ng-hide') {
                var finishPoint = element.parent().width();
                if (scope.direction !== 'right') {
                    finishPoint = -finishPoint;
                }
                TweenMax.to(element, 0.5, { left: finishPoint, onComplete: done });
            }
            else {
                done();
            }
        },
        removeClass: function (element, className, done) {
            var scope = element.scope();
            element = $(element);
            if (className == 'ng-hide') {
                element.removeClass('ng-hide');

                var startPoint = element.parent().width();
                if (scope.direction === 'right') {
                    startPoint = -startPoint;
                }

                TweenMax.set(element, { left: startPoint });
                TweenMax.to(element, 0.5, { left: 0, onComplete: done });
            }
            else {
                done();
            }
        }
    };
});
