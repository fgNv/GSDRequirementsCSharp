var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var ChangeUserPasswordController = (function () {
        function ChangeUserPasswordController($scope, UserPasswordResource) {
            $scope.pendingRequests = 0;
            $scope.save = function () {
                if ($scope.passwordConfirmation != $scope.userData.Password) {
                    Notification.notifyWarning(Sentences.errorSavingUserAccount, [Sentences.passwordAndConfirmationMustMatch]);
                    return;
                }
                $scope.pendingRequests++;
                UserPasswordResource.update($scope.userData)
                    .$promise
                    .then(function () {
                    Notification.notifySuccess(Sentences.passwordSuccessfullyChanged);
                })
                    .catch(function (error) {
                    Notification.notifyError(Sentences.errorChangingPassword, error.data.messages);
                })
                    .finally(function () {
                    $scope.pendingRequests--;
                });
            };
        }
        return ChangeUserPasswordController;
    })();
    app.controller('ChangeUserPasswordController', ["$scope", "UserPasswordResource", ChangeUserPasswordController]);
})(Controllers || (Controllers = {}));
