var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var ModalPermissionAddController = (function () {
        function ModalPermissionAddController($scope, $uibModalInstance, UserResource, permissionsGrantedPreviously) {
            $scope.profileOptions = _.filter(Globals.enumerateEnum(Models.profile), function (i) { return i.value != Models.profile.projectOwner; });
            $scope.loadingUsers = false;
            $scope.permission = {};
            $scope.permission.profile = Models.profile.editor;
            var usersWithPermissionsEmails = _.map(permissionsGrantedPreviously, function (p) { return p.user.email; });
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
                    return _.filter(r, function (item) { return !_.any(usersWithPermissionsEmails, function (e) { return item.email == e; }); });
                })
                    .catch(function (error) {
                    Notification.notifyError(Sentences.errorSearchingUsers, error.data.messages);
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
    app.controller('ModalPermissionAddController', ["$scope", "$uibModalInstance",
        "UserResource", "permissionsGrantedPreviously", ModalPermissionAddController]);
})(Controllers || (Controllers = {}));
//# sourceMappingURL=ModalPermissionAddController.js.map