(function () {
    "use strict";
    //Getting the exsisting module
    angular.module("app-index")
        .controller("indexController", indexController);

    function indexController($uibModal) {
        console.log();
        $uibModal.open({
            templateUrl: '/Html/Front/Modal/Test.html',
            animation: false,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            size: 'lg'
        });

    }
})();

