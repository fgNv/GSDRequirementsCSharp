var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdPackageTranslation = (function () {
        function GsdPackageTranslation() {
            var _this = this;
            this.scope = { 'package': '=package', 'afterSave': '=afterSave' };
            this.templateUrl = GSDRequirements.baseUrl + 'package/translation';
            this.controller = ['$scope', 'PackageTranslationResource',
                function ($scope, PackageTranslationResource) {
                    $scope.pendingRequests = 0;
                    $scope.availableLocaleContents = [];
                    $scope.translations = [];
                    $scope.translationsAlreadyProvided = [];
                    $scope.displayLocale = null;
                    $scope.originalDescription = '';
                    $scope.originalName = '';
                    $scope.package = null;
                    var self = _this;
                    $scope.$watch('package', function (newValue, oldValue) {
                        if (!newValue) {
                            return;
                        }
                        _this.clearScope($scope);
                        self.defineAvailableLocaleContents($scope, newValue);
                        $scope.translationsAlreadyProvided = _.map($scope.availableLocaleContents, function (c) { return c.name; });
                        $scope.displayLocale = $scope.translationsAlreadyProvided[0];
                    });
                    $scope.$watch('displayLocale', function (newValue, oldValue) {
                        if (!newValue || !$scope.package) {
                            return;
                        }
                        var content = _.find($scope.package.contents, function (c) { return c.locale == newValue; });
                        $scope.originalDescription = content.description;
                    });
                    $scope.save = function () {
                        $scope.pendingRequests++;
                        var request = { id: $scope.package.id, items: $scope.translations };
                        PackageTranslationResource.save(request)
                            .$promise
                            .then(function () {
                            Notification.notifySuccess(Sentences.translationAddedSuccessfully);
                            if ($scope.afterSave) {
                                $scope.afterSave();
                            }
                            $scope.package = null;
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
        GsdPackageTranslation.prototype.defineAvailableLocaleContents = function ($scope, packageEntity) {
            var packageLocales = _.chain(packageEntity.contents)
                .filter(function (c) { return c.isUpdated; })
                .map(function (c) { return c.locale; })
                .value();
            $scope.availableLocaleContents = _.filter(GSDRequirements.localesAvailable, function (l) { return _.any(packageLocales, function (pl) { return pl == l.name; }); });
        };
        GsdPackageTranslation.prototype.clearScope = function ($scope) {
            $scope.availableLocaleContents = [];
            $scope.translations = [];
            $scope.displayLocale = null;
            $scope.originalDescription = '';
            $scope.originalName = '';
        };
        GsdPackageTranslation.Factory = function () {
            return new GsdPackageTranslation();
        };
        return GsdPackageTranslation;
    })();
    app.directive('gsdPackageTranslation', GsdPackageTranslation.Factory);
})(Directives || (Directives = {}));
//# sourceMappingURL=GsdPackageTranslation.js.map