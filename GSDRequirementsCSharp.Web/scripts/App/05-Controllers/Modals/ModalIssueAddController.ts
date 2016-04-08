module Controllers {

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: Globals.GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);


    class ModalIssueAddController {
        constructor($scope: any, $uibModalInstance: any, issueResource) {
            $scope.pendingRequests = 0

            $scope.contentItems = [
                {
                    locale: GSDRequirements.currentLocale,
                    description: ''
                }
            ]

            $scope.addContentItem = (locale) => {
                $scope.contentItems.push({
                    locale: locale,
                    description: ''
                })
            }

            $scope.specificationItemLabel = 'specificationItemLabel';

            $scope.save = () :void => {
                issueResource.save()
            }

        }
    }
    app.controller('ModalIssueAddController', ["$scope", "$uibModalInstance",
        "IssueResource", ModalIssueAddController]);
}