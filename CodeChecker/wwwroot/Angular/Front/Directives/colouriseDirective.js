(function () {
    "use strict";
    angular.module('colourise',[])
        .directive('colourise', function () {
            return {
                restrict: 'EAC',
                link: function(scope, ele, attrs) {
                    var points = attrs.colourise;
                    console.log(attrs);
                    var color = '#18fc29';

                    if (points > 500 && points < 1000) {
                        color = '#1c93f7';
                    }else if (points >= 1000 && points < 2000) {
                        color = '#331aec';
                    }else if (points >= 2000 && points < 3000) {
                        color = '#880eff';
                    }else if (points >= 3000 && points < 4000) {
                        color = '#ff901f';
                    }else if (points >= 4000) {
                        color = '#ff1345';
                    }

                    ele.css({ 'color': color });
                }
            };
        });
})();
