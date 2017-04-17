// Default colors
var brandPrimary = '#20a8d8';
var brandSuccess = '#4dbd74';
var brandInfo = '#63c2de';
var brandWarning = '#f8cb00';
var brandDanger = '#f86c6b';

var grayDark = '#2a2c36';
var gray = '#55595c';
var grayLight = '#818a91';
var grayLighter = '#d1d4d7';
var grayLightest = '#f8f9fa';

angular
    .module('app',
    [
        'ui.router',
        'oc.lazyLoad',
        'ncy-angular-breadcrumb',
        'angular-loading-bar',
        'ngResource',
        'ngFileUpload'

    ])
    .service('Auth', function($resource, $rootScope) {
        this.getCurrentUser = function() {
            if (!$rootScope.currentUser) {
                $rootScope.currentUser = $resource('/api/admin/user/current').get();
                $rootScope.currentUser.$promise.then(function(data) {
                    $rootScope.currentUser = data;
                    if (!data.profileImage) {
                        $rootScope.currentUser.avatar = "/assets/default.png";
                    } else {
                        $rootScope.currentUser.avatar = "/assets/" + data.profileImage.name;
                    }
                });
            }

            return $rootScope.currentUser;
        };
    })
    .config([
        'cfpLoadingBarProvider', function(cfpLoadingBarProvider) {
            cfpLoadingBarProvider.includeSpinner = false;
            cfpLoadingBarProvider.latencyThreshold = 1;
        }
    ])
    .run([
        '$rootScope', '$state', '$stateParams', function($rootScope, $state, $stateParams) {
            $rootScope.$on('$stateChangeSuccess',
                function() {
                    document.body.scrollTop = document.documentElement.scrollTop = 0;
                });
            $rootScope.$state = $state;
            return $rootScope.$stateParams = $stateParams;
        }
    ]);