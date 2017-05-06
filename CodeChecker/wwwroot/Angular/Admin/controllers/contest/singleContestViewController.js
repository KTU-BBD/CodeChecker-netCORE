
(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app")
        .controller("SingleContestViewController", SingleContestViewController);

    function SingleContestViewController($state, $stateParams, $http, toastr) {
       
        var scc = this;
        scc.notBusy = false;
        var conId = $stateParams.id;
        var apiUrl = "/api/admin/Contest/GetFull/" + conId.toString();
        var updateContestUrl = "/api/admin/Contest/Update";
        var deleteAssignmentUrl = "/api/admin/Assignment/Delete";
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

        scc.saveUser = function () {
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

        scc.deleteAssignment = function (index) {
            $http.post(deleteAssignmentUrl, scc.contest.assignments[index])
                .then(function (response) {
                    scc.contest.assignments.splice(index, 1);
                    toastr.success(response.data);
                }, function (error) {
                    toastr.error(error.data);
                }).finally(function (response) {
                });
        }

    }
})();

