var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdIssueCreate = (function () {
        function GsdIssueCreate() {
            this.scope = {
                'specificationItem': '=specificationItem',
                'afterSave': '=afterSave'
            };
            this.templateUrl = GSDRequirements.baseUrl + 'issue/create';
            this.controller = ['$scope', "$uibModal", function ($scope, $uibModal) {
                    $scope.addIssue = function () {
                        var modal = $uibModal.open({
                            templateUrl: GSDRequirements.baseUrl + "issue/form",
                            controller: 'ModalIssueAddController',
                            size: 'lg',
                            resolve: {
                                'specificationItem': function () { return $scope.specificationItem; }
                            }
                        });
                        modal.result.then(function () {
                            if ($scope.afterSave) {
                                $scope.afterSave();
                            }
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
