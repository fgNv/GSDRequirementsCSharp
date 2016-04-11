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
            
            $scope.$watch("project", (newValue, oldValue) => {
                if (!newValue)
                    return

                $scope.translations = []
            })

            $scope.save = () => {
                $scope.pendingRequests++;

                $scope.project.items = [
                    {
                        "name": $scope.project.name,
                        "description": $scope.project.description,
                        "locale": GSDRequirements.currentLocale
                    }
                ]

                _.each($scope.translations, (i): void=> $scope.project.items.push(i))

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