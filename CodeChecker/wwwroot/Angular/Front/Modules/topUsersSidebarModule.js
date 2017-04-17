(function () {
    "use strsct";
    angular.module("sidebarModule", ["topUsers"])
        .controller("sidebarController", sidebarController);

    function sidebarController($http, $scope) {
    var sc = this;
    sc.notBusy = false;
    var apiUrl = "/api/front/User/GetTopUsers/10";
   // sc.alert = function () { window.alert(); }
    sc.errorMessage = "";
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

