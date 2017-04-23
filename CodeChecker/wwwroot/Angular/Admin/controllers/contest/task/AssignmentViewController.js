
(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app")
        .controller("AssignmentViewController", AssignmentViewController);

    function AssignmentViewController($state, $stateParams, $http) {
        window.alert($stateParams.id);

        var awc = this;
        awc.notBusy = false;
        var assgnId = $stateParams.id;
        var apiUrl = "/api/admin/Assignment/GetFull/" + assgnId.toString();
        var updateContestUrl = "/api/admin/Contest/Update";


        $http.get(apiUrl)
            .then(function (response) {
                awc.assignment = response.data;
                window.alert("get ok");
            }, function (error) {
                awc.errorMessage = "Failed to load data: " + error;
                window.alert(error);
            }).finally(function () {
                awc.notBusy = true;
            });


        //scc.reset = function () {
        //    $http.get(apiUrl)
        //        .then(function (response) {
        //            scc.contest = response.data;
        //        }, function (error) {
        //            scc.errorMessage = "Failed to load data: " + error;
        //        }).finally(function () {
        //            scc.notBusy = true;
        //        });
        //}

        //scc.contest_to_update = scc.contest;

        //scc.saveUser = function () {
        //    var d = new Date();
        //    scc.contest.updatedAt = d.toISOString();
        //    $http.post(updateContestUrl, scc.contest)
        //        .then(function (response) {
        //        })
        //        .finally(function (response) {
        //        });
        //};
    }
})();

