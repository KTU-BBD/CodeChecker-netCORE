(function() {
    "use strict";
    //Getting the exsisting module
    angular.module("app-contest")
        .controller("contestController", function (NgTableParams, $http, $resource, $scope, toastr, $uibModal, $uibModalStack) {
            var apiUrl = "/api/front/contest/all";
            var _self = this;


            $scope.tableParams = new NgTableParams({}, {
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

            $scope.close = function() {

                //Closing modal instance
                var top = $uibModalStack.getTop();
                if (top) {
                    $uibModalStack.dismiss(top.key);
                }
            };

            $scope.joinContest = function(contest,isPublic) {
                _self.contest = contest;
                //Disabling button to prevent multiple clicking
                $('#contest-' + _self.contest + ' .join').attr('disabled', 'disabled');

                var attendContest = function(password) {
                    var contestData = {
                        contestId: _self.contest,
                        password: password
                    };

                    $http.post("/api/front/contest/join", contestData)
                        .then(function(response) {
                                //Showing View contest button and removing enter contest button
                                $('#contest-' + _self.contest + ' .join').addClass('ng-hide');
                                $('#contest-' + _self.contest + ' .enter').removeClass('ng-hide');
                                $scope.close();
                                toastr.success(response.data);
                            },
                            function(error) {
                                toastr.error(error.data);
                                //Enablig disabled button
                                $('#contest-' + _self.contest + ' .join').removeAttr('disabled');
                            });
                };

                if (!isPublic) {
                    $uibModal.open({
                        templateUrl: '/Html/Front/Modal/ContestPassword.html',
                        animation: false,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        size: 'md',
                        controller: function($scope) {
                            $scope.close = function() {
                                //Closing modal instance
                                var top = $uibModalStack.getTop();
                                if (top) {
                                    $uibModalStack.dismiss(top.key);
                                }
                            };
                            $scope.submit = function() {
                                attendContest($scope.password);
                            };
                        }
                    }).result.then(function() {
                        $('#contest-' + _self.contest + ' .join').removeAttr('disabled');
                    }, function(res) {
                        $('#contest-' + _self.contest + ' .join').removeAttr('disabled');
                    });
                } else {
                    attendContest();
                }
            };

            $scope.contests = [];
        })

        ;

})();