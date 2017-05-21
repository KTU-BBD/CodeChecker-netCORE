(function() {
    "use strict";
    //Getting the exsisting module
    angular.module("app-submission")
        .controller("submissionAllController", submissionAllController);

    function submissionAllController($http, $scope, toastr, $uibModal, $uibModalStack) {
        var apiUrl = "/api/front/submission/all";
        $scope.submissions = [];

        $scope.refresh = function(hideToster) {
            $http.get(apiUrl)
                .then(function(response) {
                        angular.copy(response.data, $scope.submissions);
                        if (!hideToster) {
                            toastr.success("Data reloaded");
                        }
                    },
                    function(error) {
                        toastr.error(error.data);
                    });
        };

        $scope.refresh(true);


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
            }, function(res) {
            });
        }
    }
})();