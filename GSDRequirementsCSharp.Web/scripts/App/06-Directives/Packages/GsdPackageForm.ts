module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdPackageForm {
        public scope = { 'package': '=package', 'afterSave': '=afterSave' };
        public templateUrl = GSDRequirements.baseUrl + 'package/form'
        public controller = ['$scope', 'PackageResource', ($scope: any, PackageResource: any) => {
            $scope.pendingRequests = 0;

            $scope.save = () => {
                $scope.pendingRequests++;
                var promise = $scope.package.id ?
                    PackageResource.update($scope.package).$promise :
                    PackageResource.save($scope.package).$promise

                var successMessage = $scope.package.id ?
                    Sentences.packageUpdatedSuccessfully :
                    Sentences.packageSuccessfullyCreated;

                promise.then(function () {
                    Notification.notifySuccess(successMessage);
                    if ($scope.afterSave) { $scope.afterSave() }
                        $scope.package = null
                    })
                    .catch(function (error) {
                        Notification.notifyError(Sentences.errorSavingPackage, error.data.messages)
                    })
                    .finally(function () {
                        $scope.pendingRequests--;
                    });
            }
        }]
        public static Factory() {
            return new GsdPackageForm();
        }
    }
    app.directive('gsdPackageForm', GsdPackageForm.Factory)
}