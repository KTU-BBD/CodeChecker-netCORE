angular
.module('app')
.config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', '$breadcrumbProvider', function($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, $breadcrumbProvider) {

  $urlRouterProvider.otherwise('/dashboard');

  $ocLazyLoadProvider.config({
    // Set to true if you want to see what and when is dynamically loaded
    debug: true
  });

  $breadcrumbProvider.setOptions({
    prefixStateName: 'app.main',
    includeAbstract: true,
    template: '<li class="breadcrumb-item" ng-repeat="step in steps" ng-class="{active: $last}" ng-switch="$last || !!step.abstract"><a ng-switch-when="false" href="{{step.ncyBreadcrumbLink}}">{{step.ncyBreadcrumbLabel}}</a><span ng-switch-when="true">{{step.ncyBreadcrumbLabel}}</span></li>'
  });

  $stateProvider
  .state('app', {
    abstract: true,
    templateUrl: 'Html/Admin/common/layouts/full.html',
    //page title goes here
    ncyBreadcrumb: {
      label: 'Root',
      skip: true
    }
  })
  .state('app.main', {
    url: '/dashboard',
    templateUrl: 'Html/Admin/pages/contest/main.html',
    controller: 'ContestViewController',
    controllerAs: 'contest',
    //page title goes here
    ncyBreadcrumb: {
      label: 'Home',
    },
    //page subtitle goes here
    params: { subtitle: 'Welcome to ROOT powerfull Bootstrap & AngularJS UI Kit' }
  })
  .state('appSimple', {
    abstract: true,
    templateUrl: 'Html/Admin/common/layouts/simple.html'
  })

  // Additional Pages
  .state('appSimple.login', {
    url: '/login',
    templateUrl: 'Html/Admin/pages/login.html'
  })
  .state('appSimple.register', {
    url: '/register',
    templateUrl: 'Html/Admin/pages/register.html'
  })
  .state('appSimple.404', {
    url: '/404',
    templateUrl: 'Html/Admin/pages/404.html'
  })
  .state('appSimple.500', {
    url: '/500',
    templateUrl: 'Html/Admin/pages/500.html'
  })
}]);
