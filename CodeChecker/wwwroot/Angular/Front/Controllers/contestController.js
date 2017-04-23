(function() {
    "use strict";
    //Getting the exsisting module
    angular.module("app-contest")
        .controller("contestController", contestController);
    function contestController($http, $scope, toastr) {
        var apiUrl = "/api/front/contest/all";

        $scope.joinContest = function(id) {
            $('#' + id + ' .join').attr('disabled', 'disabled');

            $http.get('/api/front/contest/join/' + id).then(function (response) {
                toastr.success(response.data);
                $('#contest-' + id + ' .join').addClass('ng-hide');
                $('#contest-' + id + ' .enter').removeClass('ng-hide');
            }, function (error) {
                toastr.info(error.data);
            });
        };

        $scope.contests = [];
        $http.get(apiUrl)
            .then(function (response) {
                angular.copy(response.data, $scope.contests);
            }, function (error) {
                toastr.error('Error while retrieving contest data', 'Error');
            }).finally(function () {

            });
    }
})();