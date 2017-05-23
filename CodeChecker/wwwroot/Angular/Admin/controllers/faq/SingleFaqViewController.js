
(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app")
        .controller("SingleFaqViewController", SingleFaqViewController);

    function SingleFaqViewController($state, $stateParams, $http, toastr, $uibModal, $uibModalStack, Auth) {

        var sfvc = this;
        var conId = $stateParams.id;
        var apiUrl = "/api/admin/faq/GetFull/" + conId.toString();
        var updateContestUrl = "/api/admin/faq/UpdateFaq";
       
        

        sfvc.ajaxGet = function () {
            $http.get("/api/admin/user/current")
                .then(function (response) {
                    sfvc.currentUser = response.data;
                }).finally(function () {
                    console.log(sfvc.currentUser.roles);
                    sfvc.show = contains(sfvc.currentUser.roles);
                });
        }

        sfvc.ajaxGet();

        $http.get(apiUrl)
            .then(function (response) {
                sfvc.faq = response.data;
            }, function (error) {
                sfvc.errorMessage = "Failed to load data: " + error;
                if (error.data === "Unauthorized") {
                    toastr.error(error.data);
                }
            }).finally(function () {
                sfvc.notBusy = true;
            });

        sfvc.reset = function () {
            $http.get(apiUrl)
                .then(function (response) {
                    sfvc.faq = response.data;
                }, function (error) {
                    sfvc.errorMessage = "Failed to load data: " + error;
                }).finally(function () {
                    sfvc.notBusy = true;
                });
        }
        
        sfvc.saveFaq = function () {
            $http.post(updateContestUrl, sfvc.faq)
                .then(function (response) {
                    toastr.success(response.data);
                }, function (error) {
                    toastr.error(error.data);
                })
                .finally(function (response) {
                });
        }

        function contains(a) {
            var i = a.length;
            while (i--) {
                if (a[i] === "Administrator" || a[i] === "Moderator") {
                    return true;
                }
            }
            return false;
        }
    }
})();

