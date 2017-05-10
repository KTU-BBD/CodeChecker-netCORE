(function () {
    "use strict";
    // Creating the module
    angular.module("app-index", ["ngRoute", "sidebarModule", "ui.ace", "app-navbar", "ui.bootstrap", "toastr"])
        .config(function ($routeProvider) {
            $routeProvider.when("/", {
                controller: "indexController",
                controllerAs: "ic",
                templateUrl: "/Html/Front/Index/newsWrapper.html"
            });
            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();