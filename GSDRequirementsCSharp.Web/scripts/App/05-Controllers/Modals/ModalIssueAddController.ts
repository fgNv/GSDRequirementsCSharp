module Controllers {

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: Globals.GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class ModalIssueAddController {
        private initializeIssueData($scope) {
            $scope.issueData = {}
            $scope.issueData.locale = GSDRequirements.currentLocale
            $scope.locales = _.map(GSDRequirements.localesAvailable, l => l.name)

            $scope.issue = {}

            _.each(GSDRequirements.localesAvailable, (l): void => {
                $scope.issue[l.name] = {}
                $scope.issue[l.name].description = ''
                $scope.issue[l.name].locale = l.name
            })
        }
        private definePlaceholder($scope, locale, $q) {
            var deferred = $q.defer()
            
            if ($scope.issueData.locale == locale) {
                deferred.reject()
                return deferred.promise;
            }

            var content = $scope.issue[locale]
            if (!content.description) {
                deferred.reject()
                return deferred.promise;
            }

            $scope.placeholder = content.description
            deferred.resolve()

            return deferred.promise
        }
        constructor($scope, $uibModalInstance, issueResource, specificationItem, $q) {
            $scope.pendingRequests = 0
            $scope.availableLocales = []
            $scope.specificationItem = specificationItem

            $scope.utility = {}
            $scope.utility.issueContainsLocale =
                (i) => $scope.issue &&
                    $scope.issue[i] &&
                    $scope.issue[i].description

            this.initializeIssueData($scope)
            $scope.specificationItemLabel = specificationItem.getLabel();

            $scope.placeholder = ''

            $scope.cancel = (): void => {
                $uibModalInstance.dismiss('cancel');
            }

            $scope.$watch('issueData.locale', (newValue, oldValue) => {
                var self = this

                self.definePlaceholder($scope, GSDRequirements.currentLocale, $q)
                    .catch(() => self.definePlaceholder($scope, 'en-US', $q))
                    .catch(() => {
                        var firstContent = _.find($scope.issue, i => i.description)
                        if (firstContent)
                            $scope.placeholder = firstContent.description
                    })
            })

            $scope.save = (): void => {
                $scope.pendingRequests++

                var contents = _.chain($scope.issue)
                    .filter(i => i.description)
                    .map(i => i)
                    .value()

                var request = {
                    'specificationItemId': specificationItem.id,
                    'contents': contents
                }
                issueResource.save(request)
                    .$promise
                    .then((): void=> {
                        Notification.notifySuccess(Sentences.issueCreatedSuccessfully)
                        $uibModalInstance.close()
                    })
                    .catch((error): void=> {
                        Notification.notifyError(Sentences.errorCreatingIssue,
                            error.messages)
                    })
                    .finally((): void=> {
                        $scope.pendingRequests--
                    })
            }
        }
    }
    app.controller('ModalIssueAddController', ["$scope", "$uibModalInstance",
        "IssueResource", 'specificationItem', '$q', ModalIssueAddController]);
}