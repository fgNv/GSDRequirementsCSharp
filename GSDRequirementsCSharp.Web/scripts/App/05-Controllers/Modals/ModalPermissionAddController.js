var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var ModalPermissionAddController = (function () {
        function ModalPermissionAddController($scope, UserResource, $uibModalInstance) {
            this.UserResource = UserResource;
            $scope.profileOptions = Globals.enumerateEnum(Models.profile);
            $scope.permissions = [];
            $scope.conclude = function () {
                $uibModalInstance.close($scope.permissions);
            };
            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
        }
        return ModalPermissionAddController;
    })();
    app.controller('ModalPermissionAddController', ["$scope", "$uibModalInstance", "UserResource",
        ModalPermissionAddController]);
})(Controllers || (Controllers = {}));
