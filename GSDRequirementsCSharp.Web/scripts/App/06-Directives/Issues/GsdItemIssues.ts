module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdItemIssues {
        public scope = {
            'specificationItem': '=specificationItem'
        };
        public templateUrl = GSDRequirements.baseUrl + 'issue/itemIssues'
        public controller = ['$scope', "$uibModal", ($scope: any, $uibModal: any) => {
            $scope.addIssue = (): void=> {
                var modal = $uibModal.open({
                    templateUrl: `${GSDRequirements.baseUrl}issue/form`,
                    controller: 'ModalItemIssuesController',
                    size: 'lg',
                    resolve: {
                        'specificationItem': () => $scope.specificationItem
                    }
                });
            }
        }]
        public static Factory() {
            return new GsdItemIssues();
        }
    }
    app.directive('gsdItemIssues', GsdItemIssues.Factory)
}