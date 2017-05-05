angular
    .module('app')
    .controller('SingleUserViewController', ['NgTableParams', '$scope', '$resource', 'Auth', '$http', '$state', '$stateParams', function (NgTableParams, $scope, $resource, Auth, $http, $state, $stateParams) {
        // Clone data array

        var suvc = this;
        var foo = $stateParams.id;
        window.alert(foo);
        window.alert();
    }
    ]);