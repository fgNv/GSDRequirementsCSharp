var directives;
(function (directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdRequirementTranslation = (function () {
        function GsdRequirementTranslation() {
            var _this = this;
            this.scope = { 'requirement': '=requirement', 'afterSave': '=afterSave' };
            this.templateUrl = GSDRequirements.baseUrl + 'requirement/translation';
            this.defineDisplayDescription = function ($scope) {
                var content = null;
                if (GSDRequirements.currentLocale == 'en-US') {
                    content = $scope.requirement.contents[0];
                }
                else {
                    content = _.first($scope.requirement.contents, function (pc) { return pc.locale == 'en-US'; });
                    if (!content) {
                        content = $scope.requirement.contents[0];
                    }
                }
                $scope.originalDescriptionLocale = content.locale;
                $scope.originalDescription = content.description;
            };
            this.controller = ['$scope', 'RequirementContentResource',
                function ($scope, RequirementContentResource) {
                    $scope.pendingRequests = 0;
                    var self = _this;
                    $scope.$watch('requirement', function (newValue, oldValue) {
                        if (newValue) {
                            self.defineDisplayDescription($scope);
                        }
                    });
                    $scope.save = function () {
                        $scope.pendingRequests++;
                        $scope.data.requirementId = $scope.requirement.id;
                        RequirementContentResource.save($scope.data)
                            .$promise
                            .then(function () {
                            Notification.notifySuccess(Sentences.translationAddedSuccessfully);
                            if ($scope.afterSave) {
                                $scope.afterSave();
                            }
                            $scope.requirement = null;
                        })
                            .catch(function (error) {
                            Notification.notifyError(Sentences.errorAddingTranslation, error.data.messages);
                        })
                            .finally(function () {
                            $scope.pendingRequests--;
                        });
                    };
                }];
        }
        GsdRequirementTranslation.Factory = function () {
            return new GsdRequirementTranslation();
        };
        return GsdRequirementTranslation;
    })();
    app.directive('gsdRequirementTranslation', GsdRequirementTranslation.Factory);
})(directives || (directives = {}));
