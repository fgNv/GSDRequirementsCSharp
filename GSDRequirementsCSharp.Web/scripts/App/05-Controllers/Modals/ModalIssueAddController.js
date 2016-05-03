var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var ModalIssueAddController = (function () {
        function ModalIssueAddController($scope, $uibModalInstance, issueResource, specificationItem, $q) {
            var _this = this;
            $scope.pendingRequests = 0;
            $scope.availableLocales = [];
            $scope.specificationItem = specificationItem;
            $scope.utility = {};
            $scope.utility.issueContainsLocale =
                function (i) { return $scope.issue &&
                    $scope.issue[i] &&
                    $scope.issue[i].description; };
            this.initializeIssueData($scope);
            $scope.specificationItemLabel = specificationItem.getLabel();
            $scope.placeholder = '';
            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
            $scope.$watch('issueData.locale', function (newValue, oldValue) {
                var self = _this;
                self.definePlaceholder($scope, GSDRequirements.currentLocale, $q)
                    .catch(function () { return self.definePlaceholder($scope, 'en-US', $q); })
                    .catch(function () {
                    var firstContent = _.find($scope.issue, function (i) { return i.description; });
                    if (firstContent)
                        $scope.placeholder = firstContent.description;
                });
            });
            $scope.save = function () {
                $scope.pendingRequests++;
                var contents = _.chain($scope.issue)
                    .filter(function (i) { return i.description; })
                    .map(function (i) { return i; })
                    .value();
                var request = {
                    'specificationItemId': specificationItem.id,
                    'contents': contents
                };
                issueResource.save(request)
                    .$promise
                    .then(function () {
                    Notification.notifySuccess(Sentences.issueCreatedSuccessfully);
                    $uibModalInstance.close();
                })
                    .catch(function (error) {
                    Notification.notifyError(Sentences.errorCreatingIssue, error.data.messages);
                })
                    .finally(function () {
                    $scope.pendingRequests--;
                });
            };
        }
        ModalIssueAddController.prototype.initializeIssueData = function ($scope) {
            $scope.issueData = {};
            $scope.issueData.locale = GSDRequirements.currentLocale;
            $scope.locales = _.map(GSDRequirements.localesAvailable, function (l) { return l.name; });
            $scope.issue = {};
            _.each(GSDRequirements.localesAvailable, function (l) {
                $scope.issue[l.name] = {};
                $scope.issue[l.name].description = '';
                $scope.issue[l.name].locale = l.name;
            });
        };
        ModalIssueAddController.prototype.definePlaceholder = function ($scope, locale, $q) {
            var deferred = $q.defer();
            if ($scope.issueData.locale == locale) {
                deferred.reject();
                return deferred.promise;
            }
            var content = $scope.issue[locale];
            if (!content.description) {
                deferred.reject();
                return deferred.promise;
            }
            $scope.placeholder = content.description;
            deferred.resolve();
            return deferred.promise;
        };
        return ModalIssueAddController;
    })();
    app.controller('ModalIssueAddController', ["$scope", "$uibModalInstance",
        "IssueResource", 'specificationItem', '$q', ModalIssueAddController]);
})(Controllers || (Controllers = {}));
//# sourceMappingURL=ModalIssueAddController.js.map