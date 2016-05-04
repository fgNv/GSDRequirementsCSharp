var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdItemIssues = (function () {
        function GsdItemIssues() {
            this.scope = {
                'specificationItem': '=specificationItem',
                'onAllIssuesConcluded': '=onAllIssuesConcluded'
            };
            this.templateUrl = GSDRequirements.baseUrl + 'issue/itemIssues';
            this.controller = ['$scope', "$uibModal", function ($scope, $uibModal) {
                    $scope.pendingRequests = 0;
                    $scope.seeIssues = function () {
                        window.location.href = "#/issueList";
                        $scope.pendingRequests++;
                        var modal = $uibModal.open({
                            templateUrl: GSDRequirements.baseUrl + "issue/list",
                            controller: 'ModalItemIssuesController',
                            size: 'lg',
                            resolve: {
                                'specificationItem': function () { return $scope.specificationItem; },
                                'onAllIssuesConcluded': function () { return $scope.onAllIssuesConcluded; }
                            }
                        });
                        modal.rendered.then(function () { $scope.pendingRequests--; });
                        modal.closed.then(function () { window.location.href = "#"; });
                    };
                }];
        }
        GsdItemIssues.Factory = function () {
            return new GsdItemIssues();
        };
        return GsdItemIssues;
    }());
    app.directive('gsdItemIssues', GsdItemIssues.Factory);
})(Directives || (Directives = {}));
//# sourceMappingURL=GsdItemIssues.js.map