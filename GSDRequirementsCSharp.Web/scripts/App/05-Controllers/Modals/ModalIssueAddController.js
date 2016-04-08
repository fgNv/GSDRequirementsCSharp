var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var ModalIssueAddController = (function () {
        function ModalIssueAddController($scope, $uibModalInstance, issueResource) {
            $scope.pendingRequests = 0;
            $scope.contentItems = [
                {
                    locale: GSDRequirements.currentLocale,
                    description: ''
                }
            ];
            $scope.addContentItem = function (locale) {
                $scope.contentItems.push({
                    locale: locale,
                    description: ''
                });
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
