angular
    .module('app')
    .controller('ContestViewController', ['NgTableParams', '$scope', '$resource', 'Auth', '$http', function (NgTableParams, $scope, $resource, Auth, $http) {
        // Clone data array

        var cvc = this;
        var Api = $resource('/api/admin/contest/all/');

        $http.get("/api/admin/user/current")
            .then(function (response) {
                cvc.currentUser = response.data;
            }).finally(function () {

                cvc.show = contains(cvc.currentUser.roles);
            });

        this.tableParams = new NgTableParams({}, {
            getData: function (params) {
                // ajax request to api
                return Api.query(params.url()).$promise.then(function (data) {
                    return data;
                });
            }
        });
        // Parodyt arvydui kad neveik servisas normaliai
        //$scope.currentUser = Auth.getCurrentUser();
        //window.alert($scope.currentUser.userName)
        
        function contains(a) {
            var i = a.length;
            while (i--) {
                if (a[i] === "Administrator" || a[i] === "Moderator") {
                    return true;
                }
            }
            return false;
        } 
    }
    ]);