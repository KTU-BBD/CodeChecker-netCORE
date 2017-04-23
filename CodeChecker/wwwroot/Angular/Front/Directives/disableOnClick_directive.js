(function () {
    "use strict";
    angular.module('disableOnClick',[])
        .directive('disableOnClick', function () {
            return {
                restrict: 'A',
                link: function(scope, ele, attrs){
                    $(ele).click(function(){
                        $(ele).attr('disabled', true);
                    });
                }
            };
        });
})();
