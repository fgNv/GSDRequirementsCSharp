module Controllers {

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: Globals.GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);


    class ModalIssueAddController {
        constructor($scope, $uibModalInstance, issueResource, specificationItem) {
            $scope.pendingRequests = 0
            $scope.availableLocales = []
            $scope.specificationItem = specificationItem;
            
            $scope.contentItems = [
                {
                    locale: GSDRequirements.currentLocale,
                    description: ''
                }
            ]
            
            function setAvailableLocales() {
                $scope.availableLocales = _.filter(GSDRequirements.localesAvailable, (l) => {
                    return !_.any($scope.contentItems,(ci) => ci.locale == l.name)
                });
            }

            setAvailableLocales()

            $scope.removeContentItem = (contentItem) => {
                $scope.contentItems = _.filter($scope.contentItems, (ci) => ci != contentItem);
                setAvailableLocales()
            }

            $scope.addContentItem = (locale) => {
                $scope.contentItems.push({
                    locale: locale.name,
                    description: ''
                })
                setAvailableLocales()
            }

            $scope.specificationItemLabel = specificationItem.getLabel();

            $scope.cancel = (): void => {
                $uibModalInstance.dismiss('cancel');
            }

            $scope.save = (): void => {
                $scope.pendingRequests++

                var request = {
                    'specificationItemId': specificationItem.id,
                    'contents': $scope.contentItems
                }
                issueResource.save(request)
                    .$promise
                    .then(() :void=> {
                        Notification.notifySuccess(Sentences.issueCreatedSuccessfully)
                        $uibModalInstance.close();
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
        "IssueResource", 'specificationItem', ModalIssueAddController]);
}