angular
    .module('app')
    .controller('ContestViewController', ['NgTableParams', '$scope', '$resource', 'Auth', '$http', '$state', '$window', function (NgTableParams, $scope, $resource, Auth, $http, $state, $window) {
        // Clone data array

        var cvc = this;
        var Api = $resource('/api/admin/contest/all/');
        cvc.ajaxGet = function () {
            $http.get("/api/admin/user/current")
                .then(function (response) {
                    cvc.currentUser = response.data;
                }).finally(function () {
                    console.log(cvc.currentUser.roles);
                    cvc.show = contains(cvc.currentUser.roles);
                });
        }
        
        cvc.showstatus = function (num){
            if (num == 0) { return "Created"}
            if (num == 1) { return "Accepted" }
            if (num == 2) { return "Cancelled" }
        }

        cvc.ajaxGet();
        //$http.get("/api/admin/user/current")
        //    .then(function (response) {
        //        cvc.currentUser = response.data;
        //    }).finally(function () {

        //        cvc.show = contains(cvc.currentUser.roles);
        //    });
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

        var findAndReplace = function (arr,con,val) {
            for (var x in arr)
            {
                if (arr[x].id === con.id)
                {
                    arr[x].status = val
                }
            }
        }

        cvc.changeStatus = function (value, contest) {
            $http.post("/api/admin/contest/ChangeStatus/" + contest.id, value)
                .then(function (response) {
                    findAndReplace(cvc.tableParams.data, contest, value);
                }
                , function (err) {})
                .finally(function () {
                    
                });

        }

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