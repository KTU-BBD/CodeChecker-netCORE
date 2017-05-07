(function () {
    "use strict";
    // Creating the module
    angular.module("app-profile", ["ngRoute", "app-navbar", "toastr"])
        .config(function ($routeProvider) {
            $routeProvider.when("/", {
                controller: "profileController",
                controllerAs: "pc",
                templateUrl: "/Html/Front/Profile/.html"
            });
            $routeProvider.when("/:username", {
                controller: "profileController",
                controllerAs: "pc",
                templateUrl: "/Html/Front/Profile/profile.html"
            });
            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();