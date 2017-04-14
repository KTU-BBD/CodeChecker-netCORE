(function () {
    "use strict";
    // Creating the module
    angular.module("app-contest", ["ngRoute"])
        .config(function ($routeProvider) {
            $routeProvider.when("/", {
                controller: "codeController",
                controllerAs: "vm",
                templateUrl: "/Html/Contest/Contest.html"
            });
            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();