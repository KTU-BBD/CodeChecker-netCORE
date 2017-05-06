angular
    .module('app')
    .controller('UserViewController',
    [
        'NgTableParams', '$scope', '$resource', 'Auth', '$http', '$state', 'toastr', function (NgTableParams, $scope, $resource, Auth, $http, $state, toastr) {
        // Clone data array

        var uvc = this;
        var Api = $resource('/api/admin/user/allpaged/');
            uvc.ajaxGet = function() {
                this.tableParams = new NgTableParams({},
                {
                    getData: function(params) {
                        // ajax request to api
                        return Api.query(params.url()).$promise.then(function(data) {
                            return data;
                        }, function(data) {
                            toastr.error(data.data);
                        } );
                    }
                });
            };
        uvc.ajaxGet();
    }
    ]);