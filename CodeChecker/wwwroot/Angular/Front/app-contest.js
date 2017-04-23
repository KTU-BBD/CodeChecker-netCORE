(function() {
    "use strict";
    // Creating the module
    angular.module("app-contest",
            ["ngAnimate", "toastr", "ngRoute", "angularMoment", "sidebarModule", "timer"])
        .config(function($routeProvider) {
            $routeProvider
                .when("/",
                {
                    controller: "contestController",
                    controllerAs: "cc",
                    templateUrl: "/Html/Front/Contest/Contest.html"
                })
                .when("/:contestId",
                {
                    controller: "contestViewController",
                    controllerAs: "cvc",
                    templateUrl: "/Html/Front/Contest/ContestView.html"
                });
            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();