(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app")
        .controller("SingleArticleViewController", SingleArticleViewController);

    function SingleArticleViewController($state, $stateParams, $http, toastr, $uibModal, $uibModalStack, Auth) {

        var savc = this;
        savc.notBusy = false;
        var artId = $stateParams.id;
        var apiUrl = "/api/admin/Article/GetFull/" + artId.toString();
        var updateArticleUrl = "/api/admin/Article/Update";
        var deleteArticleUrl = "api/admin/Article/DeleteArticle/";
        savc.showContent = false;

        savc.ajaxGet = function () {
            $http.get("/api/admin/user/current")
                .then(function (response) {
                    savc.currentUser = response.data;
                }).finally(function () {
                    savc.show = contains(savc.currentUser.roles);
                });
        }

        savc.ajaxGet();
        $http.get(apiUrl)
            .then(function (response) {
                savc.article = response.data;
                savc.showContent = true;
            }, function (error) {
                savc.errorMessage = "Failed to load data: " + error;
                if (error.data === "Unauthorized") {
                    toastr.error(error.data);
                }
            }).finally(function () {
                savc.notBusy = true;
            });

        function contains(a) {
            var i = a.length;
            while (i--) {
                if (a[i] === "Administrator" || a[i] === "Moderator") {
                    return true;
                }
            }
            return false;
        } 

        savc.reset = function () {
            $http.get(apiUrl)
                .then(function (response) {
                    savc.article = response.data;
                }, function (error) {
                    savc.errorMessage = "Failed to load data: " + error;
                }).finally(function () {
                    scc.notBusy = true;
                });
        }

        savc.saveArticle = function () {
            $http.post(updateArticleUrl, savc.article)
                .then(function (response) {
                    toastr.success(response.data);
                }, function (error) {
                    toastr.error(error.data);
                })
                .finally(function (response) {
                });
        }

        var deleteArticleLocal = function (id) {
            $http.post(deleteArticleUrl + id)
                .then(function (response) {
                    toastr.success(response.data);
                    $state.go('app.articles.all');
                }
                , function (err) {
                    toastr.error(err.data);
                })
                .finally(function () {
                });
        }

        savc.deleteArticle = function (id) {
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
                        deleteArticleLocal(id);
                        $scope.close()
                    };
                }
            }).result.then(function () {

            }, function (res) {

            });
        }

        var publishLocal = function () {
            $http.post("/api/admin/article/ChangeStatus/" + savc.article.id, 1)
                .then(function (response) {
                    savc.article.status = 1;
                    toastr.success("Article submited");
                }
                , function (err) {
                    toastr.error("Error");
                })
                .finally(function () {

                });
        }

        savc.publish = function () {
            $uibModal.open({
                templateUrl: '/Html/Admin/Modal/submitItem.html',
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
                        publishLocal();
                        $scope.close()
                    };
                }
            }).result.then(function () {

            }, function (res) {

            });
        }
    }
})();