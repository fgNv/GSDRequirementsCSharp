var directives;
(function (directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdRequirementForm = (function () {
        function GsdRequirementForm() {
            this.scope = { 'requirement': '=requirement', 'afterSave': '=afterSave' };
            this.templateUrl = GSDRequirements.baseUrl + 'requirement/form';
            this.controller = ['$scope', 'RequirementResource', function ($scope, RequirementResource) {
                    $scope.pendingRequests = 0;
                    $scope.save = function () {
                        $scope.pendingRequests++;
                        var promise = $scope.requirement.id ?
                            RequirementResource.update($scope.requirement).$promise :
                            RequirementResource.save($scope.requirement).$promise;
                        var successMessage = $scope.requirement.id ?
                            Sentences.requirementUpdatedSuccessfully :
                            Sentences.requirementSuccessfullyCreated;
                        promise.then(function () {
                            Notification.notifySuccess(successMessage);
                            if ($scope.afterSave) {
                                $scope.afterSave();
                            }
                            $scope.requirement = null;
                        })
                            .catch(function (error) {
                            Notification.notifyError(Sentences.errorSavingRequirement, error.data.messages);
                        })
                            .finally(function () {
                            $scope.pendingRequests--;
                        });
                    };
                }];
        }
        GsdRequirementForm.Factory = function () {
            return new GsdRequirementForm();
        };
        return GsdRequirementForm;
    })();
    app.directive('gsdRequirementForm', GsdRequirementForm.Factory);
})(directives || (directives = {}));
//# sourceMappingURL=GsdRequirementForm.js.map