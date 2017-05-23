angular
    .module('app')
    .controller('ContactController', function (NgTableParams, $scope, $resource, Auth, $http, $state) {
        var cc = this;
        var Api = $resource('/api/admin/contact/all');
        
        this.tableParams = new NgTableParams({}, {
            getData: function (params) {
                return Api.query(params.url()).$promise.then(function (data) {
                    return data;
                });
            }
        });

            cc.goToContact = function(id) {
                $state.go('app.contacts.one', { id: id });
            };


        cc.showStatus = function (num) {
            if (num === 0) { return "Created" }
            if (num === 1) { return "Answered" }
        }

    }
);