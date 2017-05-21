(function() {
    "use strict";
    // Creating the module
    angular.module("app-contest",
            ["app-navbar", "ngAnimate", "toastr", "ngRoute", "angularMoment", "ngResource", "sidebarModule", "timer", "ui.ace", "ui.bootstrap", "ngTable", "colourise", "verdict"])
        .config(function($routeProvider) {
            $routeProvider
                .when("/",
                {
                    controller: "contestController",
                    controllerAs: "cc",
                    templateUrl: "/Html/Front/Contest/Contest.html"
                })
                .when("/submissions/:contestId",
                    {
                        controller: "submissionController",
                        controllerAs: "svc",
                        templateUrl: "/Html/Front/Submission/submissionsAll.html"
                    })
                .when("/standings/:contestId",
                    {
                        controller: "standingsController",
                        controllerAs: "svc",
                        templateUrl: "/Html/Front/Submission/submissionsContest.html"
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