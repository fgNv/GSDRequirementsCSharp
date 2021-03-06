﻿module Controllers {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var sentences: any;
    declare var baseUrl: string;
    declare var _: any;

    var app = angular.module(GSDRequirements.angularModuleName);

    class PermissionController {
        constructor($scope, PermissionResource, $uibModal) {
            $scope.save = () => this.Save(PermissionResource, $scope)
            $scope.pendingRequests = 0
            $scope.permissions = []

            $scope.profileOptions = _.filter(Globals.enumerateEnum(Models.profile),
                (i) => i.value != Models.profile.projectOwner)

            $scope.removePermission = (p): void=> {
                $scope.permissions = _.filter($scope.permissions, (i) => i != p)
            }

            $scope.showAddPermissionsModal = () => {
                var modal = $uibModal.open({
                    templateUrl: 'permissionAddModal.html',
                    controller: 'ModalPermissionAddController',
                    size: 'md',
                    resolve: {
                        'permissionsGrantedPreviously': () => $scope.permissions
                    }
                });
                modal.result.then((data): void => {
                    $scope.permissions.push(data)
                });
            }

            $scope.loadPermissions = (): void=> {
                this.LoadPermissions($scope, PermissionResource)
            }

            this.LoadPermissions($scope, PermissionResource)
        }
        private LoadPermissions($scope: any, permissionResource: any): void {
            $scope.pendingRequests++;

            permissionResource.query()
                .$promise
                .then((response: any): void => {
                    $scope.permissions = _.map(response, (d) => new Models.Permission(d))
                })
                .catch((error: any): void => {
                    Notification.notifyError(Sentences.errorLoadingPermissions, error.data.messages)
                })
                .finally((): void=> {
                    $scope.pendingRequests--
                })
        }
        private Save(permissionResource: any, $scope: any): void {
            $scope.pendingRequests++;

            $scope.permissions = _.map($scope.permissions, (p) => {
                p.userId = p.user.id
                return p
            })

            var request = { items: $scope.permissions }

            permissionResource.save(request)
                .$promise
                .then((response): void => {
                    Notification.notifySuccess(Sentences.permissionsSuccessfullyGranted);
                })
                .catch((error): void => {
                    Notification.notifyError(Sentences.errorGrantingPermissions, error.data.messages)
                })
                .finally((): void=> {
                    $scope.pendingRequests--;
                });
        }
    }

    app.controller('PermissionController', ["$scope", "PermissionResource", "$uibModal",
        PermissionController]);
}