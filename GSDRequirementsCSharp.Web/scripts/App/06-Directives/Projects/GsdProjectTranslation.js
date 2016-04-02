var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdProjectTranslation = (function () {
        function GsdProjectTranslation() {
            var _this = this;
            this.scope = { 'project': '=project', 'afterSave': '=afterSave' };
            this.templateUrl = GSDRequirements.baseUrl + 'project/translation';
            this.controller = ['$scope', 'ProjectTranslationResource', function ($scope, ProjectTranslationResource) {
                    $scope.pendingRequests = 0;
                    $scope.availableLocaleContents = [];
                    $scope.translations = [];
                    $scope.translationsAlreadyProvided = [];
                    $scope.displayLocale = null;
                    $scope.originalDescription = '';
                    $scope.originalName = '';
                    $scope.project = null;
                    var self = _this;
                    $scope.$watch('project', function (newValue, oldValue) {
                        if (!newValue) {
                            return;
                        }
                        _this.clearScope($scope);
                        self.defineAvailableLocaleContents($scope, newValue);
                        $scope.translationsAlreadyProvided = _.map($scope.availableLocaleContents, function (c) { return c.name; });
                        $scope.displayLocale = $scope.translationsAlreadyProvided[0];
                    });
                    $scope.$watch('displayLocale', function (newValue, oldValue) {
                        if (!newValue || !$scope.project) {
                            return;
                        }
                        var content = _.find($scope.project.projectContents, function (c) { return c.locale == newValue; });
                        $scope.originalDescription = content.description;
                        $scope.originalName = content.name;
                    });
                    $scope.save = function () {
                        $scope.pendingRequests++;
                        var request = { id: $scope.project.id, items: $scope.translations };
                        ProjectTranslationResource.save(request)
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
        GsdProjectTranslation.prototype.defineAvailableLocaleContents = function ($scope, project) {
            var projectLocales = _.map(project.projectContents, function (c) { return c.locale; });
            $scope.availableLocaleContents = _.filter(GSDRequirements.localesAvailable, function (l) { return _.any(projectLocales, function (pl) { return pl == l.name; }); });
        };
        GsdProjectTranslation.prototype.clearScope = function ($scope) {
            $scope.availableLocaleContents = [];
            $scope.translations = [];
            $scope.displayLocale = null;
            $scope.originalDescription = '';
            $scope.originalName = '';
        };
        GsdProjectTranslation.Factory = function () {
            return new GsdProjectTranslation();
        };
        return GsdProjectTranslation;
    })();
    app.directive('gsdProjectTranslation', GsdProjectTranslation.Factory);
})(Directives || (Directives = {}));
