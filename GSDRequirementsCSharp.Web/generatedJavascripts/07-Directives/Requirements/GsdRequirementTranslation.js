var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdRequirementTranslation = (function () {
        function GsdRequirementTranslation() {
            var _this = this;
            this.scope = { 'requirement': '=requirement', 'afterSave': '=afterSave' };
            this.templateUrl = GSDRequirements.baseUrl + 'requirement/translation';
            this.controller = ['$scope', 'RequirementTranslationResource',
                function ($scope, RequirementTranslationResource) {
                    $scope.pendingRequests = 0;
                    $scope.availableLocaleContents = [];
                    $scope.translations = [];
                    $scope.translationsAlreadyProvided = [];
                    $scope.displayLocale = null;
                    $scope.originalDescription = '';
                    $scope.originalName = '';
                    $scope.requirement = null;
                    $scope.cancel = function () {
                        $scope.requirement = null;
                        window.location.href = '#';
                    };
                    var self = _this;
                    $scope.$watch('requirement', function (newValue, oldValue) {
                        if (!newValue) {
                            return;
                        }
                        _this.clearScope($scope);
                        self.defineAvailableLocaleContents($scope, newValue);
                        $scope.translationsAlreadyProvided = _.map($scope.availableLocaleContents, function (c) { return c.name; });
                        $scope.displayLocale = $scope.translationsAlreadyProvided[0];
                        setDisplayLocale(newValue, $scope.displayLocale);
                    });
                    function setDisplayLocale(requirement, locale) {
                        var content = _.find(requirement.requirementContents, function (c) { return c.locale == locale; });
                        $scope.originalAction = content.action;
                        $scope.originalCondition = content.condition;
                        $scope.originalSubject = content.subject;
                        $scope.originalData = {
                            'action': content.action,
                            'condition': content.condition,
                            'subject': content.subject
                        };
                    }
                    $scope.$watch('displayLocale', function (newValue, oldValue) {
                        if (!newValue || !$scope.requirement) {
                            return;
                        }
                        setDisplayLocale($scope.requirement, newValue);
                    });
                    $scope.save = function () {
                        $scope.pendingRequests++;
                        var request = { id: $scope.requirement.id, items: $scope.translations };
                        RequirementTranslationResource.save(request)
                            .$promise
                            .then(function () {
                            Notification.notifySuccess(Sentences.translationAddedSuccessfully);
                            if ($scope.afterSave) {
                                $scope.afterSave();
                            }
                            $scope.requirement = null;
                            window.location.href = "#";
                        }).catch(function (error) {
                            Notification.notifyError(Sentences.errorAddingTranslation, error.data.messages);
                        }).finally(function () {
                            $scope.pendingRequests--;
                        });
                    };
                }];
        }
        GsdRequirementTranslation.prototype.defineAvailableLocaleContents = function ($scope, requirement) {
            var latestVersionContent = _.max(requirement.requirementContents, function (c) { return c.version; });
            var latestVersion = latestVersionContent.version;
            var requirementLocales = _.chain(requirement.requirementContents)
                .filter(function (c) { return c.version == latestVersion; })
                .map(function (c) { return c.locale; })
                .value();
            $scope.availableLocaleContents = _.filter(GSDRequirements.localesAvailable, function (l) { return _.any(requirementLocales, function (pl) { return pl == l.name; }); });
        };
        GsdRequirementTranslation.prototype.clearScope = function ($scope) {
            $scope.availableLocaleContents = [];
            $scope.translations = [];
            $scope.displayLocale = null;
            $scope.originalAction = '';
            $scope.originalSubject = '';
            $scope.originalCondition = '';
            $scope.originalData = {};
        };
        GsdRequirementTranslation.Factory = function () {
            return new GsdRequirementTranslation();
        };
        return GsdRequirementTranslation;
    }());
    app.directive('gsdRequirementTranslation', GsdRequirementTranslation.Factory);
})(Directives || (Directives = {}));
//# sourceMappingURL=GsdRequirementTranslation.js.map