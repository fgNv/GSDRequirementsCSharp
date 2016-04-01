var directives;
(function (directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdProjectForm = (function () {
        function GsdProjectForm() {
            this.scope = { 'project': '=project', 'afterSave': '=afterSave' };
            this.templateUrl = GSDRequirements.baseUrl + 'project/form';
            this.controller = ['$scope', 'ProjectResource', function ($scope, ProjectResource) {
                    $scope.pendingRequests = 0;
                    $scope.save = function () {
                        $scope.pendingRequests++;
                        var promise = $scope.project.id ?
                            ProjectResource.update($scope.project).$promise :
                            ProjectResource.save($scope.project).$promise;
                        var successMessage = $scope.project.id ?
                            Sentences.projectUpdatedSuccessfully :
                            Sentences.projectSuccessfullyCreated;
                        promise.then(function () {
                            Notification.notifySuccess(successMessage);
                            $scope.$emit(Globals.EventNames.projectListChanged);
                            if ($scope.afterSave) {
                                $scope.afterSave();
                            }
                            $scope.project = null;
                        })
                            .catch(function (error) {
                            Notification.notifyError(Sentences.errorSavingProject, error.data.messages);
                        })
                            .finally(function () {
                            $scope.pendingRequests--;
                        });
                    };
                }];
        }
        GsdProjectForm.Factory = function () {
            return new GsdProjectForm();
        };
        return GsdProjectForm;
    })();
    app.directive('gsdProjectForm', GsdProjectForm.Factory);
})(directives || (directives = {}));
