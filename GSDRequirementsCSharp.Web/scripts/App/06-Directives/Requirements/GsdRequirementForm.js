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
                    $scope.translations = [];
                    _this.LoadPackagesOptions(PackageResource, $scope);
                    $scope.difficultyOptions = Globals.enumerateEnum(Models.Difficulty);
                    $scope.requirementTypeOptions = Globals.enumerateEnum(Models.RequirementType);
                    $scope.$watch("requirement", function (newValue, oldValue) {
                        if (!newValue)
                            return;
                        $scope.translations = [];
                        $scope.conditionPlaceholder = $scope.defaultConditionPlaceholder;
                        $scope.actionPlaceholder = $scope.defaultActionPlaceholder;
                        $scope.subjectPlaceholder = $scope.defaultSubjectPlaceholder;
                    });
                    $scope.save = function () {
                        $scope.pendingRequests++;
                        $scope.requirement.items = [
                            {
                                "action": $scope.requirement.action,
                                "subject": $scope.requirement.subject,
                                "condition": $scope.requirement.condition,
                                "locale": GSDRequirements.currentLocale
                            }
                        ];
                        _.each($scope.translations, function (i) { return $scope.requirement.items.push(i); });
                        var promise = $scope.requirement.id ?
                            RequirementResource.update($scope.requirement).$promise :
                            RequirementResource.save($scope.requirement).$promise;
                        var successMessage = $scope.requirement.id ?
                            Sentences.requirementUpdatedSuccessfully :
                            Sentences.requirementSuccessfullyCreated;
                        promise
                            .then(function () {
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
            $scope.pendingRequests++;
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