module Directives {

    declare var angular: any;
    declare var GSDRequirements: Globals.GSDRequirementsData;
    declare var _: any;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdProjectTranslation {
        public scope = { 'project': '=project', 'afterSave': '=afterSave' };
        public templateUrl = GSDRequirements.baseUrl + 'project/translation'
        private defineAvailableLocaleContents($scope, project) {
            var projectLocales = _.map(project.projectContents, c => c.locale)

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

            var self = this
            $scope.$watch('project', (newValue, oldValue) => {
                if (!newValue) { return }

                this.clearScope($scope)
                self.defineAvailableLocaleContents($scope, newValue)
                $scope.translationsAlreadyProvided = _.map($scope.availableLocaleContents, (c: Models.Locale) => c.name)
                $scope.displayLocale = $scope.translationsAlreadyProvided[0]
            })

            $scope.$watch('displayLocale', (newValue, oldValue) => {
                if (!newValue || !$scope.project) { return; }

                var content = <Models.ProjectContent>_.find($scope.project.projectContents,
                    (c: Models.ProjectContent) => c.locale == newValue)

                $scope.originalDescription = content.description
                $scope.originalName = content.name
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