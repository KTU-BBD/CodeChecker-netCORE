(function() {
    "use strict";
    // Creating the module
    angular.module("app-submission",
            ["app-navbar", "ngAnimate", "toastr", "ngRoute", "angularMoment", "ngResource", "sidebarModule", "timer", "ui.ace", "ui.bootstrap", "ngTable", "colourise", "verdict"])
        .config(function($routeProvider) {
            $routeProvider
                .when("/",
                    {
                        controller: "submissionAllController",
                        controllerAs: "svc",
                        templateUrl: "/Html/Front/Submission/submissionsAll.html"
                    });


            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();