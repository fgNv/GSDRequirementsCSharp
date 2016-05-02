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
        constructor($scope, PackageResource, $rootScope, $location) {
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

            $scope.addPackage = () => {
                $scope.currentPackage = {}
                window.location.href = "#/form"
            }

            $rootScope.$on('$locationChangeStart', (event, newUrl, oldUrl): void => {
                var pathValues = $location.path().split('/')
                var step = pathValues[1];

                if (!step) {
                    $scope.currentPackage = null
                    $scope.packageToTranslate = null
                }
            });

            window.location.href = "#"

            $scope.setCurrentPackage = (p): void => {
                $scope.currentPackage = p
                window.location.href = "#/form"
            }
            $scope.setPackageToTranslate = (p): void => {
                $scope.packageToTranslate = p
                window.location.href = "#/translate"
            }

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
            $scope.UserData = new UserData()
        }
        private InactivatePackage(packageResource: any, $scope: any, packageEntity: Models.Package): void {
            if (!confirm(Sentences.areYouCertainYouWishToRemoveThisItem)) {
                return;
            }

            $scope.pendingRequests++;
            packageResource.remove({ id: packageEntity.id })
                .$promise
                .then(r => {
                    Notification.notifySuccess(Sentences.packageInactivatedSuccessfully)
                    $scope.loadPackages()
                })
                .catch(error => {
                    Notification.notifyError(Sentences.errorInactivatingPackage, error.data.messages)
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
    app.controller('PackageListController', ["$scope", "PackageResource", "$rootScope", "$location", PackageListController]);
}