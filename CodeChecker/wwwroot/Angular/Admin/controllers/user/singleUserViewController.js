angular
    .module('app')
    .controller('SingleUserViewController',function ($scope, Auth, $http, $state, $stateParams, Upload, toastr) {
        var userId = $stateParams.id;

        $http.get('/api/admin/user/get/' + userId)
            .then(function(response) {
                    $scope.profile = response.data;
                },
                function(error) {
                    toastr.error(error.data);
                });
        $scope.save = function () {
            $scope.profile.Userid = userId;
            Upload.upload({
                url: 'api/admin/user/changeProfile',
                data: $scope.profile
            }).then(function(resp) {
                Auth.updateUser();
                toastr.success(resp.data);
            }, function (resp) {
                toastr.error(resp.data);
            });
        };

        $scope.changeRole = function(role) {
            var data = {
                userId: userId,
                role: role
            };

            $http.post('api/admin/user/changeRole', data)
                .then(function (response) {
                    toastr.success(response.data);
                }, function(response) {
                    toastr.error(response.data);
                });
        };

        $scope.changeLock = function(lock) {
            $('.unlock, .lock').attr('disabled', 'disabled');
            var data = {
                userId: userId,
                lock: lock
            };

            $http.post('api/admin/user/changeLock', data)
                .then(function (response) {
                    if (lock) {
                        $(".unlock").removeClass("ng-hide");
                        $(".lock").addClass("ng-hide");
                    } else {
                        $(".unlock").addClass("ng-hide");
                        $(".lock").removeClass("ng-hide");
                    }
                    $('.unlock, .lock').removeAttr('disabled');
                    toastr.success(response.data);
                }, function(response) {
                    toastr.error(response.data);
                });
        }
    });