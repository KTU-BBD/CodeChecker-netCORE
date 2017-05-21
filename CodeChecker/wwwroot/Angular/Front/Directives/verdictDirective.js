(function() {
    "use strict";
    angular.module('verdict', [])
        .directive('verdict',
            function() {
                return {
                    restrict: 'EAC',
                    link: function(scope, ele, attrs) {
                        scope.$watch(function() {
                                var value = "Unknown";

                                switch (parseInt(attrs['verdict'])) {
                                    case 0:
                                        value = "Error";
                                        break;
                                    case 1:
                                        value = "Success";
                                        break;
                                    case 2:
                                        value = "Wrong answer";
                                        break;
                                    case 3:
                                        value = "Time overflow";
                                        break;
                                    case 4:
                                        value = "Memory overflow";
                                        break;
                                    case 5:
                                        value = "Compilation error";
                                        break;
                                    case 6:
                                        value = "Runtime error";
                                        break;
                                    case 7:
                                        value = "Running tests";
                                        break;
                                    case 8:
                                        value = "Server error";
                                        break;
                                }

                                ele.html(value);
                            }
                        );
                    }
                };
            });
})();