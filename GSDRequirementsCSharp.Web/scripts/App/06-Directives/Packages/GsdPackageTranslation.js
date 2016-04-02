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
                    var self = _this;
                    $scope.$watch('package', function (newValue, oldValue) {
                        if (newValue) {
                            $scope.originalDescriptionLocale = newValue.locale;
                            $scope.originalDescription = newValue.description;
                        }
                    });
                    $scope.save = function () {
                        $scope.pendingRequests++;
                        var request = {
                            id: $scope.package.id,
                            description: $scope.descriptionTranslation
                        };
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
        GsdPackageTranslation.Factory = function () {
            return new GsdPackageTranslation();
        };
        return GsdPackageTranslation;
    })();
    app.directive('gsdPackageTranslation', GsdPackageTranslation.Factory);
})(Directives || (Directives = {}));
