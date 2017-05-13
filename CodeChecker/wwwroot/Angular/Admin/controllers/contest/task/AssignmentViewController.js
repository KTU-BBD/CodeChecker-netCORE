
(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app")
        .controller("AssignmentViewController", AssignmentViewController);

    function AssignmentViewController($state, $stateParams, $http, toastr, $uibModal, $uibModalStack) {
        var awc = this;
        awc.notBusy = false;
        var assgnId = $stateParams.id;
        var apiUrl = "/api/admin/Assignment/GetFull/" + assgnId.toString();
        var updateAssignmentUrl = "/api/admin/Assignment/Update";
        var deleteAssignmentUrl = "/api/admin/Assignment/Delete";
        var updateTest = "/api/admin/Input/UpdateTest";
        var deleteTest = "api/admin/Input/DeleteTest"
        var createTest = "api/admin/Assignment/CreateTest/"
        awc.showContent = false;
        


        awc.getContent = function () {
            $http.get(apiUrl)
                .then(function (response) {
                    awc.assignment = response.data;
                    awc.showContent = true;
                }, function (error) {
                    toastr.error(error.data);
                }).finally(function () {
                    awc.notBusy = true;
                });
        }

        awc.getContent();

        awc.reset = function () {
            $http.get(apiUrl)
                .then(function (response) {
                    awc.assignment = response.data;
                }, function (error) {
                    toastr.error("Failed to load data");
                }).finally(function () {
                    awc.notBusy = true;
                });
        }

        awc.saveTest = function (input)
        {
            $http.post(updateTest, input)
                .then(function (response) {
                    toastr.success(response.data);
                }, function (error) {
                    toastr.error(error.data);
                })
                .finally(function (response) {
                });
        }

        awc.deleteTest = function (index) {
            $uibModal.open({
                templateUrl: '/Html/Admin/Modal/deleteItem.html',
                animation: false,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                size: 'md',
                controller: function ($scope) {
                    $scope.close = function () {
                        //Closing modal instance
                        var top = $uibModalStack.getTop();
                        if (top) {
                            $uibModalStack.dismiss(top.key);
                        }
                    };
                    $scope.submit = function () {
                        $http.post(deleteTest, awc.assignment.inputs[index])
                            .then(function (response) {
                                awc.assignment.inputs.splice(index, 1);
                                toastr.success(response.data);
                            }, function (error) {
                                toastr.error(error.data);
                            })
                            .finally(function (response) {
                                $scope.close()
                            });
                    };
                }
            }).result.then(function () {
                
            }, function (res) {
                
            });
        }

        awc.createTest = function () {
            $http.get(createTest + awc.assignment.id)
                .then(function (response) {
                    toastr.success(response.data);
                    awc.getContent();
                }, function (error) {
                    toastr.error(error.data);
                }).finally(function (response) {
                });
        }

        awc.saveAssignment = function () {
            $http.post(updateAssignmentUrl, awc.assignment)
                .then(function (response) {
                    toastr.success(response.data);
                }, function (error) {
                    toastr.error(error.data);
                }).finally(function (response) {
                });
        }

        var deleteAssignmentLocal = function () {
            $http.post(deleteAssignmentUrl, awc.assignment)
                .then(function (response) {
                    toastr.success(response.data);
                    $state.go('app.contests.one', {id:awc.assignment.contest.id});
                }, function (error) {
                    toastr.error(error.data);
                }).finally(function (response) {
                });
        }

        awc.deleteAssignment = function (index) {
            $uibModal.open({
                templateUrl: '/Html/Admin/Modal/deleteItem.html',
                animation: false,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                size: 'md',
                controller: function ($scope) {
                    $scope.close = function () {
                        //Closing modal instance
                        var top = $uibModalStack.getTop();
                        if (top) {
                            $uibModalStack.dismiss(top.key);
                        }
                    };
                    $scope.submit = function () {
                        deleteAssignmentLocal();
                        $scope.close()
                    };
                }
            }).result.then(function () {

            }, function (res) {

            });
        }
    }
})();