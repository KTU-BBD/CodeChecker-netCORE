//main.js
angular
    .module('app')
    .controller('NavbarController',['Auth', '$rootScope', function(Auth, $rootScope) {
            Auth.getCurrentUser();
        }
    ]);