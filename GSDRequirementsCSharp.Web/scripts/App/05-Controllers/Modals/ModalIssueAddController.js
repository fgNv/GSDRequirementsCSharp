var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var ModalIssueAddController = (function () {
        function ModalIssueAddController($scope, $uibModalInstance, issueResource) {
            $scope.pendingRequests = 0;
            $scope.availableLocales = [];
            $scope.contentItems = [
                {
                    locale: GSDRequirements.currentLocale,
                    description: ''
                }
            ];
            function setAvailableLocales() {
                $scope.availableLocales = _.filter(GSDRequirements.localesAvailable, function (l) {
                    return !_.any($scope.contentItems, function (ci) { return ci.locale == l.name; });
                });
            }
            setAvailableLocales();
            $scope.removeContentItem = function (contentItem) {
                $scope.contentItems = _.filter($scope.contentItems, function (ci) { return ci != contentItem; });
                setAvailableLocales();
            };
            $scope.addContentItem = function (locale) {
                $scope.contentItems.push({
                    locale: locale.name,
                    description: ''
                });
                setAvailableLocales();
            };
            $scope.specificationItemLabel = 'specificationItemLabel';
            $scope.save = function () {
                issueResource.save();
            };
        }
        return ModalIssueAddController;
    })();
    app.controller('ModalIssueAddController', ["$scope", "$uibModalInstance",
        "IssueResource", ModalIssueAddController]);
})(Controllers || (Controllers = {}));
//# sourceMappingURL=ModalIssueAddController.js.map