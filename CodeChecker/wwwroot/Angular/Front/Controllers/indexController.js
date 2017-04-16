(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app-index")
        .controller("indexController", indexController);

    function indexController($http) {
        var ic = this;
        ic.errorMessage = "";
        ic.ShowTopUsers = true;
        ic.isBusy = true;
        var apiUrl = "/api/trips";
        

        

        //$http.get(apiUrl)
        //    .then(function (response) {
        //        angular.copy(response.data, vm.trips);
        //    }, function (error) {
        //        vm.errorMessage = "Failed to load data: " + error;
        //    }).finally(function () {
        //        vm.isBusy = false;
        //    });
    }
})();

