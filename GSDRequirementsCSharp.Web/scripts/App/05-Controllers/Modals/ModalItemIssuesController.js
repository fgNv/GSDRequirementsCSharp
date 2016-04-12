var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var ModalItemIssuesController = (function () {
        function ModalItemIssuesController($scope, $uibModalInstance, itemIssuesResource, specificationItem) {
            $scope.pendingRequests = 0;
            $scope.specificationItem = specificationItem;
            $scope.issues = [];
            this.loadIssues($scope, itemIssuesResource, specificationItem);
        }
        ModalItemIssuesController.prototype.loadIssues = function ($scope, itemIssuesResource, specificationItem) {
            $scope.pendingRequests++;
            itemIssuesResource.query({ id: specificationItem.id })
                .$promise
                .then(function (issues) {
                $scope.issues = _.map(issues, function (i) { return new Models.Issue(i); });
            })
                .catch(function (error) {
                Notification.notifyError(Sentences.errorLoadingIssues, error.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        return ModalItemIssuesController;
    })();
    app.controller('ModalItemIssuesController', ["$scope", "$uibModalInstance",
        "ItemIssuesResource", 'specificationItem', ModalItemIssuesController]);
})(Controllers || (Controllers = {}));
