(function () {
    "use strict";
    // Creating the module
    angular.module("app-index", ["ngRoute", "sidebarModule", "ui.ace", "ui.bootstrap"])
        .config(function ($routeProvider) {
            $routeProvider.when("/", {
                controller: "indexController",
                controllerAs: "ic",
                templateUrl: "/Html/Front/Index/newsWrapper.html"
            });
            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();