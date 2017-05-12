(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app-index")
        .controller("viewArticleController", indexController);

    function indexController($routeParams, $scope, toastr, $http) {
        $scope.article = {};

        getArticles($routeParams.articleId);

        function getArticles(page) {
            $http.get("/api/front/article/get/" + page)
                .then(function (response) {
                    $scope.article = response.data;
                }, function (error) {
                    toastr.error(error.data);
                });
        }

    }
})();

