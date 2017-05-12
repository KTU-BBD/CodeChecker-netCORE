(function () {
    "use strict";
    angular.module("trust",['ngResource'])
        .filter("trust", ['$sce', function($sce) {
            return function(htmlCode){
                return $sce.trustAsHtml(htmlCode);
            }
        }]);
})();
