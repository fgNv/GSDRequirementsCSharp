var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var PermissionController = (function () {
        function PermissionController($scope, PermissionResource, $uibModal) {
            var _this = this;
            this.$scope = $scope;
            this.PermissionResource = PermissionResource;
            this.$uibModal = $uibModal;
            $scope.save = function () { return _this.Save(PermissionResource, $scope); };
            $scope.pendingRequests = 0;
            $scope.permissions = [];
            $scope.profileOptions = _.filter(Globals.enumerateEnum(Models.profile), function (i) { return i.value != Models.profile.projectOwner; });
            $scope.removePermission = function (p) {
                $scope.permissions = _.filter($scope.permissions, function (i) { return i != p; });
            };
            $scope.showAddPermissionsModal = function () {
                var modal = $uibModal.open({
                    templateUrl: 'permissionAddModal.html',
                    controller: 'ModalPermissionAddController',
                    size: 'md'
                });
                modal.result.then(function (data) {
                    $scope.permissions.push(data);
                });
            };
            $scope.loadPermissions = function () {
                _this.LoadPermissions($scope, PermissionResource);
            };
            this.LoadPermissions($scope, PermissionResource);
        }
        PermissionController.prototype.LoadPermissions = function ($scope, permissionResource) {
            $scope.pendingRequests++;
            permissionResource.query()
                .$promise
                .then(function (response) {
                $scope.permissions = _.map(response, function (d) { return new Models.Permission(d); });
            })
                .catch(function (error) {
                Notification.notifyError(Sentences.errorLoadingPermissions, error.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        PermissionController.prototype.Save = function (permissionResource, $scope) {
            $scope.pendingRequests++;
            $scope.permissions = _.map($scope.permissions, function (p) {
                p.userId = p.user.id;
                return p;
            });
            var request = { items: $scope.permissions };
            permissionResource.save(request)
                .$promise
                .then(function (response) {
                Notification.notifySuccess(Sentences.permissionsSuccessfullyGranted);
            })
                .catch(function (error) {
                Notification.notifyError(Sentences.errorGrantingPermissions, error.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        return PermissionController;
    })();
    app.controller('PermissionController', ["$scope", "PermissionResource", "$uibModal",
        PermissionController]);
})(Controllers || (Controllers = {}));
