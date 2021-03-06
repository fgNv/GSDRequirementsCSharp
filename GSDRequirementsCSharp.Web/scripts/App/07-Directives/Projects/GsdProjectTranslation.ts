﻿module Directives {

    declare var angular: any;
    declare var GSDRequirements: Globals.GSDRequirementsData;
    declare var _: any;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdProjectTranslation {
        public scope = { 'project': '=project', 'afterSave': '=afterSave' };
        public templateUrl = GSDRequirements.baseUrl + 'project/translation'
        private defineAvailableLocaleContents($scope, project) {
            var projectLocales = _.chain(project.projectContents)
                .filter((c: Models.ProjectContent) => c.isUpdated == true)
                .map(c => c.locale)
                .value();
            
            $scope.availableLocaleContents = _.filter(GSDRequirements.localesAvailable,
                l => _.any(projectLocales, (pl) => pl == l.name))
        }
        private clearScope($scope) {
            $scope.availableLocaleContents = []
            $scope.translations = []
            $scope.displayLocale = null
            $scope.originalDescription = ''
            $scope.originalName = ''
        }
        public controller = ['$scope', 'ProjectTranslationResource', ($scope: any, ProjectTranslationResource: any) => {
            $scope.pendingRequests = 0;

            $scope.availableLocaleContents = []
            $scope.translations = []
            $scope.translationsAlreadyProvided = []
            $scope.displayLocale = null
            $scope.originalDescription = ''
            $scope.originalName = ''
            $scope.project = null;

            $scope.setDisplayLocale = (locale) :void => {
                $scope.displayLocale = locale
            }

            var self = this
            $scope.$watch('project', (newValue, oldValue) => {
                if (!newValue) { return }

                this.clearScope($scope)
                self.defineAvailableLocaleContents($scope, newValue)
                $scope.translationsAlreadyProvided = _.map($scope.availableLocaleContents, (c: Models.Locale) => c.name)
                $scope.displayLocale = $scope.translationsAlreadyProvided[0]
                setDisplayLocale(newValue, $scope.displayLocale)
            })

            function setDisplayLocale(project, locale) {
                if (!project) return;
                var content = <Models.ProjectContent>_.find(project.projectContents,
                    (c: Models.ProjectContent) => c.locale == locale)

                $scope.originalDescription = content.description
                $scope.originalName = content.name
            }

            $scope.$watch('displayLocale', (newValue, oldValue) => {
                if (!newValue || !$scope.project) { return; }
                setDisplayLocale($scope.project, newValue)
            })

            $scope.save = () => {
                $scope.pendingRequests++;

                var request = { id: $scope.project.id, items: $scope.translations }

                ProjectTranslationResource.save(request)
                    .$promise
                    .then(function () {
                        Notification.notifySuccess(Sentences.translationAddedSuccessfully);
                        if ($scope.afterSave) { $scope.afterSave() }
                        $scope.project = null
                        $scope.$emit(Globals.EventNames.projectListChanged)
                        window.location.href = "#"
                    })
                    .catch(function (error) {
                        Notification.notifyError(Sentences.errorAddingTranslation, error.data.messages)
                    })
                    .finally(function () {
                        $scope.pendingRequests--;
                    });
            }
        }]
        public static Factory() {
            return new GsdProjectTranslation();
        }
    }
    app.directive('gsdProjectTranslation', GsdProjectTranslation.Factory)
}