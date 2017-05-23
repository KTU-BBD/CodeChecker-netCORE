(function() {
    "use strict";
    // Creating the module
    angular.module("app-contact",
            ["app-navbar", "ngAnimate", "toastr", "ngRoute", "angularMoment", "ngResource", "timer", "ui.ace", "ui.bootstrap", "ngTable", "colourise", "verdict"])
        .config(function($routeProvider) {
            $routeProvider
                .when("/",
                {
                    controller: "contactController",
                    controllerAs: "cc",
                    templateUrl: "/Html/Front/Contact/contact.html"
                });

            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();