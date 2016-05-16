module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdIssueCreate {
        public scope = {
            'specificationItem': '=specificationItem',
            'afterSave': '=afterSave'
        };
        public templateUrl = GSDRequirements.baseUrl + 'issue/create'
        public controller = ['$scope', "$uibModal", ($scope: any, $uibModal: any) => {
            $scope.addIssue = (): void=> {
                var modal = $uibModal.open({
                    templateUrl: `${GSDRequirements.baseUrl}issue/form`,
                    controller: 'ModalIssueAddController',
                    size: 'lg',
                    resolve: {
                        'specificationItem': () => $scope.specificationItem 
                    }
                });

                modal.result.then((): void=> {
                    if ($scope.afterSave) {
                        $scope.afterSave()
                    }
                })
            }
        }]
        public static Factory() {
            return new GsdIssueCreate();
        }
    }
    app.directive('gsdIssueCreate', GsdIssueCreate.Factory)
}