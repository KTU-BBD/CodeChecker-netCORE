(function() {
    "use strict";
    //Getting the exsisting module
    angular.module("app-contest")
        .controller("contestViewController", contestViewController);
    function contestViewController($routeParams, $http, $scope, toastr) {
        var apiUrl = "/api/front/contest/get/" + $routeParams.contestId;
        $scope.contest = [];
        $http.get(apiUrl)
            .then(function (response) {
                console.log(response.data);
                angular.copy(response.data, $scope.contest);
            }, function (error) {
                toastr.error(error.data);
            });
    }
})();