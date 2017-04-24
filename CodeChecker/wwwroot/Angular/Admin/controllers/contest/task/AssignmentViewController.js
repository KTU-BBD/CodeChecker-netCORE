
(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app")
        .controller("AssignmentViewController", AssignmentViewController);

    function AssignmentViewController($state, $stateParams, $http) {
        var awc = this;
        awc.notBusy = false;
        var assgnId = $stateParams.id;
        var apiUrl = "/api/admin/Assignment/GetFull/" + assgnId.toString();
        var updateAssignmentUrl = "/api/admin/Assignment/Update";
        var updateTest = "/api/admin/Input/UpdateTest";


        $http.get(apiUrl)
            .then(function (response) {
                awc.assignment = response.data;
            }, function (error) {
                awc.errorMessage = "Failed to load data: " + error;
                window.alert(error);
            }).finally(function () {
                awc.notBusy = true;
            });


        awc.reset = function () {
            $http.get(apiUrl)
                .then(function (response) {
                    awc.assignment = response.data;
                }, function (error) {
                    awc.errorMessage = "Failed to load data: " + error;
                }).finally(function () {
                    awc.notBusy = true;
                });
        }

        awc.saveTest = function (input)
        {
            $http.post(updateTest, input)
                .then(function (response) {
                })
                .finally(function (response) {
                });
        }

        //scc.contest_to_update = scc.contest;

        awc.saveAssignment = function () {
           
            $http.post(updateAssignmentUrl, awc.assignment)
                .then(function (response) {
                })
                .finally(function (response) {
                });
        };
    }
})();

