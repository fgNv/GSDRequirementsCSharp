module Controllers {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var sentences: any;
    declare var baseUrl: string;
    declare var _: any;

    var app = angular.module(GSDRequirements.angularModuleName);

    class PermissionController {
        constructor(
            private $scope: any,
            private PermissionResource: any,
            private $uibModal: any
        ) {
            $scope.save = () => this.Save(PermissionResource, $scope)
            $scope.pendingRequests = 0
            $scope.permissions = []

            $scope.profileOptions = Globals.enumerateEnum(Models.profile)

            $scope.showAddPermissionsModal = () => {
                var modal = $uibModal.open({
                    templateUrl: 'permissionAddModal.html',
                    controller: 'ModalPermissionAddController',
                    size: 'lg'
                });
                modal.result.then((data): void => {
                    $scope.permissions = _.union($scope.permissions, data)
                });
            }

            this.LoadPermissions($scope, PermissionResource)
        }
        private LoadPermissions($scope: any, permissionResource: any): void {
            $scope.pendingRequests++;

            permissionResource.query()
                .$promise
                .then((response: any): void => {
                    $scope.permissions = response
                })
                .catch((error: any): void => {
                    Notification.notifyError(Sentences.errorLoadingPermissions,
                        error.messages)
                })
                .finally((): void=> {
                    $scope.pendingRequests--
                })
        }
        private Save(permissionResource: any, $scope: any): void {
            $scope.pendingRequests++;

            permissionResource.save($scope.permissions)
                .$promise
                .then((response): void => {
                    Notification.notifySuccess(Sentences.permissionsSuccessfullyGranted);
                })
                .catch((error): void => {
                    Notification.notifyError(Sentences.errorGrantingPermissions,
                        error.messages)
                })
                .finally((): void=> {
                    $scope.pendingRequests--;
                });
        }
    }

    app.controller('PermissionController', ["$scope", "PermissionResource", "$uibModal",
        PermissionController]);
}