
(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app")
        .controller("SingleContestViewController", SingleContestViewController);

    function SingleContestViewController($state, $stateParams, $http, toastr, $uibModal, $uibModalStack) {
       
        var scc = this;
        scc.notBusy = false;
        var conId = $stateParams.id;
        var apiUrl = "/api/admin/Contest/GetFull/" + conId.toString();
        var updateContestUrl = "/api/admin/Contest/Update";
        var createAssignmentUrl = "/api/admin/Assignment/Create/";
        var deleteAssignmentUrl = "/api/admin/Assignment/Delete";
        var deleteContestUrl = "api/admin/Contest/DeleteContest/";
        scc.showContent = false;

        $http.get(apiUrl)
            .then(function (response) {
                scc.contest = response.data;
                scc.showContent = true;
            }, function (error) {
                scc.errorMessage = "Failed to load data: " + error;
                if (error.data === "Unauthorized") {
                    toastr.error(error.data);
                }
            }).finally(function () {
                scc.notBusy = true;
            });


        scc.reset = function () {
            $http.get(apiUrl)
                .then(function (response) {
                    scc.contest = response.data;
                }, function (error) {
                    scc.errorMessage = "Failed to load data: " + error;
                }).finally(function () {
                    scc.notBusy = true;
                });
        }
        
        scc.contest_to_update = scc.contest;

        scc.saveContest = function () {
            var d = new Date();
            scc.contest.updatedAt = d.toISOString(); 
            $http.post(updateContestUrl, scc.contest)
                .then(function (response) {
                }, function (error) {
                    window.alert();
                })
                .finally(function (response) {
                });
        }

        var deleteContestLocal = function (id) {
            $http.post(deleteContestUrl + id)
                .then(function (response) {
                    toastr.success(response.data);
                    $state.go('app.contests.all');
                }
                , function (err) {
                    toastr.error(err.data);
                })
                .finally(function () {
                });
        }

        scc.deleteContest = function (id) {
            $uibModal.open({
                templateUrl: '/Html/Admin/Modal/deleteItem.html',
                animation: false,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                size: 'md',
                controller: function ($scope) {
                    $scope.close = function () {
                        var top = $uibModalStack.getTop();
                        if (top) {
                            $uibModalStack.dismiss(top.key);
                        }
                    };
                    $scope.submit = function () {
                        deleteContestLocal(id);
                        $scope.close()
                    };
                }
            }).result.then(function () {

            }, function (res) {

            }); 
        }

        var createAssignmentLocal = function (name) {
            window.alert(name);
            $http.post(createAssignmentUrl + scc.contest.id, name)
                .then(function (response) {
                    scc.contest.assignments.unshift(response.data);
                    toastr.success("Assignment created");
                    $state.go('app.contests.assignment', { id: response.data.id });
                }, function (error) {
                    toastr.error(error.data);
                }).finally(function (response) {
                });
        }

        scc.createAssignment = function () {
            $uibModal.open({
                templateUrl: '/Html/Admin/Modal/createItem.html',
                animation: false,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                size: 'md',
                controller: function ($scope) {
                    $scope.close = function () {
                        var top = $uibModalStack.getTop();
                        if (top) {
                            $uibModalStack.dismiss(top.key);
                        }
                    };
                    $scope.submit = function () {
                        if ($scope.name) {
                            createAssignmentLocal($scope.name);
                            $scope.close()
                        } else {
                            toastr.error("Name cannot be empty");
                        }
                    };
                }
            }).result.then(function () {

            }, function (res) {

            });
        }
        
        var deleteAssignmentLocal = function (index) {
            $http.post(deleteAssignmentUrl, scc.contest.assignments[index])
                .then(function (response) {
                    scc.contest.assignments.splice(index, 1);
                    toastr.success(response.data);
                    $state.go('app.contests.one', {id:scc.contest.id});
                }, function (error) {
                    toastr.error(error.data);
                }).finally(function (response) {
                });
        }

        scc.deleteAssignment = function (index) {
            $uibModal.open({
                templateUrl: '/Html/Admin/Modal/deleteItem.html',
                animation: false,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                size: 'md',
                controller: function ($scope) {
                    $scope.close = function () {
                        var top = $uibModalStack.getTop();
                        if (top) {
                            $uibModalStack.dismiss(top.key);
                        }
                    };
                    $scope.submit = function () {
                        deleteAssignmentLocal(index);
                        $scope.close()
                    };
                }
            }).result.then(function () {

            }, function (res) {

            });
        }
    }
})();

