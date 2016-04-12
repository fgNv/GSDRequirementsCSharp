var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var ModalItemIssuesController = (function () {
        function ModalItemIssuesController($scope, $uibModalInstance, itemIssuesResource, specificationItem) {
            $scope.pendingRequests = 0;
            $scope.specificationItem = specificationItem;
        }
        return ModalItemIssuesController;
    })();
    app.controller('ModalItemIssuesController', ["$scope", "$uibModalInstance",
        "ItemIssuesResource", 'specificationItem', ModalItemIssuesController]);
})(Controllers || (Controllers = {}));
//# sourceMappingURL=ModalItemIssuesController.js.map