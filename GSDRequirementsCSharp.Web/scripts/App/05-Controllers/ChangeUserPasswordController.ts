module Controllers {

    declare var angular: any;
    declare var GSDRequirements: Globals.GSDRequirementsData;

    var app = angular.module(GSDRequirements.angularModuleName);

    class ChangeUserPasswordController {
        constructor($scope: any, UserPasswordResource: any) {
            $scope.pendingRequests = 0

            $scope.save = () => {
                if ($scope.passwordConfirmation != $scope.userData.Password) {
                    Notification.notifyWarning(Sentences.errorSavingUserAccount,
                        [Sentences.passwordAndConfirmationMustMatch]);
                    return;
                }

                $scope.pendingRequests++

                UserPasswordResource.update($scope.userData)
                    .$promise
                    .then((): void=> {
                        Notification.notifySuccess(Sentences.passwordSuccessfullyChanged);
                    })
                    .catch((error): void => {
                        Notification.notifyError(Sentences.errorChangingPassword, error.messages);
                    })
                    .finally((): void=> {
                        $scope.pendingRequests--
                    })
            }
        }
    }

    app.controller('ChangeUserPasswordController', ["$scope", "UserPasswordResource", ChangeUserPasswordController]);
}