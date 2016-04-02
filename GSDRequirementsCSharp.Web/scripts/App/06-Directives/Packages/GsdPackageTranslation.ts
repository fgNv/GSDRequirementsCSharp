module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdPackageTranslation {
        public scope = { 'package': '=package', 'afterSave': '=afterSave' };
        public templateUrl = GSDRequirements.baseUrl + 'package/translation'        
        public controller = ['$scope', 'PackageTranslationResource',
            ($scope: any, PackageTranslationResource: any) => {
                $scope.pendingRequests = 0;
                var self = this
                $scope.$watch('package', (newValue, oldValue) => {
                    if (newValue) {
                        $scope.originalDescriptionLocale = newValue.locale
                        $scope.originalDescription = newValue.description
                    }
                })

                $scope.save = () => {
                    $scope.pendingRequests++;

                    var request = {
                        id: $scope.package.id,
                        description: $scope.descriptionTranslation
                    }

                    PackageTranslationResource.save(request)
                        .$promise
                        .then(function () {
                            Notification.notifySuccess(Sentences.translationAddedSuccessfully);
                            if ($scope.afterSave) { $scope.afterSave() }
                            $scope.package = null
                        })
                        .catch(function (error) {
                            Notification.notifyError(Sentences.errorAddingTranslation, error.data.messages)
                        })
                        .finally(function () {
                            $scope.pendingRequests--;
                        });
                }
            }]
        public static Factory() {
            return new GsdPackageTranslation();
        }
    }
    app.directive('gsdPackageTranslation', GsdPackageTranslation.Factory)
}