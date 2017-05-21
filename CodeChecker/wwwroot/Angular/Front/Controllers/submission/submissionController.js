(function() {
    "use strict";
    //Getting the exsisting module
    angular.module("app-contest")
        .controller("submissionController", submissionController);

    function submissionController($routeParams, $http, $scope, toastr, $uibModal,$uibModalStack) {
        var apiUrl = "/api/front/submission/contestSubmssions/" + $routeParams.contestId;

        $scope.refresh = function(hideToster) {
            $http.get(apiUrl)
                .then(function(response) {
                        angular.copy(response.data, $scope.submissions);
                        if (!hideToster) {
                            toastr.success("Reloaded data");
                        }
                    },
                    function(error) {
                        toastr.error(error.data);
                    });
        };


        $scope.viewSubmission = function(submissionId) {
            $uibModal.open({
                templateUrl: '/Html/Front/Modal/submissionView.html',
                animation: false,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                size: 'lg',
                controller: function($scope) {
                    $http.get("/api/front/submission/get/" + submissionId)
                        .then(function(response) {
                                $scope.submission = response.data;
                            },
                            function(error) {
                                toastr.error(error.data);
                            });

                    $scope.close = function() {
                        //Closing modal instance
                        var top = $uibModalStack.getTop();
                        if (top) {
                            $uibModalStack.dismiss(top.key);
                        }
                    };
                    $scope.submit = function() {
                    };
                }
            }).result.then(function() {
                },
                function(res) {
                });
        };

        $scope.refresh(true);

        $scope.submissions = [];

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
        }
    }
})();