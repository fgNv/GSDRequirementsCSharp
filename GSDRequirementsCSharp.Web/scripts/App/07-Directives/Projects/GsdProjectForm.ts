module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdProjectForm {
        public scope = { 'project': '=project', 'afterSave': '=afterSave' };
        public templateUrl = GSDRequirements.baseUrl + 'project/form'
        public controller = ['$scope', 'ProjectResource', '$uibModal', ($scope: any, ProjectResource: any, $uibModal: any) => {
            $scope.pendingRequests = 0;
            $scope.translations = []
            $scope.translationsAlreadyProvided = [GSDRequirements.currentLocale]

            $scope.locales = _.map(GSDRequirements.localesAvailable, l => l.name)

            $scope.$watch("project", (newValue, oldValue) => {
                if (!newValue)
                    return

                $scope.translations = []
            })

            $scope.save = () => {
                $scope.pendingRequests++;

                $scope.project.items = []

                var contents = _.filter($scope.project.contentDictionary,
                    (i) => i.name && i.description)
                
                _.each(contents, (i): void => $scope.project.items.push(i))

                var promise = $scope.project.id ?
                    ProjectResource.update($scope.project).$promise :
                    ProjectResource.save($scope.project).$promise

                var successMessage = $scope.project.id ?
                    Sentences.projectUpdatedSuccessfully :
                    Sentences.projectSuccessfullyCreated;

                promise.then((): void=> {
                    Notification.notifySuccess(successMessage);
                    $scope.$emit(Globals.EventNames.projectListChanged)
                    if ($scope.afterSave) { $scope.afterSave() }
                    window.location.href = "#"
                    $scope.project = null
                }).catch((error): void=> {
                    Notification.notifyError(Sentences.errorSavingProject, error.data.messages)
                }).finally((): void => {
                    $scope.pendingRequests--;
                });
            }
        }]
        public static Factory() {
            return new GsdProjectForm();
        }
    }
    app.directive('gsdProjectForm', GsdProjectForm.Factory)
}