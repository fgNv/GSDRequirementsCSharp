﻿module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdRequirementTranslation {
        public scope = { 'requirement': '=requirement', 'afterSave': '=afterSave' };
        public templateUrl = GSDRequirements.baseUrl + 'requirement/translation'
        private defineAvailableLocaleContents($scope, requirement: Models.Requirement) {
            var requirementLocales = _.chain(requirement.requirementContents)
                .map(c => c.locale)
                .value();

            $scope.availableLocaleContents = _.filter(GSDRequirements.localesAvailable,
                l => _.any(requirementLocales, (pl) => pl == l.name))
        }
        private clearScope($scope) {
            $scope.availableLocaleContents = []
            $scope.translations = []
            $scope.displayLocale = null
            $scope.originalAction = ''
            $scope.originalSubject = ''
            $scope.originalCondition = ''
        }
        public controller = ['$scope', 'RequirementTranslationResource',
            ($scope: any, RequirementTranslationResource: any) => {
                $scope.pendingRequests = 0

                $scope.availableLocaleContents = []
                $scope.translations = []
                $scope.translationsAlreadyProvided = []
                $scope.displayLocale = null
                $scope.originalDescription = ''
                $scope.originalName = ''
                $scope.requirement = null;

                var self = this
                $scope.$watch('requirement', (newValue, oldValue) => {
                    if (!newValue) { return }

                    this.clearScope($scope)
                    self.defineAvailableLocaleContents($scope, newValue)
                    $scope.translationsAlreadyProvided = _.map($scope.availableLocaleContents, (c: Models.Locale) => c.name)
                    $scope.displayLocale = $scope.translationsAlreadyProvided[0]
                })
                
                $scope.$watch('displayLocale', (newValue, oldValue) => {
                    if (!newValue || !$scope.requirement) { return; }

                    var content = <Models.RequirementContent>_.find($scope.requirement.requirementContents,
                        (c: Models.RequirementContent) => c.locale == newValue)

                    $scope.originalAction = content.action
                    $scope.originalCondition = content.condition
                    $scope.originalSubject = content.subject
                })

                $scope.save = () => {
                    $scope.pendingRequests++;
                    
                    var request = { id: $scope.requirement.id, items: $scope.translations }

                    RequirementTranslationResource.save(request)
                        .$promise
                        .then(function () {
                            Notification.notifySuccess(Sentences.translationAddedSuccessfully);
                            if ($scope.afterSave) { $scope.afterSave() }
                            $scope.requirement = null
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
            return new GsdRequirementTranslation();
        }
    }
    app.directive('gsdRequirementTranslation', GsdRequirementTranslation.Factory)
}