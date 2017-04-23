(function() {
    "use strict";
    //Getting the exsisting module
    angular.module("app-contest")
        .controller("contestViewController", contestViewController);
    function contestViewController($routeParams, $http, $scope, toastr, $sce) {
        var apiUrl = "/api/front/contest/get/" + $routeParams.contestId;
        $scope.contest = [];
        $http.get(apiUrl)
            .then(function (response) {
                angular.copy(response.data, $scope.contest);
                $scope.contest.description = $sce.trustAsHtml($scope.contest.description);
            }, function (error) {
                toastr.error(error.data);
            });
    }
})();