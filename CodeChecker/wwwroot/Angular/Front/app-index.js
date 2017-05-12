(function() {
    "use strict";
    // Creating the module
    angular.module("app-index", ["ngRoute", "sidebarModule", "ui.ace", "app-navbar", "ui.bootstrap", "toastr", "trust"])
        .config(function($routeProvider) {
            $routeProvider.when("/",
                {
                    controller: "indexController",
                    controllerAs: "ic",
                    templateUrl: "/Html/Front/Index/newsWrapper.html"
                })
                .when("/:articleId",
                {
                    controller: "viewArticleController",
                    controllerAs: "ac",
                    templateUrl: "/Html/Front/Index/article.html"
                });
            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();