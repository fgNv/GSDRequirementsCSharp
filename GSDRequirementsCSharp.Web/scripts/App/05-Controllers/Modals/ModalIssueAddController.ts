module Controllers {

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: Globals.GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);


    class ModalIssueAddController {
        constructor($scope: any, $uibModalInstance: any, issueResource) {
            $scope.pendingRequests = 0

            $scope.availableLocales = []
            
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

            $scope.specificationItemLabel = 'specificationItemLabel';

            $scope.save = (): void => {
                issueResource.save()
            }

        }
    }
    app.controller('ModalIssueAddController', ["$scope", "$uibModalInstance",
        "IssueResource", ModalIssueAddController]);
}