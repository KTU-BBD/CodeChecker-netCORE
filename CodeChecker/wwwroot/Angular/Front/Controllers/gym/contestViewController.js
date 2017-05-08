(function() {
    "use strict";
    //Getting the exsisting module
    angular.module("app-gym")
        .controller("gymViewController", contestViewController);
    function contestViewController($routeParams, $http, $scope, toastr, $sce) {
        var apiUrl = "/api/front/gym/get/" + $routeParams.contestId;
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