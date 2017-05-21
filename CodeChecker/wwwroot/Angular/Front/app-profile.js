(function () {
    "use strict";
    // Creating the module
    angular.module("app-profile", ["ngRoute", "app-navbar","ngFileUpload", "toastr", "n3-line-chart","colourise"])
        .config(function ($routeProvider) {

            $routeProvider.when("/", {
                controller: "personalProfileController",
                controllerAs: "pc",
                templateUrl: "/Html/Front/Profile/personal.html"
            });
            $routeProvider.when("/:username", {
                controller: "profileController",
                controllerAs: "pc",
                templateUrl: "/Html/Front/Profile/profile.html"
            });
            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();