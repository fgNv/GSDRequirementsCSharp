module directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdProjectTranslation{
        public scope = { 'project': '=project', 'afterSave': '=afterSave' };
        public templateUrl = GSDRequirements.baseUrl + 'project/translation'
        private defineDisplayDescription = ($scope) => {
            var content = null;
            if (GSDRequirements.currentLocale == 'en-US') {
                content = $scope.project.projectContents[0];
            } else {
                content = _.first($scope.project.projectContents, (pc) => pc.locale == 'en-US');
                if (!content) {
                    content = $scope.project.projectContents[0]
                }
            }
            $scope.originalDescriptionLocale = content.locale
            $scope.originalDescription = content.description
        }
        public controller = ['$scope', 'ProjectContentResource', ($scope: any, ProjectContentResource: any) => {
            $scope.pendingRequests = 0;
            var self = this
            $scope.$watch('project', (newValue, oldValue) => {
                if (newValue) {
                    self.defineDisplayDescription($scope)
                }
            })

            $scope.save = () => {
                $scope.pendingRequests++;

                var request = {projectId : $scope.project.id, description: $scope.descriptionTranslation }

                ProjectContentResource.save(request)
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