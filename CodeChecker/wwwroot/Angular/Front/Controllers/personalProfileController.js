(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app-profile")
        .controller("personalProfileController", function($routeParams, $scope, $http, toastr) {
            $http.get('/api/front/user/get/' + $routeParams.username).then(function (data) {
                $scope.profileUser = data.data;
            }, function(response) {
                toastr.error(response.data);
            });


        });
})();

