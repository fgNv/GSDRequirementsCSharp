var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var ModalIssueAddController = (function () {
        function ModalIssueAddController($scope, $uibModalInstance, issueResource, specificationItem) {
            $scope.pendingRequests = 0;
            $scope.availableLocales = [];
            $scope.specificationItem = specificationItem;
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
            $scope.specificationItemLabel = specificationItem.getLabel();
            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
            $scope.save = function () {
                $scope.pendingRequests++;
                var request = {
                    'specificationItemId': specificationItem.id,
                    'contents': $scope.contentItems
                };
                issueResource.save(request)
                    .$promise
                    .then(function () {
                    Notification.notifySuccess(Sentences.issueCreatedSuccessfully);
                    $uibModalInstance.close();
                })
                    .catch(function (error) {
                    Notification.notifyError(Sentences.errorCreatingIssue, error.messages);
                })
                    .finally(function () {
                    $scope.pendingRequests--;
                });
            };
        }
        return ModalIssueAddController;
    })();
    app.controller('ModalIssueAddController', ["$scope", "$uibModalInstance",
        "IssueResource", 'specificationItem', ModalIssueAddController]);
})(Controllers || (Controllers = {}));
