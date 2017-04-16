(function () {
    "use strict";
    angular.module('topUsers',[])
        .directive('topUsers', function () {
            return {
                scope: { show: "=displayWhen" },
                restrict: 'E',
                templateUrl: "/Html/Front/DirectiveView/topUsers_directive.html"
            };
        });
})();
