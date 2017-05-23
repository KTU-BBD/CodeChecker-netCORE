(function() {
    "use strict";
    //Getting the exsisting module
    angular.module("app-contact")
        .controller("contactController", contactController);
    function contactController($http, $scope, toastr) {
        $scope.contacted = false;

        $scope.contact = {
            name: "",
            email: "",
            message: ""
        };

        $scope.submitContact = function() {
            $http.post("/api/front/contact/submit", $scope.contact)
                .then(function(response) {
                        toastr.success(response.data);
                        $scope.contacted = true;
                    },
                    function(error) {
                        toastr.error(error.data);
                    });
        }
    }
})();