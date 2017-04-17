//main.js
angular
    .module('app')
    .controller('ProfileEditController',['$scope','Upload',function ($scope, Upload) {
            $scope.model = {};
            $scope.selectedFile = [];
            $scope.uploadProgress = 0;

            $scope.uploadFile = function () {
                var file = $scope.selectedFile[0];
                $scope.upload = Upload.upload({
                    url: 'api/admin/util/changeProfile',
                    method: 'POST',
                    data: angular.toJson($scope.model),
                    file: file
                }).success(function (data) {
                    //do something
                });
            };

            $scope.onFileSelect = function ($files) {
                $scope.uploadProgress = 0;
                $scope.selectedFile = $files;
            };
        }
    ]);