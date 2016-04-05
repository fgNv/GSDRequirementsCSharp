var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var ModalPermissionAddController = (function () {
        function ModalPermissionAddController($scope, $uibModalInstance, UserResource) {
            this.UserResource = UserResource;
            $scope.profileOptions = Globals.enumerateEnum(Models.profile);
            $scope.loadingUsers = false;
            $scope.permissions = [];
            $scope.getUserLabel = function (user) {
                if (!user)
                    return "";
                return user.name + " (" + user.email + ")";
            };
            $scope.getUsers = function (searchTerm) {
                $scope.loadingUsers = true;
                return UserResource.query({ 'searchTerm': searchTerm })
                    .$promise
                    .then(function (r) {
                    return r;
                })
                    .catch(function (error) {
                    Notification.notifyError(Sentences.errorSearchingUsers, error.messages);
                })
                    .finally(function (r) {
                    $scope.loadingUsers = false;
                });
            };
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
