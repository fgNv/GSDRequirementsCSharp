var directives;
(function (directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdProjectTranslation = (function () {
        function GsdProjectTranslation() {
            var _this = this;
            this.scope = { 'project': '=project', 'afterSave': '=afterSave' };
            this.templateUrl = GSDRequirements.baseUrl + 'project/translation';
            this.defineDisplayDescription = function ($scope) {
                var content = null;
                if (GSDRequirements.currentLocale == 'en-US') {
                    content = $scope.project.projectContents[0];
                }
                else {
                    content = _.first($scope.project.projectContents, function (pc) { return pc.locale == 'en-US'; });
                    if (!content) {
                        content = $scope.project.projectContents[0];
                    }
                }
                $scope.originalDescriptionLocale = content.locale;
                $scope.originalDescription = content.description;
            };
            this.controller = ['$scope', 'ProjectContentResource', function ($scope, ProjectContentResource) {
                    $scope.pendingRequests = 0;
                    var self = _this;
                    $scope.$watch('project', function (newValue, oldValue) {
                        if (newValue) {
                            self.defineDisplayDescription($scope);
                        }
                    });
                    $scope.save = function () {
                        $scope.pendingRequests++;
                        var request = { projectId: $scope.project.id, description: $scope.descriptionTranslation };
                        ProjectContentResource.save(request)
                            .$promise
                            .then(function () {
                            Notification.notifySuccess(Sentences.translationAddedSuccessfully);
                            if ($scope.afterSave) {
                                $scope.afterSave();
                            }
                            $scope.project = null;
                            $scope.$emit(Globals.EventNames.projectListChanged);
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
        GsdProjectTranslation.Factory = function () {
            return new GsdProjectTranslation();
        };
        return GsdProjectTranslation;
    })();
    app.directive('gsdProjectTranslation', GsdProjectTranslation.Factory);
})(directives || (directives = {}));
//# sourceMappingURL=GsdProjectTranslation.js.map