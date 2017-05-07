(function() {
    "use strict";
    // Creating the module
    angular.module("app-contest",
            ["app-navbar", "ngAnimate", "toastr", "ngRoute", "angularMoment", "ngResource", "sidebarModule", "timer", "ui.ace", "ui.bootstrap", "ngTable", "colourise"])
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
                })
                .when("/:contestId/:assignmentId",
                {
                    controller: "assignmentViewController",
                    controllerAs: "avc",
                    templateUrl: "/Html/Front/Contest/AssignmentView.html"
                });

            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();