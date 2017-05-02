(function() {
    "use strict";
    //Getting the exsisting module
    angular.module("app-contest")
        .controller("assignmentViewController", assignmentViewController);
    function assignmentViewController($routeParams, $http, $scope, toastr, $sce) {
        var apiUrl = "/api/front/assignment/get/" + $routeParams.assignmentId;
        $scope.assignment = [];
        $scope.sendDisabled = false;

        $http.get(apiUrl)
            .then(function (response) {
                angular.copy(response.data, $scope.assignment);
                $scope.assignment.description = $sce.trustAsHtml($scope.assignment.description);
                $scope.task = {
                    language: $scope.assignment.lastSubmission.language,
                    code: $scope.assignment.lastSubmission.code
                };
            }, function (error) {
                toastr.error(error.data);
            });

        $scope.sendCode = function() {
            $scope.sendDisabled = true;
            if (!$scope.task || !$scope.task.code) {
                toastr.error("Please select language and type your code");
                $scope.sendDisabled = false;
                return;
            }

            $scope.task.assignmentId =  $routeParams.assignmentId;
            $scope.task.language = "PYT";

            if ($scope.task.code.length < 5) {
                toastr.error("Code too short");
                $scope.sendDisabled = false;
                return;
            }

            $http.post("/api/front/assignment/submit", $scope.task)
                .then(function() {
                    toastr.success("Your code has been submited for review");
                    $scope.sendDisabled = true;
                }, function (error) {
                    toastr.error(error.data);
                    $scope.sendDisabled = false;
                });
        };
    }
})();