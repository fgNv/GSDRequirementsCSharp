module Controllers {

    import UserData = NewAccount.UserData
    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var sentences: any;

    var app = angular.module(GSDRequirements.angularModuleName);

    export class NewAccountController {
        constructor(
            private $scope: any,
            private UserResource: any
        ) {
            $scope.save = () => this.Save(UserResource, $scope)
            $scope.pendingRequests = 0
            this.$scope.UserData = new UserData()
        }
        private Save(userResource: any, $scope: any): void {

            if ($scope.passwordConfirmation != $scope.UserData.password) {
                Notification.notifyWarning(Sentences.errorSavingUserAccount,
                    [Sentences.passwordAndConfirmationMustMatch]);
                return;
            }

            $scope.pendingRequests++;
            userResource.save($scope.UserData)
                .$promise
                .then((response) => {
                    Notification.notifySuccess(Sentences.userAccountSuccessfullyCreated);
                })
                .catch((error) => {
                    Notification.notifyError(Sentences.errorSavingUserAccount,
                                             error.data.errors)
                })
                .finally(() => {
                    $scope.pendingRequests--;
                });
        }
    }
    app.controller('NewAccountController', ["$scope", "UserResource", ($scope, UserResource) =>
        new NewAccountController($scope, UserResource)]);
}