(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app")
        .controller("SingleContactViewController", SingleContactViewController);

    function SingleContactViewController($state, $stateParams, $http, toastr, $uibModal, $uibModalStack, Auth) {

        var savc = this;
        savc.notBusy = false;
        savc.contact = {};
        var artId = $stateParams.id;
        var apiUrl = "/api/admin/contact/get/" + artId.toString();
        var updateArticleUrl = "/api/admin/contact/update";
        savc.showContent = false;

        $http.get(apiUrl)
            .then(function (response) {
                savc.contact = response.data;
                console.log(savc.contact);
                savc.showContent = true;
            }, function (error) {
                savc.errorMessage = "Failed to load data: " + error;
                if (error.data === "Unauthorized") {
                    toastr.error(error.data);
                }
            }).finally(function () {
                savc.notBusy = true;
            });

        savc.showStatus = function(num) {
            if (num === 0) {
                return "Created"
            }
            if (num === 1) {
                return "Answered"
            }
        };

        savc.saveContact = function () {
            savc.model = {
                id: savc.contact.id,
                responseMessage: savc.contact.responseMessage
            };

            $http.post(updateArticleUrl, savc.model)
                .then(function (response) {
                    toastr.success(response.data);
                }, function (error) {
                    toastr.error(error.data);
                })
                .finally(function (response) {
                });
        }

    }
})();