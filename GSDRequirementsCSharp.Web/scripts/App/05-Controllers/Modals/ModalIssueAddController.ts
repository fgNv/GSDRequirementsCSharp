module Controllers {

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: Globals.GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);


    class ModalIssueAddController {
        constructor($scope: any,
            $uibModalInstance: any,
            issueResource) {
        }

    }

    app.controller('ModalIssueAddController', ["$scope", "$uibModalInstance",
        "IssueResource", ModalIssueAddController]);
}