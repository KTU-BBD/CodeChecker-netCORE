//main.js
angular
    .module('app')
    .controller('ProfileEditController',['$scope','Upload', 'Auth',function ($scope, Upload, Auth) {
            $scope.model = {};
            $scope.selectedFile = [];
            $scope.uploadProgress = 0;

            $scope.uploadFile = function (file) {
                Upload.upload({
                    url: 'api/admin/user/changeProfile',
                    data: {
                         profile: file,
                         userName: $scope.username,
                         email: $scope.email
                    }
                }).then(function(resp) {
                        Auth.updateUser();
                    });
            };

            $scope.onFileSelect = function ($files) {
                $scope.uploadProgress = 0;
                $scope.selectedFile = $files;
            };
        }
    ]);