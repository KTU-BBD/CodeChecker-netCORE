(function () {
    "use strict";
    // Creating the module
    angular.module("app-faq",
        ["app-navbar", "ngAnimate", "toastr", "ngRoute", "angularMoment", "ngResource", "sidebarModule", "timer", "ui.ace", "ui.bootstrap", "ngTable", "colourise"])
        .config(function ($routeProvider) {
            $routeProvider
                .when("/",
                {
                    controller: "faqController",
                    controllerAs: "fc",
                    templateUrl: "/Html/Front/Faq/faqs.html"
                })
                .when("/:faqId",
                {
                    controller: "faqController",
                    controllerAs: "fc",
                    templateUrl: "/Html/Front/Faq/faq.html"
                })
            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();