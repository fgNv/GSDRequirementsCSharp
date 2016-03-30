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

            $scope.loadPackages()
            $scope.pendingRequests = 1
            this.$scope.UserData = new UserData()
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
                .catch((error) => {
                    Notification.notifyError(Sentences.errorLoadingPackages,
                        error.data.errors)
                })
                .finally(() => {
                    $scope.pendingRequests--;
                });
        }
    }
    app.controller('PackageListController', ["$scope", "PackageResource", ($scope, PackageResource) =>
        new PackageListController($scope, PackageResource)]);
}