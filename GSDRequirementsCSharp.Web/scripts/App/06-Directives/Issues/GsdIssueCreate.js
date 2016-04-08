var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdIssueCreate = (function () {
        function GsdIssueCreate() {
            this.scope = {
                'specificationItemId': '=specificationItemId',
                'afterSave': '=afterSave'
            };
            this.templateUrl = GSDRequirements.baseUrl + 'issue/create';
            this.controller = ['$scope', "$uibModal", function ($scope, $uibModal) {
                    $scope.addIssue = function () {
                        var modal = $uibModal.open({
                            templateUrl: GSDRequirements.baseUrl + "issue/form",
                            controller: 'ModalIssueAddController',
                            size: 'lg'
                        });
                    };
                }];
        }
        GsdIssueCreate.Factory = function () {
            return new GsdIssueCreate();
        };
        return GsdIssueCreate;
    })();
    app.directive('gsdIssueCreate', GsdIssueCreate.Factory);
})(Directives || (Directives = {}));
//# sourceMappingURL=GsdIssueCreate.js.map