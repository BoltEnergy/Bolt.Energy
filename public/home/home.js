angular.module('home',["ngAnimate", "ngTouch", "ui.router", "ngResource","toastr","bolt"])
.controller('HomeController', ['$scope','HomePageFactory','HomePageResults','toastr',function($scope,HomePageFactory,HomePageResults,toastr){
  if (HomePageResults.length >= 3) {
    var contentRows = Math.floor(HomePageResults.length/3);
    var pCnt = 0;
    var out = [];
    for (var iCnt = 0; iCnt < contentRows; iCnt++) {
      out[iCnt] = [];
      out[iCnt].push(HomePageResults[pCnt]);
      pCnt += 1;
      out[iCnt].push(HomePageResults[pCnt]);
      pCnt += 1;
      out[iCnt].push(HomePageResults[pCnt]);
    }
    $scope.rows = out;
 } else {
    var pCnt = HomePageResults.length;
    var out = [];
    out[0] = [];
    for (var iCnt = 0; iCnt < pCnt; iCnt++) {
      out[0].push(HomePageResults[iCnt]);
    }
    $scope.rows = out;
 }  
  $scope.updateFilters = function() {
      $scope.results = HomePageFactory.getHomePageResults($scope.FilterType,$scope.FilterState).then(function(responseData) {
           if (responseData.length >= 3) {
              var contentRows = Math.floor(responseData.length/3);
              var pCnt = 0;
              var out = [];
              for (var iCnt = 0; iCnt < contentRows; iCnt++) {
                  out[iCnt] = [];
                  out[iCnt].push(responseData[pCnt]);
                  pCnt += 1;
                  out[iCnt].push(responseData[pCnt]);
                  pCnt += 1;
                  out[iCnt].push(responseData[pCnt]);
              }
              $scope.rows = out;
           } else {
              var pCnt = responseData.length;
              var out = [];
              out[0] = [];
              for (var iCnt = 0; iCnt < pCnt; iCnt++) {
                  out[0].push(responseData[iCnt]);
              }
              $scope.rows = out;
           }
          toastr.success('Search results updated.','Success');
      }, function(err) {
          toastr.error('Error updatingsearch results.', 'Error');
      })
  }
}])
.factory('HomePageFactory',['$http','$q','ConfigService',function($http,$q,ConfigService) {
    return {
        getHomePageResults: function(filterType,filterState) {
            var filterString = '';
            
            if (filterType) {
                filterString = '?projectType=' + filterType;
            }
            if (filterString) {
                if (filterState) {
                    filterString += '&availability=' + filterState;
                }
            } else {
                if (filterState) {
                    filterString = '?availability=' + filterState;
                }
            }
            var url = '';
            url = ConfigService.appRoot() + ConfigService.epProject() + filterString;
            var retVal = $q.defer();
            retVal.promise = $http({
                method: 'GET',
                url: url
            }).then(function(responseData) {
                retVal.resolve(responseData.data);
            }, function(err) {
                retVal.reject('Error retrieving producer information.')
            })
            return retVal.promise;
        }
    }
}])