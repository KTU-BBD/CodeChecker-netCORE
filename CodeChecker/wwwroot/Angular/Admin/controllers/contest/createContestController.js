
(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app")
        .controller("CreateContestController", SingleContestViewController);

    function SingleContestViewController($state, $stateParams, $http) {
        var ccc = this;
        var createContestUrl = "/api/admin/Contest/Update";
        var ccc.contest;
        scc.saveUser = function () {
            var d = new Date();
            scc.contest.updatedAt = d.toISOString();
            $http.post(createContestUrl, ccc.contest)
                .then(function (response) {
                })
                .finally(function (response) {
                });
        }
    }
})();

