(function() {
    "use strict";
    //Getting the exsisting module
    angular.module("app-contest")
        .controller("contestViewController", contestViewController);
    function contestViewController($routeParams, $http, $scope, toastr) {
        console.log($routeParams);
    }
})();