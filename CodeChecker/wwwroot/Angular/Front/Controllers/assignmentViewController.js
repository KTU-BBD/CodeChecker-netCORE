(function() {
    "use strict";
    //Getting the exsisting module
    angular.module("app-contest")
        .controller("assignmentViewController", assignmentViewController);
    function assignmentViewController($routeParams, $http, $scope, toastr, $sce) {
        var apiUrl = "/api/front/assignment/get/" + $routeParams.assignmentId;
        $scope.assignment = [];

        $http.get(apiUrl)
            .then(function (response) {
                angular.copy(response.data, $scope.assignment);
                $scope.assignment.description = $sce.trustAsHtml($scope.assignment.description);

            }, function (error) {
                toastr.error(error.data);
            });
        $scope.sendCode = function() {
            if (!$scope.task || !$scope.task.code) {
                toastr.error("Please select language and type your code");
                return;
            }

            $scope.task.assignmentId =  $routeParams.assignmentId;
            $scope.task.language = "PYT";



            if ($scope.task.code.length < 5) {
                toastr.error("Code too short");
                return;
            }

            $http.post("/api/front/assignment/submit", $scope.task)
                .then(function(response) {
                    toastr.success("Successfully passed all tests");
                }, function (error) {
                    toastr.error(error.data);
                });
        };
        console.log($scope.task);
    }
})();