var Controllers;
(function (Controllers) {
    var UserData = NewAccount.UserData;
    var app = angular.module(GSDRequirements.angularModuleName);
    var PackageListController = (function () {
        function PackageListController($scope, PackageResource) {
            var _this = this;
            this.$scope = $scope;
            this.PackageResource = PackageResource;
            $scope.currentPage = 1;
            $scope.maxPages = 1;
            $scope.packages = [];
            var pageSize = 10;
            $scope.pendingRequests = 0;
            $scope.hasEditPermission =
                GSDRequirements.currentProfile == Models.profile.editor ||
                    GSDRequirements.currentProfile == Models.profile.projectOwner;
            $scope.loadPage = function (page) {
                $scope.currentPage = page;
                $scope.loadPackages();
            };
            $scope.setCurrentPackage = function (p) { $scope.currentPackage = p; };
            $scope.setPackageToTranslate = function (p) { $scope.packageToTranslate = p; };
            $scope.loadPackages = function () { return _this.LoadPackages(PackageResource, $scope, pageSize); };
            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };
            $scope.inactivatePackage = function (p) {
                _this.InactivatePackage(PackageResource, $scope, p);
            };
            $scope.loadPackages();
            this.$scope.UserData = new UserData();
        }
        PackageListController.prototype.InactivatePackage = function (packageResource, $scope, packageEntity) {
            $scope.pendingRequests++;
            packageResource.remove({ id: packageEntity.id })
                .$promise
                .then(function (r) {
                Notification.notifySuccess(Sentences.packageInactivatedSuccessfully);
                $scope.loadPackages();
            })
                .catch(function (error) {
                Notification.notifyError(Sentences.errorInactivatingPackage, error.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        PackageListController.prototype.LoadPackages = function (packageResource, $scope, size) {
            $scope.pendingRequests++;
            var request = { page: $scope.currentPage, pageSize: size };
            packageResource.get(request)
                .$promise
                .then(function (response) {
                $scope.packages = _.map(response.packages, function (p) { return new Models.Package(p); });
                $scope.maxPages = response.maxPages;
            })
                .catch(function (err) {
                Notification.notifyError(Sentences.errorLoadingPackages, err.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        return PackageListController;
    })();
    app.controller('PackageListController', ["$scope", "PackageResource", PackageListController]);
})(Controllers || (Controllers = {}));
