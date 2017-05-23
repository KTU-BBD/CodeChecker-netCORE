angular
    .module('app')
    .controller('FAQController', function (NgTableParams, $scope, $resource, Auth, $http, $state, $window, $uibModal, $uibModalStack, toastr) {

        var fc = this;
        var Api = $resource('/api/admin/faq/getall/');
        var deleteFaqUrl = "/api/admin/faq/deletefaq/";
        var createFaqUrl = "api/admin/faq/CreateFAQ/";


       fc.ajaxGet = function () {
            $http.get("/api/admin/user/current")
                .then(function (response) {
                    fc.currentUser = response.data;
                }).finally(function () {
                });
       }

       fc.ajaxGet();

        this.tableParams = new NgTableParams({}, {
            getData: function (params) {
                return Api.query(params.url()).$promise.then(function (data) {
                    return data;
                });
            }
        });

        fc.goToFAQ = function (id) {
            $state.go('app.faq.one', { id: id });
        }
        
        var createFaqLocal = function (question) {
            if (question === null) {
                toastr.error("Question cannot be empty");
            } else {
                var QuestionToSend = { "Question": question }
                $http.post(createFaqUrl, QuestionToSend)
                    .then(function (response) {
                        $state.go('app.faq.one', { id: response.data });
                        toastr.success("Faq created");
                    }
                    , function (err) {
                        console.log(question);
                        toastr.error(err.data);
                    })
                    .finally(function () {

                    });
            }
        }

        fc.createFaq = function () {
            $uibModal.open({
                templateUrl: '/Html/Admin/Modal/createFaq.html',
                animation: false,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                size: 'md',
                controller: function ($scope) {
                    $scope.close = function () {
                        var top = $uibModalStack.getTop();
                        if (top) {
                            $uibModalStack.dismiss(top.key);
                        }
                    };
                    $scope.submit = function () {
                        createFaqLocal($scope.question);
                        $scope.close()
                    };
                }
            }).result.then(function () {

            }, function (res) {

            });
        }
        
        var deleteFaqLocal = function (id) {
            
            $http.post(deleteFaqUrl + id)

                .then(function (response) {
                    for (var i = 0; i < fc.tableParams.data.length; i++) {
                        if (fc.tableParams.data[i].id === id) {
                            fc.tableParams.data.splice(i, 1);
                        }
                    }
                    toastr.success(response.data);
                }
                , function (err) {
                    toastr.error(err.data);
                })
                .finally(function () {
                });
        }

        fc.delete = function (id) {
            if (fc.show) {
                deleteArticleLocal(id);
            } else {
                $uibModal.open({
                    templateUrl: '/Html/Admin/Modal/deleteItemAck.html',
                    animation: false,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    size: 'md',
                    controller: function ($scope) {
                        $scope.close = function () {
                            var top = $uibModalStack.getTop();
                            if (top) {
                                $uibModalStack.dismiss(top.key);
                            }
                        };
                        $scope.submit = function () {
                            deleteFaqLocal(id);
                            $scope.close()
                        };
                    }
                }).result.then(function () {

                }, function (res) {

                });
            }
        }
        

        var findAndReplace = function (arr, con, val) {
            for (var x in arr) {
                if (arr[x].id === con.id) {
                    arr[x].status = val
                }
            }
        }

    }
);