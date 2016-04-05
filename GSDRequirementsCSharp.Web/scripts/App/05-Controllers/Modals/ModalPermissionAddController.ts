module Controllers {

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: Globals.GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);
    
    class ModalPermissionAddController {
        constructor($scope: any,
            private UserResource: any,
            $uibModalInstance: any) {
            
            $scope.profileOptions = Globals.enumerateEnum(Models.profile)

            $scope.permissions = []

            $scope.conclude = function () {
                $uibModalInstance.close($scope.permissions);
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
        }
    }

    app.controller('ModalPermissionAddController', ["$scope", "$uibModalInstance", "UserResource",
        ModalPermissionAddController]);
}