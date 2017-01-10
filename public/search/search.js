angular.module('search',["ngAnimate", "ngTouch", "ui.router", "ngResource","toastr","bolt"])
.controller('SearchController', ['$scope','toastr','$q','SearchFactory',function($scope,toastr,$q,SearchFactory){
    var producerRows = [];
    var projectRows = [];
    producerRows[0] = [];
    projectRows[0] = [];
    $scope.producerRows = producerRows;
    $scope.projectRows = projectRows;
    
    $scope.search = function() {
      var producers = SearchFactory.searchProducer($scope.searchTerm);
      var projects = SearchFactory.searchProject($scope.searchTerm);
      $q.all([producers,projects]).then(function(responseData) {
        var prods = responseData[0].data;
        var projs = responseData[1].data;
        if (prods.length >= 3) {
          var contentRows = Math.floor(prods.length/3);
          var pCnt = 0;
          var out = [];
          for (var iCnt = 0; iCnt < contentRows; iCnt++) {
            out[iCnt] = [];
            out[iCnt].push(prods[pCnt]);
            pCnt += 1;
            out[iCnt].push(prods[pCnt]);
            pCnt += 1;
            out[iCnt].push(prods[pCnt]);
          }
          $scope.producerRows = out;
       } else {
          var pCnt = prods.length;
          var out = [];
          out[0] = [];
          for (var iCnt = 0; iCnt < pCnt; iCnt++) {
            out[0].push(prods[iCnt]);
          }
          $scope.producerRows = out;
       }
       
        if (projs.length >= 3) {
          var contentRows = Math.floor(projs.length/3);
          var pCnt = 0;
          var out = [];
          for (var iCnt = 0; iCnt < contentRows; iCnt++) {
            out[iCnt] = [];
            out[iCnt].push(projs[pCnt]);
            pCnt += 1;
            out[iCnt].push(projs[pCnt]);
            pCnt += 1;
            out[iCnt].push(projs[pCnt]);
          }
          $scope.projectRows = out;
       } else {
          var pCnt = projs.length;
          var out = [];
          out[0] = [];
          for (var iCnt = 0; iCnt < pCnt; iCnt++) {
            out[0].push(projs[iCnt]);
          }
          $scope.projectRows = out;
       }
       
       toastr.success('Search results update.','Update');
      }, function(err) {
        toastr.error('Error updating search results.', 'Error');
        console.log(err);
      })
    }
}])
.factory('SearchFactory', ['$http','$q','ConfigService',function($http,$q,ConfigService) {
  return {
    searchProducer: function(term) {
      var retVal = $q.defer();
      var url = ConfigService.appRoot() + ConfigService.epSearchProducer() + '/' + term;
      retVal.promise = $http({
        method: 'GET',
        url: url
      }).then(function(responseData) {
        retVal.resolve(responseData);
      }, function(err) {
        retVal.reject({ message: 'Error getting search results', error: err });
      });
      return retVal.promise;
    },
    searchProject: function(term) {
      var retVal = $q.defer();
      var url = ConfigService.appRoot() + ConfigService.epSearchProject() + '/' + term;
      retVal.promise = $http({
        method: 'GET',
        url: url
      }).then(function(responseData) {
        retVal.resolve(responseData);
      }, function(err) {
        retVal.reject({ message: 'Error getting search results', error: err });
      });
      return retVal.promise;
    }
  }
}])