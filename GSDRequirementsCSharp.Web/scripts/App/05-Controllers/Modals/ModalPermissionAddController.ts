module Controllers {

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: Globals.GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class ModalPermissionAddController {
        constructor($scope, $uibModalInstance, UserResource, permissionsGrantedPreviously) {

            $scope.profileOptions = _.filter(Globals.enumerateEnum(Models.profile),
                (i) => i.value != Models.profile.projectOwner)

            $scope.loadingUsers = false;
            $scope.permission = {}
            $scope.permission.profile = Models.profile.editor

            var usersWithPermissionsEmails = _.map(permissionsGrantedPreviously,
                (p) => p.user.email)

            $scope.getUserLabel = (user) => {
                if (!user) return "";
                return `${user.name} (${user.email})`
            }

            $scope.getUsers = (searchTerm) => {
                $scope.loadingUsers = true;
                return UserResource.query({ 'searchTerm': searchTerm })
                    .$promise
                    .then((r) => {
                        return _.filter(r,
                            (item) => !_.any(usersWithPermissionsEmails,
                                e => item.email == e)
                        );
                    })
                    .catch((error) => {
                        Notification.notifyError(Sentences.errorSearchingUsers, error.data.messages)
                    })
                    .finally((r) => {
                        $scope.loadingUsers = false
                    })
            }

            $scope.conclude = () => {
                if (!$scope.permission || !$scope.permission.user || !$scope.permission.user.id) {
                    Notification.notifyError(Sentences.errorAddingPermission, [Sentences.youMustClickInTheUserToSelectItBeforeAddingThePermission])
                    return;
                }
                $uibModalInstance.close($scope.permission);
            };

            $scope.cancel = () => {
                $uibModalInstance.dismiss('cancel');
            };
        }
    }

    app.controller('ModalPermissionAddController', ["$scope", "$uibModalInstance",
        "UserResource", "permissionsGrantedPreviously", ModalPermissionAddController]);
}