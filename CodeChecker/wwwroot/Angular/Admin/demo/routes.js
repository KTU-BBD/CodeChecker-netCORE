angular
.module('app')
.config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', '$breadcrumbProvider', function($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, $breadcrumbProvider) {
  $stateProvider
  .state('app.icons', {
    url: "/icons",
    abstract: true,
    template: '<ui-view></ui-view>',
    ncyBreadcrumb: {
      label: 'Icons'
    }
  })
  .state('app.icons.fontawesome', {
    url: '/font-awesome',
    templateUrl: 'Html/Admin/icons/font-awesome.html',
    ncyBreadcrumb: {
      label: 'Font Awesome'
    }
  })
  .state('app.icons.simplelineicons', {
    url: '/simple-line-icons',
    templateUrl: 'Html/Admin/icons/simple-line-icons.html',
    ncyBreadcrumb: {
      label: 'Simple Line Icons'
    }
  })
  .state('app.components', {
    url: "/components",
    abstract: true,
    template: '<ui-view></ui-view>',
    ncyBreadcrumb: {
      label: 'Components'
    }
  })
  .state('app.components.buttons', {
    url: '/buttons',
    templateUrl: 'Html/Admin/components/buttons.html',
    ncyBreadcrumb: {
      label: 'Buttons'
    }
  })
  .state('app.components.social-buttons', {
    url: '/social-buttons',
    templateUrl: 'Html/Admin/components/social-buttons.html',
    ncyBreadcrumb: {
      label: 'Social Buttons'
    }
  })
  .state('app.components.cards', {
    url: '/cards',
    templateUrl: 'Html/Admin/components/cards.html',
    ncyBreadcrumb: {
      label: 'Cards'
    }
  })
  .state('app.components.forms', {
    url: '/forms',
    templateUrl: 'Html/Admin/components/forms.html',
    ncyBreadcrumb: {
      label: 'Forms'
    }
  })
  .state('app.components.switches', {
    url: '/switches',
    templateUrl: 'Html/Admin/components/switches.html',
    ncyBreadcrumb: {
      label: 'Switches'
    }
  })
  .state('app.components.tables', {
    url: '/tables',
    templateUrl: 'Html/Admin/components/tables.html',
    ncyBreadcrumb: {
      label: 'Tables'
    }
  })
  .state('app.forms', {
    url: '/forms',
    templateUrl: 'Html/Admin/forms.html',
    ncyBreadcrumb: {
      label: 'Forms'
    }
  })
  .state('app.widgets', {
    url: '/widgets',
    templateUrl: 'Html/Admin/widgets.html',
    ncyBreadcrumb: {
      label: 'Widgets'
    }
  })
  .state('app.charts', {
    url: '/charts',
    templateUrl: 'Html/Admin/charts.html',
    ncyBreadcrumb: {
      label: 'Charts'
    }
      })
  .state('app.contests', {
      url: "/contests",
      abstract: true,
      template: '<ui-view></ui-view>',
      ncyBreadcrumb: {
          label: 'Contests'
      },
      })
      .state('app.contests.all', {
          url: '/all',
          templateUrl: 'Html/Admin/pages/contest/main.html',
          ncyBreadcrumb: {
              label: 'All'
          },
          controller: 'ContestViewController',
          controllerAs: 'contest'
      })
    .state('app.contests.one', {
        url: '/contest/:id',
        templateUrl: 'Html/Admin/pages/contest/contest.html',
        ncyBreadcrumb: {
            label: 'View'
        },
        controller: 'SingleContestViewController',
        controllerAs: 'scc',
        params: {
            id:null
        }
        })
      .state('app.contests.assignment', {
          url: '/contest/assignment/:id',
          templateUrl: 'Html/Admin/pages/contest/task/assignment.html',
          ncyBreadcrumb: {
              label: 'Assignment'
          },
          controller: 'AssignmentViewController',
          controllerAs: 'awc',
          params: {
              id: null
          }
      })
.state('app.users', {
    url: "/users",
    abstract: true,
    template: '<ui-view></ui-view>',
    ncyBreadcrumb: {
        label: 'Users'
    },
        })
      .state('app.users.all', {
          url: '/all',
          templateUrl: 'Html/Admin/pages/user/users.html',
          ncyBreadcrumb: {
              label: 'All Users'
          },
          controller: 'UserViewController',
          controllerAs: 'uvc'
        })
      .state('app.users.one', {
          url: '/:id',
          templateUrl: 'Html/Admin/pages/user/user.html',
          ncyBreadcrumb: {
              label: 'View'
          },
          controller: 'SingleUserViewController',
          controllerAs: 'suvc',
          params: {
              id: null
          }
        })
.state('app.articles', {
    url: "/articles",
    abstract: true,
    template: '<ui-view></ui-view>',
    ncyBreadcrumb: {
        label: 'Articles'
    },
})
.state('app.articles.all', {
    url: '/all',
    templateUrl: 'Html/Admin/pages/article/articles.html',
    ncyBreadcrumb: {
        label: 'All Articles'
    },
    controller: 'ArticleController',
    controllerAs: 'ac'
})
.state('app.articles.one', {
    url: '/:id',
    templateUrl: 'Html/Admin/pages/article/article.html',
    ncyBreadcrumb: {
        label: 'Article'
    },
    controller: 'SingleArticleViewController',
    controllerAs: 'savc',
    params: {
        id: null
    }
        })


.state('app.faq', {
    url: "/articles",
    abstract: true,
    template: '<ui-view></ui-view>',
    ncyBreadcrumb: {
        label: 'Articles'
    },
})
.state('app.faq.all', {
    url: '/all',
    templateUrl: 'Html/Admin/pages/faq/faqs.html',
    ncyBreadcrumb: {
        label: 'All Articles'
    },
    controller: 'FAQController',
    controllerAs: 'fc'
})
.state('app.faq.one', {
    url: '/:id',
    templateUrl: 'Html/Admin/pages/faq/faq.html',
    ncyBreadcrumb: {
        label: 'Article'
    },
    controller: 'SingleFAQController',
    controllerAs: 'sfc',
    params: {
        id: null
    }
})


}]);
