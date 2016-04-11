module Controllers {

    declare var angular: any
    declare var _: any
    declare var GSDRequirements: Globals.GSDRequirementsData
    var app = angular.module(GSDRequirements.angularModuleName)

    class ModalItemIssuesController {
        constructor($scope, $uibModalInstance, itemIssuesResource,
            specificationItem) {
            $scope.pendingRequests = 0
            $scope.specificationItem = specificationItem
        }
    }

    app.controller('ModalIssueAddController', ["$scope", "$uibModalInstance",
        "ItemIssuesResource", 'specificationItem', ModalItemIssuesController]);
}