angular.module('producer',["ngAnimate", "ngTouch", "ui.router", "ngResource","toastr","bolt"])
.controller('ProducerController', ['$scope','$rootScope','toastr','$q','producer','ProducerFactory','$state',function($scope,$rootScope,toastr,$q,producer,ProducerFactory,$state){
  $scope.producer = producer;
  $scope.producerOwner = (producer.owner._id == $rootScope.currentUserId);
  //$scope.activeTab = 'tab1';
  $scope.activeTab = $state.params.activeTab || "tab1";
  
  $scope.vidSrc = "userImages/5828e1dbd3c68ccf542c4e53/1479357268973_WIN_20161116_23_32_18_Pro.mp4"
    
  if (producer.projects.length >= 3) {
    var contentRows = Math.floor(producer.projects.length/3);
    var pCnt = 0;
    var out = [];
    for (var iCnt = 0; iCnt < contentRows; iCnt++) {
      out[iCnt] = [];
      out[iCnt].push(producer.projects[pCnt]);
      pCnt += 1;
      out[iCnt].push(producer.projects[pCnt]);
      pCnt += 1;
      out[iCnt].push(producer.projects[pCnt]);
    }
    $scope.rows = out;
  } else {
    var pCnt = producer.projects.length;
    var out = [];
    out[0] = [];
    for (var iCnt = 0; iCnt < pCnt; iCnt++) {
      out[0].push(producer.projects[iCnt]);
    }
    $scope.rows = out;
  }

  $scope.activateTab = function(tab) {
    $scope.activeTab = tab;
  }
  $scope.showProject = function(id) {
    $scope.single = true;
    for (var i = 0; i < producer.projects.length; i++) {
      if (producer.projects[i]._id == id) {
        $scope.project = producer.projects[i];
      }
    }
  }
  $scope.showAll = function() {
    $scope.single = false;
  }
  
  $scope.direction = 'left';
  $scope.currentIndex = 0;

  $scope.setCurrentSlideIndex = function (index) {
      $scope.direction = (index > $scope.currentIndex) ? 'left' : 'right';
      $scope.currentIndex = index;
  };

  $scope.isCurrentSlideIndex = function (index) {
      return $scope.currentIndex === index;
  };

  $scope.prevSlide = function () {
      $scope.direction = 'left';
      $scope.currentIndex = ($scope.currentIndex < $scope.producer.uploads.length - 1) ? ++$scope.currentIndex : 0;
  };

  $scope.nextSlide = function () {
      $scope.direction = 'right';
      $scope.currentIndex = ($scope.currentIndex > 0) ? --$scope.currentIndex : $scope.producer.uploads.length - 1;
  };
  
  
  $scope.projectDirection = 'left';
  $scope.currentProjectIndex = 0;

  $scope.setCurrentProjectSlideIndex = function (index) {
      $scope.projectDirection = (index > $scope.currentProjectIndex) ? 'left' : 'right';
      $scope.currentProjectIndex = index;
  };

  $scope.isCurrentProjectSlideIndex = function (index) {
      return $scope.currentProjectIndex === index;
  };

  $scope.prevProjectSlide = function () {
      $scope.projectDirection = 'left';
      $scope.currentProjectIndex = ($scope.currentProjectIndex < $scope.project.uploads.length - 1) ? ++$scope.currentProjectIndex : 0;
  };

  $scope.nextProjectSlide = function () {
      $scope.projectDirection = 'right';
      $scope.currentProjectIndex = ($scope.currentProjectIndex > 0) ? --$scope.currentProjectIndex : $scope.project.uploads.length - 1;
  };
  
  $scope.postComment = function() {
    $scope.comment.postedBy = $rootScope.currentUserId;
    ProducerFactory.postComment($state.params.producerId, $scope.comment).then(function(responseData) {
      $state.go('producer', { producerId: $state.params.producerId, activeTab: 'tab3' }, { reload: true });
      toastr.success('Comment posted.', 'Success');
    });
  }
    
  $scope.postReply = function(commentId) {
    $scope.comment.reply.postedBy = $rootScope.currentUserId;
    ProducerFactory.postReply($state.params.producerId, commentId, $scope.comment.reply).then(function(responseData) {
      $state.go('producer', { producerId: $state.params.producerId }, { reload: true });
      toastr.success('Reply posted.', 'Success');
    });
  }
  
  $scope.comment = {};
  $scope.comment.reply = {};
}])
.factory('ProducerFactory', ['$http','$q','ConfigService',function($http,$q,ConfigService) {
  return {
      getProducer: function(id) {
        if (id) {
          var retVal = $q.defer();
          var url = ConfigService.appRoot() + ConfigService.epProducer() + '/' + id
          retVal.promise = $http({
            method: 'GET',
            url :url
          }).then(function(responseData) {
            retVal.resolve(responseData.data.producer);
          }, function(error) {
            retVal.resolve(error);
          });
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
      },
      postComment: function(producerId, comment) {
        var retVal = $q.defer();
        retVal.promiae = $http({
          method: 'POST',
          url: ConfigService.appRoot() + ConfigService.epProducer() + '/' + producerId + '/comment',
          data: comment
        }).then(function(responseData) {
          retVal.resolve(responseData);
        }, function(err) {
          retVal.reject(err);
        });
        return retVal.promise;
      },
      postReply: function(producerId, commentId, comment) {
        var retVal = $q.defer();
        retVal.promiae = $http({
          method: 'POST',
          url: ConfigService.appRoot() + ConfigService.epProducer() + '/' + producerId + '/comment/' + commentId + '/reply',
          data: comment
        }).then(function(responseData) {
          retVal.resolve(responseData);
        }, function(err) {
          retVal.reject(err);
        });
        return retVal.promise;
      }
    }
  }
])