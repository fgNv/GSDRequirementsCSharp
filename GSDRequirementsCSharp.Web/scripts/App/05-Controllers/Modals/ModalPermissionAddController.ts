module Controllers {

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: Globals.GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);
    
    class ModalPermissionAddController {
        constructor($scope: any,
            $uibModalInstance: any,
            private UserResource: any) {
            
            $scope.profileOptions = _.filter(Globals.enumerateEnum(Models.profile),
                (i) => i.value != Models.profile.projectOwner)

            $scope.loadingUsers = false;
            $scope.permission = {}
            $scope.permission.profile = Models.profile.editor

            $scope.getUserLabel = (user) => {
                if (!user) return "";
                return `${user.name} (${user.email})`
            }

            $scope.getUsers = (searchTerm) => {
                $scope.loadingUsers = true;
                return UserResource.query({ 'searchTerm': searchTerm })
                                   .$promise
                                   .then((r) => {
                                       return r;
                                   })
                                   .catch((error) => {
                                        Notification.notifyError(Sentences.errorSearchingUsers,
                                           error.messages)
                                   })
                                   .finally((r) => {
                                       $scope.loadingUsers = false
                                   })
            }

            $scope.conclude = () => {
                $uibModalInstance.close($scope.permission);
            };

            $scope.cancel = () => {
                $uibModalInstance.dismiss('cancel');
            };
        }
    }

    app.controller('ModalPermissionAddController', ["$scope", "$uibModalInstance", "UserResource",
        ModalPermissionAddController]);
}