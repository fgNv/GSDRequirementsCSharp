var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdPackageForm = (function () {
        function GsdPackageForm() {
            this.scope = { 'package': '=package', 'afterSave': '=afterSave' };
            this.templateUrl = GSDRequirements.baseUrl + 'package/form';
            this.controller = ['$scope', 'PackageResource', function ($scope, PackageResource) {
                    $scope.pendingRequests = 0;
                    $scope.save = function () {
                        $scope.pendingRequests++;
                        var promise = $scope.package.id ?
                            PackageResource.update($scope.package).$promise :
                            PackageResource.save($scope.package).$promise;
                        var successMessage = $scope.package.id ?
                            Sentences.packageUpdatedSuccessfully :
                            Sentences.packageSuccessfullyCreated;
                        promise.then(function () {
                            Notification.notifySuccess(successMessage);
                            if ($scope.afterSave) {
                                $scope.afterSave();
                            }
                            $scope.package = null;
                        })
                            .catch(function (error) {
                            Notification.notifyError(Sentences.errorSavingPackage, error.data.messages);
                        })
                            .finally(function () {
                            $scope.pendingRequests--;
                        });
                    };
                }];
        }
        GsdPackageForm.Factory = function () {
            return new GsdPackageForm();
        };
        return GsdPackageForm;
    })();
    app.directive('gsdPackageForm', GsdPackageForm.Factory);
})(Directives || (Directives = {}));
