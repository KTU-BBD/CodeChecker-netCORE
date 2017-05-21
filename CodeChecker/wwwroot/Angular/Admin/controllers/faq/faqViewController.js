angular
    .module('app')
    .controller('FAQController', function (NgTableParams, $scope, $resource, Auth, $http, $state, $window, $uibModal, $uibModalStack, toastr) {

        var ac = this;
        var Api = $resource('/api/admin/faq/getall/');
        var deleteArticleUrl = "/api/admin/faq/deletefaq/";
        var recoverContestUrl = "/api/admin/faq/recoverfaq/";
        var createArticleUrl = "api/admin/faq/Createfaq/";


       fc.ajaxGet = function () {
            $http.get("/api/admin/user/current")
                .then(function (response) {
                    fc.currentUser = response.data;
                }).finally(function () {
                    fc.show = contains(fc.currentUser.roles);
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

        fc.goToArticle = function (id) {
            for (var i = 0; i < fc.tableParams.data.length; i++) {
                if (fc.tableParams.data[i].id === id) {
                    var article = fc.tableParams.data[i];
                }
            }
            if (article.deletedAt !== null) {
                toastr.error("Article is deleted");
            } else {
                $state.go('app.articles.one', { id: article.id });
            }
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



        var createArticleLocal = function (title) {
            if (title === null) {
                toastr.error("Title cannot be empty");
            } else {
                var TitleToSend = { "Title": title }
                $http.post(createArticleUrl, TitleToSend)
                    .then(function (response) {
                        $state.go('app.articles.one', { id: response.data });
                        toastr.success("Article created");
                    }
                    , function (err) {
                        toastr.error(err.data);
                    })
                    .finally(function () {

                    });
            }
        }

        fc.createArticle = function () {
            $uibModal.open({
                templateUrl: '/Html/Admin/Modal/createArticle.html',
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
                        createArticleLocal($scope.name);
                        $scope.close()
                    };
                }
            }).result.then(function () {

            }, function (res) {

            });
        }



        var deleteArticleLocal = function (id) {
            var idMatch;
            for (var i = 0; i < fc.tableParams.data.length; i++) {
                if (fc.tableParams.data[i].id === id) {
                    idMatch = i;

                }
            }
            $http.post(deleteArticleUrl + id)

                .then(function (response) {
                    toastr.success(response.data);
                    var d = new Date();
                    d.toISOString();
                    fc.tableParams.data[idMatch].deletedAt = d.toISOString();
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
                            deleteArticleLocal(id);
                            $scope.close()
                        };
                    }
                }).result.then(function () {

                }, function (res) {

                });
            }
        }

        fc.recover = function (id) {
            var idMatch;
            for (var i = 0; i < fc.tableParams.data.length; i++) {
                if (fc.tableParams.data[i].id === id) {
                    idMatch = i;
                }
            }
            $http.post(recoverContestUrl + id)
                .then(function (response) {
                    toastr.success(response.data);
                    fc.tableParams.data[idMatch].deletedAt = null;
                }
                , function (err) {
                    toastr.error(err.data);
                })
                .finally(function () {
                });
        }

        var changeStatusLocalWMessage = function (value, article, message) {
            var obj = {
                "status": value,
                "message": message
            }
            console.log(value);
            console.log(message);
            $http.post("/api/admin/article/ChangeStatusWMessage/" + article.id, obj)
                .then(function (response) {
                    findAndReplace(fc.tableParams.data, article, value);
                    toastr.success("Status changed");
                }
                , function (err) {
                    toastr.error("Error");
                })
                .finally(function () {

                });
        }

        var changeStatusLocal = function (value, article) {
            $http.post("/api/admin/article/ChangeStatus/" + article.id, value)
                .then(function (response) {
                    findAndReplace(fc.tableParams.data, article, value);
                    toastr.success("Status changed");
                }
                , function (err) {
                    toastr.error("Error");
                })
                .finally(function () {

                });
        }

        fc.changeStatus = function (value, article) {
            if (!fc.show) {
                changeStatusLocal(value, article);
            } else {
                $uibModal.open({
                    templateUrl: '/Html/Admin/Modal/addMessage.html',
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
                            changeStatusLocalWMessage(value, article, $scope.message);
                            $scope.close()
                        };
                    }
                }).result.then(function () {

                }, function (res) {

                });
            }
        }





        ac.showStatus = function (num) {
            if (num === 0) { return "Created" }
            if (num === 1) { return "Submited" }
            if (num === 2) { return "Published" }
            if (num === 3) { return "Cancelled" }
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