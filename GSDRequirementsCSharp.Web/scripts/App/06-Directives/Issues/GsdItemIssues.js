var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdItemIssues = (function () {
        function GsdItemIssues() {
            this.scope = {
                'specificationItem': '=specificationItem'
            };
            this.templateUrl = GSDRequirements.baseUrl + 'issue/itemIssues';
            this.controller = ['$scope', "$uibModal", function ($scope, $uibModal) {
                    $scope.seeIssues = function () {
                        var modal = $uibModal.open({
                            templateUrl: GSDRequirements.baseUrl + "issue/list",
                            controller: 'ModalItemIssuesController',
                            size: 'lg',
                            resolve: {
                                'specificationItem': function () { return $scope.specificationItem; }
                            }
                        });
                    };
                }];
        }
        GsdItemIssues.Factory = function () {
            return new GsdItemIssues();
        };
        return GsdItemIssues;
    })();
    app.directive('gsdItemIssues', GsdItemIssues.Factory);
})(Directives || (Directives = {}));
//# sourceMappingURL=GsdItemIssues.js.map