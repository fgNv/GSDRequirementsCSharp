module directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdProjectForm {
        public scope = { 'project': '=project', 'afterSave': '=afterSave' };
        public templateUrl = GSDRequirements.baseUrl + 'project/form'
        public controller = ['$scope', 'ProjectResource', ($scope: any, ProjectResource: any) => {
            $scope.pendingRequests = 0;

            $scope.save = () => {
                $scope.pendingRequests++;
                var promise = $scope.project.id ?
                    ProjectResource.update($scope.project).$promise :
                    ProjectResource.save($scope.project).$promise

                var successMessage = $scope.project.id ?
                    Sentences.projectUpdatedSuccessfully :
                    Sentences.projectSuccessfullyCreated;

                promise.then(function () {
                    Notification.notifySuccess(successMessage);
                    $scope.$emit(Globals.EventNames.projectListChanged)
                    if ($scope.afterSave) { $scope.afterSave() }
                        $scope.project = null
                    })
                    .catch(function (error) {
                        Notification.notifyError(Sentences.errorSavingProject, error.data.messages)
                    })
                    .finally(function () {
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