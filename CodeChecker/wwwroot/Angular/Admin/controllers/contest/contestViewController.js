angular
    .module('app')
    .controller('ContestViewController', function (NgTableParams, $scope, $resource, Auth, $http, $state, $window, $uibModal, $uibModalStack, toastr) {

        var cvc = this;
        var deleteContestUrl = "/api/admin/contest/DeleteContest/"
        var recoverContestUrl = "/api/admin/contest/RecoverContest/"
        var Api = $resource('/api/admin/contest/all/');
        var createContest = "/api/admin/contest/Create/";
        cvc.ajaxGet = function () {
            $http.get("/api/admin/user/current")
                .then(function (response) {
                    cvc.currentUser = response.data;
                }).finally(function () {
                    console.log(cvc.currentUser.roles);
                    cvc.show = contains(cvc.currentUser.roles);
                });
        }
        
        cvc.showStatus = function (num){
            if (num == 0) { return "Created"}
            if (num == 1) { return "Submited" }
            if (num == 2) { return "Aprooved" }
            if (num == 3) { return "Cancelled" }
        }

        cvc.ajaxGet();
        
        this.tableParams = new NgTableParams({}, {
            getData: function (params) {
                return Api.query(params.url()).$promise.then(function (data) {
                    return data;
                });
            }
        });

        cvc.goToContest = function (id) {
            for (var i = 0; i < cvc.tableParams.data.length; i++)
            {
                if (cvc.tableParams.data[i].id === id)
                {
                    var contest = cvc.tableParams.data[i];
                }
            }
            if (contest.deletedAt != null) {
                toastr.error("Contest is deleted");
            } else {
                $state.go('app.contests.one', { id: contest.id});
            }
        }

        var createContestLocal = function (name,password) {
            var contest = { "Name": name, "Password": password }
            $http.post(createContest, contest)
                .then(function (response) {
                    $state.go('app.contests.one', { id: response.data });
                    toastr.success("Contest created");
                }
                , function (err) {
                    toastr.error(err.data);
                })
                .finally(function () {

                });
        }

        cvc.createContest = function () {
            $uibModal.open({
                templateUrl: '/Html/Admin/Modal/createContest.html',
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
                        createContestLocal($scope.name,$scope.password);
                        $scope.close()
                    };
                }
            }).result.then(function () {

            }, function (res) {

            }); 
        }
        
        var deleteContestLocal = function (id) {
            var idMatch;
            for (var i = 0; i < cvc.tableParams.data.length; i++) {
                if (cvc.tableParams.data[i].id === id) {
                    idMatch = i;

                }
            }
            $http.post(deleteContestUrl + id)
                .then(function (response) {
                    toastr.success(response.data);
                    var d = new Date();
                    d.toISOString();
                    cvc.tableParams.data[idMatch].deletedAt = d.toISOString();
                }
                , function (err) {
                    toastr.error(err.data);
                })
                .finally(function () {
                });
        }

        cvc.delete = function (id) {
            if (cvc.show) {
                deleteContestLocal(id);
            } else {
                $uibModal.open({
                    templateUrl: '/Html/Admin/Modal/deleteItemAck.html',
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
        }

        cvc.recover = function (id) {
            var idMatch;
            for (var i = 0; i < cvc.tableParams.data.length; i++) {
                if (cvc.tableParams.data[i].id === id) {
                    idMatch = i;
                }
            }
            $http.post(recoverContestUrl + id)
                .then(function (response) {
                    toastr.success(response.data);
                    cvc.tableParams.data[idMatch].deletedAt = null;
                }
                , function (err) {
                    toastr.error(err.data);
                })
                .finally(function () {
                });
        }

        cvc.changeStatus = function (value, contest) {
            $http.post("/api/admin/contest/ChangeStatus/" + contest.id, value)
                .then(function (response) {
                    findAndReplace(cvc.tableParams.data, contest, value);
                }
                , function (err) { })
                .finally(function () {

                });

        }

        var findAndReplace = function (arr,con,val) {
            for (var x in arr)
            {
                if (arr[x].id === con.id)
                {
                    arr[x].status = val
                }
            }
        }

        cvc.changeStatus = function (value, contest) {
            $http.post("/api/admin/contest/ChangeStatus/" + contest.id, value)
                .then(function (response) {
                    findAndReplace(cvc.tableParams.data, contest, value);
                }
                , function (err) {})
                .finally(function () {
                    
                });

        }

        function contains(a) {
            var i = a.length;
            while (i--) {
                if (a[i] === "Administrator" || a[i] === "Moderator") {
                    return true;
                }
            }
            return false;
        } 
    }
    );