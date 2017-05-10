(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app-index")
        .controller("indexController", indexController);

    function indexController($scope, toastr, $http) {
        $scope.maxSize = 5;
        $scope.bigTotalItems = 175;
        $scope.bigCurrentPage = 1;

        var apiUrl = "/api/front/article/get/" + 0;

        $scope.articles = [];
        $http.get(apiUrl)
            .then(function (response) {
                $scope.articles = response.data;
//                angular.copy(response.data, $scope.articles);
            }, function (error) {
                toastr.error(error.data);
            });

    }
})();

