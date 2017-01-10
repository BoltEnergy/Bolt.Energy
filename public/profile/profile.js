angular.module('profile',["ngAnimate", "ngTouch", "ui.router", "ngResource","toastr","bolt"])
.factory('ProfileFactory',['$http','$q','ConfigService',function($http,$q,ConfigService) {
  return {
    getUser: function(id) {
      var retVal = $q.defer();
      var url = ConfigService.appRoot() + ConfigService.epUser() + '/' + id;
      retVal.promise = $http({
        method: 'GET',
        url: url
      }).then(function(responseData) {
        retVal.resolve(responseData.data.user);
      }, function(err) {
        retVal.reject(err);
      });
      return retVal.promise;
    },
    updateUser: function(id, data) {
      var retVal = $q.defer();
      var url = ConfigService.appRoot() + ConfigService.epUser() + '/' + id;
      retVal.promise = $http({
        method: 'PUT',
        url: url,
        data: data
      }).then(function(responseData) {
        retVal.resolve(responseData.data.user);
      }, function(error) {
        retVal.reject(error);
      })
      return retVal.promise;
    },
    addUserImage: function(image,id) {
      var fd = new FormData();
      fd.append('files',image[0]);
      var retVal = $q.defer();
      var url = ConfigService.epUser() + '/' + id + '/images';
      retVal.promise = $http({
        method: 'POST',
        url: url,
        data: fd,
        headers: {
            'Content-Type': undefined
        }
      }).then(function(responseData) {
        retVal.resolve(responseData.data);
      }, function(err) {
        retVal.reject({ error: err });
      });
      return retVal.promise;
    },
    addProducerImage: function(image,id) {
      var fd = new FormData();
      fd.append('files',image[0]);
      var retVal = $q.defer();
      var url = ConfigService.epProducer() + '/' + id + '/images';
      retVal.promise = $http({
        method: 'POST',
        url: url,
        data: fd,
        headers: {
            'Content-Type': undefined
        }
      }).then(function(responseData) {
        retVal.resolve(responseData.data);
      }, function(err) {
        retVal.reject({ error: err });
      });
      return retVal.promise;
    },
    addProjectImage: function(image,id) {
      var fd = new FormData();
      fd.append('files',image[0]);
      var retVal = $q.defer();
      var url = ConfigService.epProject() + '/' + id + '/images';
      retVal.promise = $http({
        method: 'POST',
        url: url,
        data: fd,
        headers: {
            'Content-Type': undefined
        }
      }).then(function(responseData) {
        retVal.resolve(responseData.data);
      }, function(err) {
        retVal.reject({ error: err });
      });
      return retVal.promise;
    },
    findProducerByOwner: function(id) {
      var retVal = $q.defer();
      var url = ConfigService.appRoot() + ConfigService.epProducer() + '?owner=' + id;
      retVal.promise = $http({
        method: 'GET',
        url: url
      }).then(function(responseData) {
        retVal.resolve(responseData.data.producers);
      }, function(err) {
        retVal.reject(err);
      });
      return retVal.promise;
    },
    addUpdate: function() {
      
    },
    saveProducer: function(id, producer) {
      var retVal = $q.defer();
      var url ='';
      if (id) {
        url = ConfigService.appRoot() + ConfigService.epProducer() + '/' + id;
        retVal.promise = $http({
          method: 'PUT',
          url: url,
          data: producer
        }).then(function(responseData) {
          retVal.resolve(responseData.data.producer);
        }, function(error) {
          retVal.reject(error);
        })
        return retVal.promise;  
      } else {
        url = ConfigService.appRoot() + ConfigService.epProducer();
        retVal.promise = $http({
          method: 'POST',
          url: url,
          data: producer
        }).then(function(responseData) {
          retVal.resolve(responseData.data.producer);
        }, function(error) {
          retVal.reject(error);
        })
        return retVal.promise;  
      }
    },
    getProject: function(id) {
      var retVal = $q.defer();
      var url = ConfigService.appRoot() + ConfigService.epProject() + '/' + id;
      retVal.promise = $http({
        method: 'GET',
        url: url
      }).then(function(responseData) {
        retVal.resolve(responseData.data);
      }, function(err) {
        retVal.reject(err);
      });
      return retVal.promise;
    },
    saveProject: function(producerId, id, project) {
      var retVal = $q.defer();
      var url ='';
      if (id) {
        url = ConfigService.appRoot() + ConfigService.epProducer() + '/' + producerId + "/projects/" + id;
        retVal.promise = $http({
          method: 'PUT',
          url: url,
          data: project
        }).then(function(responseData) {
          retVal.resolve(responseData.data.project);
        }, function(error) {
          retVal.reject(error);
        });
        return retVal.promise;  
      } else {
        url = ConfigService.appRoot() + ConfigService.epProducer() + '/' + producerId + "/projects/";
        retVal.promise = $http({
          method: 'POST',
          url: url,
          data: project
        }).then(function(responseData) {
          retVal.resolve(responseData.data.project);
        }, function(error) {
          retVal.reject(error);
        });
        return retVal.promise;  
      }
    },
    getUpdate: function(updateId) {
      var retVal = $q.defer();
      var url = ConfigService.appRoot() + ConfigService.epUpdate() + '/' + updateId;
      retVal.promise = $http({
        method: 'GET',
        url: url
      }).then(function(responseData) {
        retVal.resolve(responseData.data);
      }, function(err) {
        retVal.reject(err);
      });
      return retVal.promise;
    },
    saveUpdate: function(producerId, id, update) {
      var retVal = $q.defer();
      var url ='';
      if (id) {
        url = ConfigService.appRoot() + ConfigService.epProducer() + '/' + producerId + "/update/" + id;
        retVal.promise = $http({
          method: 'PUT',
          url: url,
          data: update
        }).then(function(responseData) {
          retVal.resolve(responseData.data.update);
        }, function(error) {
          retVal.reject(error);
        });
        return retVal.promise;  
      } else {
        url = ConfigService.appRoot() + ConfigService.epProducer() + '/' + producerId + "/update/";
        retVal.promise = $http({
          method: 'POST',
          url: url,
          data: update
        }).then(function(responseData) {
          retVal.resolve(responseData.data.update);
        }, function(error) {
          retVal.reject(error);
        });
        return retVal.promise;  
      }
    }
  }
}])
.controller('ProfileController', ['$scope','toastr','ProfileFactory','profileData','producerData','$state',function($scope,toastr,ProfileFactory,profileData,producerData,$state) {
  $scope.user = profileData;
  $scope.producer = (producerData && producerData.length > 0) ? producerData[0]: '';
  
  $scope.saveAccount = function() {
    ProfileFactory.updateUser($scope.user._id, $scope.user).then(function(responseData) {
      toastr.success('User updated.', 'Update');
      profileData = responseData;
    }, function(err) {
      toastr.error('Error saving user information update.', 'Error');
    })
  }
  $scope.uploadImage = function() {
    ProfileFactory.addUserImage($scope.uploads,profileData._id).then(function(responseData) {
      $state.go('profile', { id: $state.params.id }, { reload: true });
      toastr.success('User image uploaded.', 'Success');
    }, function(err) {
      toastr.error('Error uploading image.', 'Error');
    })
  }
  $scope.cancel = function() {
    $scope.user = profileData;
  }
}])
.controller('ProducerEditController', ['$scope','toastr','ProfileFactory','producerEdit','$state',function($scope,toastr,ProfileFactory,producerEdit,$state) {
  $scope.producer = producerEdit;
  
  $scope.saveProfile = function() {
    var id = ($state.params.producerId)?($state.params.producerId):(null);
    ProfileFactory.saveProducer(id,$scope.producer).then(function(responseData) {
      producerEdit = responseData;
      toastr.success('Producer profile saved.', 'Success');
    }, function(err) {
      toastr.error('Error saving producer.', 'Error');
    });
  }
  $scope.uploadImage = function() {
    ProfileFactory.addProducerImage($scope.uploads,producerEdit._id).then(function(responseData) {
      $state.go('producer_edit', { producerId: $state.params.producerId }, { reload: true });
      toastr.success('Producer image uploaded.', 'Success');
    }, function(err) {
      toastr.error('Error uploading image.', 'Error');
    })
  }
}])
.controller('ProjectEditController', ['$scope','toastr','ProfileFactory','projectEdit','$state',function($scope,toastr,ProfileFactory,projectEdit,$state) {
  $scope.project = projectEdit;
  $scope.saveProject = function() {
    var id = ($state.params.projectId)?($state.params.projectId):(null);
    ProfileFactory.saveProject($state.params.producerId,id,$scope.project).then(function(responseData) {
      projectEdit = responseData;
      toastr.success('Project profile saved.', 'Success');
    }, function(error) {
      toastr.error('Error saving project profile.', 'Error');
    });
  }
  $scope.uploadImage = function() {
    ProfileFactory.addProjectImage($scope.uploads,projectEdit._id).then(function(responseData) {
      $state.go('project_edit', { producerId: $state.params.producerId, projectId: $state.params.projectId }, { reload: true });
      toastr.success('Project image uploaded.', 'Success');
    }, function(err) {
      toastr.error('Error uploading image.', 'Error');
    })
  }
}])
.controller('UpdateEditController', ['$scope','toastr','ProfileFactory','updateEdit','$state',function($scope,toastr,ProfileFactory,updateEdit,$state) {
  $scope.update = updateEdit;
  $scope.saveUpdate = function() {
    var id = ($state.params.updateId)?($state.params.updateId):(null);
    ProfileFactory.saveUpdate($state.params.producerId,$state.params.updateId,$scope.update).then(function(responseData) {
      $scope.update = responseData;
      updateEdit = responseData;
      toastr.success('Update saved.', 'Success');
    }, function(error) {
      toastr.error('Error saving update.', 'Error');
    });
  }
}])