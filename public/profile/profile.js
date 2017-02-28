angular.module('profile', ["ngAnimate", "ngTouch", "ui.router", "ngResource", "toastr", "bolt"])
.factory('ProfileFactory', ['$http', '$q', 'ConfigService', function ($http, $q, ConfigService) {
    return {
        validateUser: function (email, password) {
            var $return = $q.defer();
            $return.promise = $http({
                method: 'POST',
                url: ConfigService.appRoot() + '/data/authenticate',
                data: {
                    email: email,
                    password: password
                }
            }).then(function (responseData) {
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
        getUser: function (id) {
            var retVal = $q.defer();
            var url = ConfigService.appRoot() + ConfigService.epUser() + '/' + id;
            retVal.promise = $http({
                method: 'GET',
                url: url
            }).then(function (responseData) {
                retVal.resolve(responseData.data.user);
            }, function (err) {
                retVal.reject(err);
            });
            return retVal.promise;
        },
        updateUser: function (id, data) {
            var retVal = $q.defer();
            var url = ConfigService.appRoot() + ConfigService.epUser() + '/' + id;
            retVal.promise = $http({
                method: 'PUT',
                url: url,
                data: data
            }).then(function (responseData) {
                retVal.resolve(responseData.data.user);
            }, function (error) {
                retVal.reject(error);
            })
            return retVal.promise;
        },
        removeUserAccount: function (id, data) {
            var retVal = $q.defer();
            var url = ConfigService.appRoot() + ConfigService.epUser() + '/' + id;
            retVal.promise = $http({
                method: 'DELETE',
                url: url,
                data: data
            }).then(function (responseData) {
                retVal.resolve(responseData.data.user);
            }, function (error) {
                retVal.reject(error);
            })
            return retVal.promise;
        },
        addUserImage: function (image, id) {
            var fd = new FormData();
            fd.append('files', image[0]);
            var retVal = $q.defer();
            var url = ConfigService.epUser() + '/' + id + '/images';
            retVal.promise = $http({
                method: 'POST',
                url: url,
                data: fd,
                headers: {
                    'Content-Type': undefined
                }
            }).then(function (responseData) {
                retVal.resolve(responseData.data);
            }, function (err) {
                retVal.reject({ error: err });
            });
            return retVal.promise;
        },
        deleteUserImage: function (imageId, id) {
            var fd = new FormData();
            var retVal = $q.defer();
            var url = ConfigService.epUser() + '/' + id + '/images' + '/' + imageId;
            retVal.promise = $http({
                method: 'DELETE',
                url: url,
                data: fd,
                headers: {
                    'Content-Type': undefined
                }
            }).then(function (responseData) {
                retVal.resolve(responseData.data);
            }, function (err) {
                retVal.reject({ error: err });
            });
            return retVal.promise;
        },
        addProducerImage: function (image, id) {
            var fd = new FormData();
            fd.append('files', image[0]);
            var retVal = $q.defer();
            var url = ConfigService.epProducer() + '/' + id + '/images';
            retVal.promise = $http({
                method: 'POST',
                url: url,
                data: fd,
                headers: {
                    'Content-Type': undefined
                }
            }).then(function (responseData) {
                retVal.resolve(responseData.data);
            }, function (err) {
                retVal.reject({ error: err });
            });
            return retVal.promise;
        },
        deleteProducerImage: function (imageId, id) {
            var fd = new FormData();
            var retVal = $q.defer();
            var url = ConfigService.epProducer() + '/' + id + '/images' + '/' + imageId;
            retVal.promise = $http({
                method: 'DELETE',
                url: url,
                data: fd,
                headers: {
                    'Content-Type': undefined
                }
            }).then(function (responseData) {
                retVal.resolve(responseData.data);
            }, function (err) {
                retVal.reject({ error: err });
            });
            return retVal.promise;
        },
        addProjectImage: function (image, id) {
            var fd = new FormData();
            fd.append('files', image[0]);
            var retVal = $q.defer();
            var url = ConfigService.epProject() + '/' + id + '/images';
            retVal.promise = $http({
                method: 'POST',
                url: url,
                data: fd,
                headers: {
                    'Content-Type': undefined
                }
            }).then(function (responseData) {
                retVal.resolve(responseData.data);
            }, function (err) {
                retVal.reject({ error: err });
            });
            return retVal.promise;
        },
        deleteProjectImage: function (imageId, id) {
            var fd = new FormData();
            var retVal = $q.defer();
            var url = ConfigService.epProject() + '/' + id + '/images' + '/' + imageId;
            retVal.promise = $http({
                method: 'DELETE',
                url: url,
                data: fd,
                headers: {
                    'Content-Type': undefined
                }
            }).then(function (responseData) {
                retVal.resolve(responseData.data);
            }, function (err) {
                retVal.reject({ error: err });
            });
            return retVal.promise;
        },
        findProducerByOwner: function (id) {
            var retVal = $q.defer();
            var url = ConfigService.appRoot() + ConfigService.epProducer() + '?owner=' + id;
            retVal.promise = $http({
                method: 'GET',
                url: url
            }).then(function (responseData) {
                retVal.resolve(responseData.data.producers);
            }, function (err) {
                retVal.reject(err);
            });
            return retVal.promise;
        },
        addUpdate: function () {

        },
        saveProducer: function (id, producer) {
            var retVal = $q.defer();
            var url = '';
            if (id) {
                url = ConfigService.appRoot() + ConfigService.epProducer() + '/' + id;
                retVal.promise = $http({
                    method: 'PUT',
                    url: url,
                    data: producer
                }).then(function (responseData) {
                    retVal.resolve(responseData.data.producer);
                }, function (error) {
                    retVal.reject(error);
                })
                return retVal.promise;
            } else {
                url = ConfigService.appRoot() + ConfigService.epProducer();
                retVal.promise = $http({
                    method: 'POST',
                    url: url,
                    data: producer
                }).then(function (responseData) {
                    retVal.resolve(responseData.data.producer);
                }, function (error) {
                    retVal.reject(error);
                })
                return retVal.promise;
            }
        },
        getProject: function (id) {
            var retVal = $q.defer();
            var url = ConfigService.appRoot() + ConfigService.epProject() + '/' + id;
            retVal.promise = $http({
                method: 'GET',
                url: url
            }).then(function (responseData) {
                retVal.resolve(responseData.data);
            }, function (err) {
                retVal.reject(err);
            });
            return retVal.promise;
        },
        getProducerProject: function (producerId) {
            var retVal = $q.defer();
            var url = ConfigService.appRoot() + ConfigService.epProducer() + '/' + producerId;
            retVal.promise = $http({
                method: 'GET',
                url: url
            }).then(function (responseData) {
                retVal.resolve(responseData.data);
            }, function (err) {
                retVal.reject(err);
            });
            return retVal.promise;
        },
        saveProject: function (producerId, id, project) {
            var retVal = $q.defer();
            var url = '';
            if (id) {
                url = ConfigService.appRoot() + ConfigService.epProducer() + '/' + producerId + "/projects/" + id;
                retVal.promise = $http({
                    method: 'PUT',
                    url: url,
                    data: project
                }).then(function (responseData) {
                    retVal.resolve(responseData.data.project);
                }, function (error) {
                    retVal.reject(error);
                });
                return retVal.promise;
            } else {
                url = ConfigService.appRoot() + ConfigService.epProducer() + '/' + producerId + "/projects/";
                retVal.promise = $http({
                    method: 'POST',
                    url: url,
                    data: project
                }).then(function (responseData) {
                    retVal.resolve(responseData.data.project);
                }, function (error) {
                    retVal.reject(error);
                });
                return retVal.promise;
            }
        },
        getUpdate: function (updateId) {
            var retVal = $q.defer();
            var url = ConfigService.appRoot() + ConfigService.epUpdate() + '/' + updateId;
            retVal.promise = $http({
                method: 'GET',
                url: url
            }).then(function (responseData) {
                retVal.resolve(responseData.data);
            }, function (err) {
                retVal.reject(err);
            });
            return retVal.promise;
        },
        saveUpdate: function (producerId, id, update) {
            var retVal = $q.defer();
           update.visible= true;
            var url = '';
            if (id) {                 
                url = ConfigService.appRoot() + ConfigService.epProducer() + '/' + producerId + "/update/" + id;
                retVal.promise = $http({
                    method: 'PUT',
                    url: url,
                    data: update
                }).then(function (responseData) {
                    retVal.resolve(responseData.data.update);
                }, function (error) {
                    retVal.reject(error);
                });
                return retVal.promise;
            } else {
                url = ConfigService.appRoot() + ConfigService.epProducer() + '/' + producerId + "/update/";
                retVal.promise = $http({
                    method: 'POST',
                    url: url,
                    data: update
                }).then(function (responseData) {
                    retVal.resolve(responseData.data.update);
                }, function (error) {
                    retVal.reject(error);
                });
                return retVal.promise;
            }
        }
    }
}])
.controller('ProfileController', ['$scope', 'toastr', 'ProfileFactory', 'profileData', 'producerData', '$state', function ($scope, toastr, ProfileFactory, profileData, producerData, $state) {
    $scope.user = profileData;
    $scope.currentEmail = profileData.email;
    $scope.producer = (producerData && producerData.length > 0) ? producerData[0] : '';

    $scope.saveAccount = function () {
        if ($scope.user.email == null || $scope.user.email.length < 5) {
            toastr.warning('Please enter your email.', 'Alert');
        }
        else if ($scope.currentPassword == "" || $scope.currentPassword == null) {
            toastr.warning('Please enter your current password.', 'Alert');
        }
        else if ($scope.newPwd != null && $scope.newPwd.length > 0 && $scope.newPwd != $scope.newPwdConfirm) {
            toastr.error('New password does not match with confirm new password', 'Error');
        }
        else if ($scope.currentPassword != "") {
            ProfileFactory.validateUser($scope.currentEmail, $scope.currentPassword).then(function (responseData) {
                $scope.user.password = $scope.newPwd ? $scope.newPwd : $scope.user.password;
                ProfileFactory.updateUser($scope.user._id, $scope.user).then(function (responseData) {
                    toastr.success('User updated.', 'Update');
                    profileData = responseData;
                    $scope.currentPassword = ""; $scope.newPwd = ""; $scope.newPwdConfirm = "";
                }, function (err) {
                    if (err.data.error.message.indexOf('duplicate') > 0) {
                        toastr.error('Email ID already exists. Please choose another email', 'Alert');
                        $scope.currentPassword = ""; $scope.newPwd = ""; $scope.newPwdConfirm = "";
                    }
                    else {
                        toastr.error('Error saving user information update.', 'Error');
                        $scope.currentPassword = ""; $scope.newPwd = ""; $scope.newPwdConfirm = "";
                    }
                })
            }, function (err) {
                toastr.error('Current password does not match', 'Error');
                $scope.currentPassword = ""; $scope.newPwd = ""; $scope.newPwdConfirm = "";
            })
        }
    }
    $scope.uploadImage = function () {
        ProfileFactory.addUserImage($scope.uploads, profileData._id).then(function (responseData) {
            $state.go('profile', { id: $state.params.id }, { reload: true });
            toastr.success('User image uploaded.', 'Success');
        }, function (err) {
            toastr.error('Error uploading image.', 'Error');
        })
    }
    $scope.cancel = function () {
        $scope.user = profileData;
    }
    $scope.changePwd = false;
    $scope.showHideNewPwd = function () {
        $scope.newPwd = "";
        $scope.newPwdConfirm = "";
        $scope.changePwd = !$scope.changePwd;
    }
    $scope.removeUser = function (id) {
        if (confirm("Are you sure you want to delete your account?")) {
            if ($scope.currentPassword == "" || $scope.currentPassword == null) {
                toastr.warning('Please enter your current password.', 'Alert');
            }
            else if ($scope.currentPassword != "") {
                ProfileFactory.validateUser($scope.currentEmail, $scope.currentPassword).then(function (responseData) {
                    ProfileFactory.removeUserAccount($scope.user._id, $scope.user).then(function (responseData) {
                        toastr.info('User account deleted.', 'Message');
                        $state.go('home', { reload: true });
                        //profileData = responseData;
                    }, function (err) {
                        console.log(err);
                        toastr.error('Error deleting user account.', 'Error');
                    })
                    //toastr.info("Account delete functionality is in progress...");

                }, function (err) {
                    toastr.error('Current password does not match', 'Error');
                })
            }
        }
        $scope.currentPassword = ""; $scope.newPwd = ""; $scope.newPwdConfirm = "";
    }
    $scope.deleteImage = function (imageId) {
        if (confirm("Confirm to delete your image?")) {
            ProfileFactory.deleteUserImage(imageId, $scope.user._id).then(function (responseData) {
                $state.go('profile', { id: $scope.user._id }, { reload: true });
                toastr.success('User image deleted.', 'Success');
            }, function (err) {
                console.log(err);
                toastr.error('Error deleting image.', 'Error');
            })
        }
    }
}])
.controller('ProducerEditController', ['$scope', 'toastr', 'ProfileFactory', 'producerEdit', '$state', function ($scope, toastr, ProfileFactory, producerEdit, $state) {
    $scope.producer = producerEdit;
    
    $scope.saveProfile = function () {
        var id = ($state.params.producerId) ? ($state.params.producerId) : (null);
        ProfileFactory.saveProducer(id, $scope.producer).then(function (responseData) {
            producerEdit = responseData;
            toastr.success('Producer profile saved.', 'Success');
        }, function (err) {
            toastr.error('Error saving producer.', 'Error');
        });
    }
    $scope.uploadImage = function () {
        ProfileFactory.addProducerImage($scope.uploads, producerEdit._id).then(function (responseData) {
            $state.go('producer_edit', { producerId: $state.params.producerId }, { reload: true });
            toastr.success('Producer image uploaded.', 'Success');
        }, function (err) {
            toastr.error('Error uploading image.', 'Error');
        })
    }
    $scope.deleteImage = function (imageId) {
        if (confirm("Confirm to delete your image?")) {
            ProfileFactory.deleteProducerImage(imageId, producerEdit._id).then(function (responseData) {
                $state.go('producer_edit', { producerId: $state.params.producerId }, { reload: true });
                toastr.success('Producer image deleted.', 'Success');
            }, function (err) {
                console.log(err);
                toastr.error('Error deleting image.', 'Error');
            })
        }
    }
}])
.controller('ProjectEditController', ['$scope', 'toastr', 'ProfileFactory', 'projectEdit', '$state', function ($scope, toastr, ProfileFactory, projectEdit, $state) {
    $scope.project = projectEdit[1];
    $scope.producer = projectEdit[0];
    $scope.saveProject = function () {
        if($scope.project.name==null || $scope.project.name=="")
        {
            toastr.error('Please enter project name.', 'Error');
        }
        else
        {
            var id = ($state.params.projectId) ? ($state.params.projectId) : (null);
            ProfileFactory.saveProject($state.params.producerId, id, $scope.project).then(function (responseData) {
                projectEdit[1] = responseData;
                $state.go('project_edit', { producerId: $state.params.producerId, projectId: responseData._id }, { reload: true });
                toastr.success('Project profile saved.', 'Success');
            }, function (error) {
                toastr.error('Error saving project profile.', 'Error');
            });
        }
    }
    $scope.uploadImage = function () {
        ProfileFactory.addProjectImage($scope.uploads, projectEdit[1]._id).then(function (responseData) {
            $state.go('project_edit', { producerId: $state.params.producerId, projectId: $state.params.projectId }, { reload: true });
            toastr.success('Project image uploaded.', 'Success');
        }, function (err) {
            toastr.error('Error uploading image.', 'Error');
        })
    }
    $scope.addNewProject = function () {
        $state.go('project_edit', { producerId: $state.params.producerId, projectId: '' }, { reload: true });
    }
    $scope.deleteImage = function (imageId) {
        if (confirm("Confirm to delete project image?")) {
            ProfileFactory.deleteProjectImage(imageId, projectEdit[1]._id).then(function (responseData) {
                $state.go('project_edit', { producerId: $state.params.producerId, projectId: $state.params.projectId }, { reload: true });
                toastr.success('Project image deleted.', 'Success');
            }, function (err) {
                toastr.error('Error deleting image.', 'Error');
            })
        }
    }
}])
.controller('UpdateEditController', ['$scope', 'toastr', 'ProfileFactory', 'updateEdit', '$state', function ($scope, toastr, ProfileFactory, updateEdit, $state) {
    $scope.update = updateEdit[1];
    $scope.listProjects = updateEdit[0].projects;
    $scope.saveUpdate = function () {
      //  var op = $scope.update.projectid;
        var id = ($state.params.updateId) ? ($state.params.updateId) : (null);
        $scope.update.projectname = "";
        if ($scope.update.projectid !=null && $scope.update.projectid.length > 5)
            $scope.update.projectname = $("#projects option:selected").text();
        ProfileFactory.saveUpdate($state.params.producerId, $state.params.updateId, $scope.update).then(function (responseData) {
            $scope.update = responseData;
            updateEdit = responseData;
            $state.go('update_edit', { producerId: $state.params.producerId, updateId: responseData._id }, { reload: true });
            toastr.success('Update saved.', 'Success');
        }, function (error) {
            toastr.error('Error saving update.', 'Error');
        });
    }
}])