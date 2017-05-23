angular.module("app-faq")
    .controller("faqController", function (NgTableParams, $http, $resource, $scope, toastr, $uibModal, $uibModalStack, $routeParams,$sce) {
        var apiUrl = "/api/front/faq/getall/";
        var apiUrlOne = "/api/front/faq/GetFull/" + $routeParams.faqId;
        var fc = this;
        fc.tableParams = new NgTableParams({}, {
            getData: function (params) {
                return $http.get(apiUrl,
                    {
                        params: params.url()
                    }
                ).then(function (data) {
                    return data.data;
                });
            }
        });
        if ($routeParams.faqId !== null)
        {
            $http.get(apiUrlOne)
                .then(function (response) {
                    fc.faq = response.data;
                    fc.faq.shortDescription = $sce.trustAsHtml(response.data.shortDescription);
                    fc.faq.longDescription = $sce.trustAsHtml(response.data.longDescription);
                }, function (error) {
                    toastr.error(error.data);
                });
        }
    }       
);