(function () {
    "use strict";
    angular.module("sidebarModule", ["topUsers", "colourise"])
        .controller("sidebarController", sidebarController);

    function sidebarController($http, $scope) {
    var sc = this;
    sc.notBusy = false;
    var apiUrl = "/api/front/User/GetTopUsers/10";
    sc.errorMessage = "aaaaa";
    sc.topUsers = [];
    $http.get(apiUrl)
        .then(function (response) {
            angular.copy(response.data, sc.topUsers);
        }, function (error) {
            sc.errorMessage = "Failed to load data: " + error;
        }).finally(function () {
            sc.notBusy = true;
        });
    }
})();

