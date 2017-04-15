(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app-contest")
        .controller("codeController", codeController);

    function codeController($http) {
        var vm = this;
        vm.code = {};

        vm.errorMessage = "";
        var apiUrl = "/api/contest";

        vm.addCode = function () {
            window.alert("aaa");
            $http.post(apiUrl, vm.code)
                .then(function (response) {
                }, function () {
                }).finally(function () {
                });

        };
    }
})();
