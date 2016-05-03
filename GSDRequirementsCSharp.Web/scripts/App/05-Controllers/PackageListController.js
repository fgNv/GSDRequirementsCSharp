var Controllers;
(function (Controllers) {
    var UserData = NewAccount.UserData;
    var app = angular.module(GSDRequirements.angularModuleName);
    var PackageListController = (function () {
        function PackageListController($scope, PackageResource, $rootScope, $location) {
            var _this = this;
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
            $scope.addPackage = function () {
                $scope.currentPackage = {};
                window.location.href = "#/form";
            };
            $rootScope.$on('$locationChangeStart', function (event, newUrl, oldUrl) {
                var pathValues = $location.path().split('/');
                var step = pathValues[1];
                if (!step) {
                    $scope.currentPackage = null;
                    $scope.packageToTranslate = null;
                }
            });
            window.location.href = "#";
            $scope.setCurrentPackage = function (p) {
                $scope.currentPackage = p;
                window.location.href = "#/form";
            };
            $scope.setPackageToTranslate = function (p) {
                $scope.packageToTranslate = p;
                window.location.href = "#/translate";
            };
            $scope.loadPackages = function () { return _this.LoadPackages(PackageResource, $scope, pageSize); };
            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };
            $scope.inactivatePackage = function (p) {
                _this.InactivatePackage(PackageResource, $scope, p);
            };
            $scope.loadPackages();
            $scope.UserData = new UserData();
        }
        PackageListController.prototype.InactivatePackage = function (packageResource, $scope, packageEntity) {
            if (!confirm(Sentences.areYouCertainYouWishToRemoveThisItem)) {
                return;
            }
            $scope.pendingRequests++;
            packageResource.remove({ id: packageEntity.id })
                .$promise
                .then(function (r) {
                Notification.notifySuccess(Sentences.packageInactivatedSuccessfully);
                $scope.loadPackages();
            })
                .catch(function (error) {
                Notification.notifyError(Sentences.errorInactivatingPackage, error.data.messages);
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
    app.controller('PackageListController', ["$scope", "PackageResource", "$rootScope", "$location", PackageListController]);
})(Controllers || (Controllers = {}));
