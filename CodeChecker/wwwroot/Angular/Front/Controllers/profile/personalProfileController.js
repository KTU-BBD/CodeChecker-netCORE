(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app-profile")
        .controller("personalProfileController", function($routeParams, $scope, $http, toastr, Upload) {
            $http.get('/api/front/user/current').then(function (data) {
                $scope.profile = data.data;
                $scope.profile.firstName = $scope.profile.firstName ? $scope.profile.firstName : "";
                $scope.profile.lastName = $scope.profile.lastName ? $scope.profile.lastName : "";
            }, function(response) {
                toastr.error(response.data);
            });

            $scope.save = function () {
                Upload.upload({
                    url: 'api/front/user/update',
                    data: $scope.profile
                }).then(function(resp) {
                    toastr.success(resp.data);
                }, function (resp) {
                    toastr.error(resp.data);
                });
            };

            $scope.changePassword = function() {
                if (!$scope.password  || !$scope.password.currentPassword || !$scope.password.password || !$scope.password.confirmPassword) {
                    toastr.error("Enter data required for password change");
                    return;
                }

                $http.post('/api/front/user/changePassword', $scope.password).then(function (data) {
                    toastr.success(data.data);
                }, function(response) {
                    toastr.error(response.data);
                });
            }
        });
})();

