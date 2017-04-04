angular.module('content', ["ngAnimate", "ngTouch", "ui.router", "ngResource", "toastr", "bolt"])
.controller('ContentController', ['$scope', 'ContentFactory', 'contentData', 'toastr', '$sce', function ($scope, ContentFactory, contentData, toastr, $sce) {
    if(contentData!=null)
    {
        contentData.pageContent = $sce.trustAsHtml(contentData.pageContent);
        $scope.content = contentData;
    }
}])
.factory('ContentFactory', ['$http', '$q', 'ConfigService', function ($http, $q, ConfigService) {
    return {
        getContent: function (vanityUrl) {
            var retVal = $q.defer();
            var url = ConfigService.appRoot() + ConfigService.epContent() + '/' + vanityUrl;
            retVal.promise = $http({
                method: 'GET',
                url: url
            }).then(function (responseData) {
                retVal.resolve(responseData.data.content);
            }, function (err) {
                retVal.reject(err);
            });
            return retVal.promise;
        }
    }
}])
