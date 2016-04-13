module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdItemIssues {
        public scope = {
            'specificationItem': '=specificationItem',
            'onAllIssuesConcluded': '=onAllIssuesConcluded'
        };
        public templateUrl = GSDRequirements.baseUrl + 'issue/itemIssues'
        public controller = ['$scope', "$uibModal", ($scope: any, $uibModal: any) => {
            $scope.pendingRequests = 0

            $scope.seeIssues = (): void=> {
                $scope.pendingRequests++
                var modal = $uibModal.open({
                    templateUrl: `${GSDRequirements.baseUrl}issue/list`,
                    controller: 'ModalItemIssuesController',
                    size: 'lg',
                    resolve: {
                        'specificationItem': () => $scope.specificationItem,
                        'onAllIssuesConcluded': () => $scope.onAllIssuesConcluded
                    }
                });

                modal.rendered.then((): void => { $scope.pendingRequests-- })

                modal.closed.then((): void => { window.location.href = "#" })
            }
        }]
        public static Factory() {
            return new GsdItemIssues();
        }
    }
    app.directive('gsdItemIssues', GsdItemIssues.Factory)
}