(function() {
    "use strict";
    // Creating the module
    angular.module("app-gym",
            ["app-navbar", "ngAnimate", "toastr", "ngRoute", "angularMoment", "ngResource", "sidebarModule", "timer", "ui.ace", "ui.bootstrap", "ngTable", "colourise"])
        .config(function($routeProvider) {
            $routeProvider
                .when("/",
                {
                    controller: "gymController",
                    controllerAs: "cc",
                    templateUrl: "/Html/Front/Gym/Contest.html"
                })
                .when("/:contestId",
                {
                    controller: "gymViewController",
                    controllerAs: "cvc",
                    templateUrl: "/Html/Front/Gym/ContestView.html"
                })
                .when("/:contestId/:assignmentId",
                {
                    controller: "assignmentViewController",
                    controllerAs: "avc",
                    templateUrl: "/Html/Front/Gym/AssignmentView.html"
                });

            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();