var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdRequirementForm = (function () {
        function GsdRequirementForm() {
            var _this = this;
            this.scope = { 'requirement': '=requirement', 'afterSave': '=afterSave' };
            this.templateUrl = GSDRequirements.baseUrl + 'requirement/form';
            this.controller = ['$scope', 'RequirementResource', 'PackageResource',
                function ($scope, RequirementResource, PackageResource) {
                    $scope.pendingRequests = 0;
                    _this.LoadPackagesOptions(PackageResource, $scope);
                    $scope.difficultyOptions = Globals.enumerateEnum(Models.difficulty);
                    $scope.requirementTypeOptions = Globals.enumerateEnum(Models.requirementType);
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
        GsdRequirementForm.prototype.LoadPackagesOptions = function (packageResource, $scope) {
            packageResource.query()
                .$promise
                .then(function (response) {
                $scope.packagesOptions = _.map(response, function (r) { return new Models.Package(r); });
            })
                .catch(function (err) {
                Notification.notifyError(Sentences.errorLoadingPackages, err.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        GsdRequirementForm.Factory = function () {
            return new GsdRequirementForm();
        };
        return GsdRequirementForm;
    })();
    app.directive('gsdRequirementForm', GsdRequirementForm.Factory);
})(Directives || (Directives = {}));
//# sourceMappingURL=GsdRequirementForm.js.map