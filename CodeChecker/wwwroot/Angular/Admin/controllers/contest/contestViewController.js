angular
    .module('app')
    .controller('ContestViewController', function (NgTableParams, $scope, $resource, Auth, $http, $state, $window, $uibModal, $uibModalStack, toastr) {

        var cvc = this;
        var deleteContestUrl = "/api/admin/contest/DeleteContest/"
        var recoverContestUrl = "/api/admin/contest/RecoverContest/"
        var Api = $resource('/api/admin/contest/all/');
        cvc.ajaxGet = function () {
            $http.get("/api/admin/user/current")
                .then(function (response) {
                    cvc.currentUser = response.data;
                }).finally(function () {
                    console.log(cvc.currentUser.roles);
                    cvc.show = contains(cvc.currentUser.roles);
                });
        }
        
        cvc.showstatus = function (num){
            if (num == 0) { return "Created"}
            if (num == 1) { return "Accepted" }
            if (num == 2) { return "Cancelled" }
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
                    var contest = cvc.tableParams.data[i]
                }
            }
            if (contest.deletedAt != null) {
                toastr.error("Contest is deleted");
            } else {
                $state.go('app.contests.one', { id: contest.id});
            }
        }

        cvc.delete = function (id) {
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