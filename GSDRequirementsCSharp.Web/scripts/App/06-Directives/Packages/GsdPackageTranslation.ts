module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdPackageTranslation {
        public scope = { 'package': '=package', 'afterSave': '=afterSave' };
        public templateUrl = GSDRequirements.baseUrl + 'package/translation'
        private defineAvailableLocaleContents($scope, packageEntity: Models.Package) {
            var packageLocales = _.chain(packageEntity.contents)
                .filter((c: Models.PackageContent) => c.isUpdated)
                .map(c => c.locale)
                .value()

            $scope.availableLocaleContents = _.filter(GSDRequirements.localesAvailable,
                l => _.any(packageLocales, (pl) => pl == l.name))
        }
        private clearScope($scope) {
            $scope.availableLocaleContents = []
            $scope.translations = []
            $scope.displayLocale = null
            $scope.originalDescription = ''
            $scope.originalName = ''
        }
        public controller = ['$scope', 'PackageTranslationResource',
            ($scope: any, PackageTranslationResource: any) => {
                $scope.pendingRequests = 0;

                $scope.availableLocaleContents = []
                $scope.translations = []
                $scope.translationsAlreadyProvided = []
                $scope.displayLocale = null
                $scope.originalDescription = ''
                $scope.originalName = ''
                $scope.package = null;

                var self = this
                $scope.$watch('package', (newValue, oldValue) => {
                    if (!newValue) { return }

                    this.clearScope($scope)
                    self.defineAvailableLocaleContents($scope, newValue)
                    $scope.translationsAlreadyProvided = _.map($scope.availableLocaleContents, (c: Models.Locale) => c.name)
                    $scope.displayLocale = $scope.translationsAlreadyProvided[0]
                })

                $scope.$watch('displayLocale', (newValue, oldValue) => {
                    if (!newValue || !$scope.project) { return; }

                    var content = <Models.PackageContent>_.find($scope.package.contents,
                        (c: Models.PackageContent) => c.locale == newValue)

                    $scope.originalDescription = content.description
                })

                $scope.save = () => {
                    $scope.pendingRequests++;

                    var request = { id: $scope.package.id, items: $scope.translations }

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