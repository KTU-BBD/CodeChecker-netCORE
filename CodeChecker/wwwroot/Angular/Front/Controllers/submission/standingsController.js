(function() {
    "use strict";
    //Getting the exsisting module
    angular.module("app-contest")
        .controller("standingsController", standingsController);

    function standingsController($routeParams, $http, $scope, toastr) {
        var apiUrl = "/api/front/submission/getByContest/" + $routeParams.contestId;
        $scope.contest = [];
        $http.get(apiUrl)
            .then(function(response) {
                    angular.copy(response.data, $scope.contest);
                },
                function(error) {
                    toastr.error(error.data);
                });
        $scope.points = function(submissions, assignmentId) {
            var points = "-";

            for (var i = 0; i < submissions.length; i++) {
                if (submissions[i].verdict === 1 &&
                    submissions[i].assignment &&
                    submissions[i].assignment.id === assignmentId) {
                    return submissions[i].points;
                }
            }

            return points;
        };

        $scope.totalSum = function(submissions, assignments) {
            var sum = 0;
            for (var i = 0; i < submissions.length; i++) {
                for (var j = 0; j < assignments.length; j++) {
                    if (submissions[i].verdict === 1  && submissions[i].assignment && submissions[i].assignment.id === assignments[j].id) {
                        sum += submissions[i].points;
                        break;
                    }
                }
            }
            return sum;
        }

//        $scope.points
    }
})();