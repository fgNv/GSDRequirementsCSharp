module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdIssueCreate {
        public scope = {
            'specificationItemId': '=specificationItemId',
            'afterSave': '=afterSave'
        };
        public templateUrl = GSDRequirements.baseUrl + 'issue/create'
        public controller = ['$scope', "$uibModal", ($scope: any, $uibModal: any) => {
            $scope.addIssue = (): void=> {
                var modal = $uibModal.open({
                    templateUrl: `${GSDRequirements.baseUrl}issue/form`,
                    controller: 'ModalIssueAddController',
                    size: 'lg'
                });
            }
        }]
        public static Factory() {
            return new GsdIssueCreate();
        }
    }
    app.directive('gsdIssueCreate', GsdIssueCreate.Factory)
}