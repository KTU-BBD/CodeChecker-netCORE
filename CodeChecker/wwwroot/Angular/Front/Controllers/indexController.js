(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app-index")
        .controller("indexController", indexController);

    function indexController($scope, toastr, $http) {

        $scope.currentPage = 1;

        $scope.isLastPage = true;

        $scope.articles = [];

        getArticles(0);

        $scope.swapPage = function(page) {
            $scope.currentPage += page;

            getArticles($scope.currentPage - 1);
        };

        function getArticles(page) {
            $http.get("/api/front/article/page/" + page)
                .then(function (response) {
                    $scope.articles = response.data;
                    console.log(response.data.length < 5);
                    $scope.isLastPage = response.data.length < 5;
                }, function (error) {
                    toastr.error(error.data);
                });
        }

    }
})();

