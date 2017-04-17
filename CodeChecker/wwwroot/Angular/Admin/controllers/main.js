//main.js
angular
    .module('app')
    .controller('NavbarController',['Auth', '$scope', function(Auth, $scope) {
            $scope.currentUser = Auth.getCurrentUser();
            console.log($scope.currentUser);
        }
    ]);