(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app-profile")
        .controller("profileController", function($routeParams, $scope, $http, toastr) {
            $scope.data = {
                dataset0: []
            };

            $scope.options = {
                drawLegend: false,
                series: [
                    {
                        axis: "y",
                        dataset: "dataset0",
                        key: "val_0",
                        label: "An area series",
                        color: "#1f77b4",
                        type: ["line"],
                        id: 'mySeries0'
                    }
                ],
                axes: {x: {
                    key: "x", type: "date", min: 8000, max: 10000
                }}
            };


            $http.get('/api/front/user/statistics/' + $routeParams.username).then(function (data) {

                for (var i = 0; i < data.data.length; i++) {
                    $scope.data.dataset0.push({
                        x: data.data[i].createdAt,
                        val_0: data.data[i].rating
                    });
                }

                $scope.data.dataset0.forEach(function(row) {
                    row.x = new Date(row.x);
                });
            }, function(response) {
                toastr.error(response.data);
            });

            $http.get('/api/front/user/get/' + $routeParams.username).then(function (data) {
                $scope.userdata = data.data;
            }, function(response) {
                toastr.error(response.data);
            });


        });
})();

