﻿module Controllers {

    declare var angular: any
    declare var _: any
    declare var GSDRequirements: Globals.GSDRequirementsData
    var app = angular.module(GSDRequirements.angularModuleName)

    class ModalItemIssuesController {
        private loadIssues($scope, itemIssuesResource, specificationItem) {
            $scope.pendingRequests++;
            itemIssuesResource.query({ id: specificationItem.id })
                .$promise
                .then((issues): void => {
                    $scope.issues = _.map(issues, i => new Models.Issue(i))
                })
                .catch((error): void => {
                    Notification.notifyError(Sentences.errorLoadingIssues, error.messages)
                })
                .finally((): void => {
                    $scope.pendingRequests--;
                });
        }
        private populateDetailData($scope, issue) {
            var locales = _.chain(issue.contents)
                .map(i => i.locale)
                .sortBy(l => l)
                .value()

            $scope.availableContentLocales = locales

            $scope.displayLocale =
                _.find(locales, l => l == GSDRequirements.currentLocale) ||
                _.find(locales, l => l == "en-US") ||
                locales[0]

            this.defineDisplayContent($scope, issue, $scope.displayLocale)
        }
        private defineDisplayContent($scope, issue, locale) {
            var content = _.find(issue.contents, c => c.locale == locale)
            if (!content) return;
            $scope.currentDescription = content.description
        }
        constructor($scope, $uibModalInstance, itemIssuesResource,
            specificationItem, $rootScope, $location) {
            $scope.pendingRequests = 0
            $scope.specificationItem = specificationItem
            $scope.issues = []
            $scope.availableContentLocales = []
            $scope.currentDescription = ''

            $scope.$watch('displayLocale', (newValue, oldValue) => {
                if (!$scope.issueInDetail) return

                this.defineDisplayContent($scope, $scope.issueInDetail, newValue)
            })

            $rootScope.$on('$locationChangeStart', (event, newUrl, oldUrl): void => {
                var pathValues = $location.path().split('/')
                var step = pathValues[1];
                var identifier = pathValues[2];

                if (step == "issue" && identifier) {
                    var issue = _.find($scope.issues, i => i.identifier == identifier)
                    if (!issue) return;
                    $scope.issueInDetail = issue;
                    this.populateDetailData($scope, issue)
                }

                if (!step) {
                    $scope.issueInDetail = null
                }
            });

            this.loadIssues($scope, itemIssuesResource, specificationItem)
        }
    }

    app.controller('ModalItemIssuesController', ["$scope", "$uibModalInstance",
        "ItemIssuesResource", 'specificationItem', '$rootScope', '$location',
        ModalItemIssuesController]);
}