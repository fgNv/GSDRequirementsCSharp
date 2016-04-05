var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var ModalPermissionAddController = (function () {
        function ModalPermissionAddController($scope, $uibModalInstance, UserResource) {
            this.UserResource = UserResource;
            $scope.profileOptions = _.filter(Globals.enumerateEnum(Models.profile), function (i) { return i.value != Models.profile.projectOwner; });
            $scope.loadingUsers = false;
            $scope.permission = {};
            $scope.permission.profile = Models.profile.editor;
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
                $uibModalInstance.close($scope.permission);
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
