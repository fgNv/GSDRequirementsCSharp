module Controllers {

    declare var angular: any
    declare var _: any
    declare var GSDRequirements: Globals.GSDRequirementsData
    var app = angular.module(GSDRequirements.angularModuleName)

    class ModalItemIssuesController {
        private loadIssues($scope, itemIssuesResource, specificationItem) {
            $scope.pendingRequests++;
            itemIssuesResource.query({ id: specificationItem.id })
                .$promise
                .then((issues): void => {
                    $scope.issues = _.map(issues, i => new Models.Issue(i))
                })
                .catch((error): void => {
                    Notification.notifyError(Sentences.errorLoadingIssues, error.messages)
                })
                .finally((): void => {
                    $scope.pendingRequests--;
                });
        }
        constructor($scope, $uibModalInstance, itemIssuesResource,
            specificationItem) {
            $scope.pendingRequests = 0
            $scope.specificationItem = specificationItem
            $scope.issues = []

            this.loadIssues($scope, itemIssuesResource, specificationItem)
        }
    }

    app.controller('ModalItemIssuesController', ["$scope", "$uibModalInstance",
        "ItemIssuesResource", 'specificationItem', ModalItemIssuesController]);
}