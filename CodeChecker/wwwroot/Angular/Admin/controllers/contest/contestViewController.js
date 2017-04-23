//main.js
angular
    .module('app')
    .controller('ContestViewController',['NgTableParams', '$scope', '$resource',function (NgTableParams, $scope, $resource) {
            // Clone data array
            var Api = $resource('/api/admin/contest/all/');
            this.tableParams = new NgTableParams({}, {
                getData: function(params) {
                    // ajax request to api
                    return Api.query(params.url()).$promise.then(function(data) {
                        return data;
                    });
                }
            });
        }
    ]);