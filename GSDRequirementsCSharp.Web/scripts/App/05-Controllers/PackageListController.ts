module Controllers {

    import UserData = NewAccount.UserData
    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var sentences: any;
    declare var baseUrl: string;
    declare var _: any;

    var app = angular.module(GSDRequirements.angularModuleName);

    class PackageListController {
        constructor(
            private $scope: any,
            private PackageResource: any
        ) {
            $scope.currentPage = 1
            $scope.maxPages = 1
            $scope.packages = []
            var pageSize = 10
            $scope.pendingRequests = 0
            $scope.hasEditPermission =
                GSDRequirements.currentProfile == Models.profile.editor ||
                GSDRequirements.currentProfile == Models.profile.projectOwner

            $scope.loadPage = (page) => {
                $scope.currentPage = page
                $scope.loadPackages()
            }

            $scope.setCurrentPackage = (p): void => { $scope.currentPackage = p }
            $scope.setPackageToTranslate = (p): void => { $scope.packageToTranslate = p }

            $scope.loadPackages = () => this.LoadPackages(PackageResource,
                $scope,
                pageSize)

            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };

            $scope.inactivatePackage = (p): void => {
                this.InactivatePackage(PackageResource, $scope, p)
            }

            $scope.loadPackages()
            this.$scope.UserData = new UserData()
        }
        private InactivatePackage(packageResource: any, $scope: any, packageEntity: Models.Package): void {
            $scope.pendingRequests++;
            packageResource.remove({ id: packageEntity.id })
                .$promise
                .then(r => {
                    Notification.notifySuccess(Sentences.packageInactivatedSuccessfully)
                    $scope.loadPackages()
                })
                .catch(error => {
                    Notification.notifyError(Sentences.errorInactivatingPackage,
                                             error.messages)
                })
                .finally(() => {
                    $scope.pendingRequests--;
                });
        }
        private LoadPackages(packageResource: any, $scope: any, size: number): void {
            $scope.pendingRequests++;
            var request = { page: $scope.currentPage, pageSize: size }
            packageResource.get(request)
                .$promise
                .then((response) => {
                    $scope.packages = _.map(response.packages,
                        (p) => new Models.Package(p))
                    $scope.maxPages = response.maxPages
                })
                .catch((err) => {
                    Notification.notifyError(Sentences.errorLoadingPackages, err.messages)
                })
                .finally(() => {
                    $scope.pendingRequests--;
                });
        }
    }
    app.controller('PackageListController', ["$scope", "PackageResource", PackageListController]);
}