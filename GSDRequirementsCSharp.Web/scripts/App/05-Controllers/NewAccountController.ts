module Controllers {

    import UserData = NewAccount.UserData
    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var sentences: any;
    declare var baseUrl: string;

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

            if ($scope.passwordConfirmation != $scope.UserData.Password) {
                Notification.notifyWarning(Sentences.errorSavingUserAccount,
                    [Sentences.passwordAndConfirmationMustMatch]);
                return;
            }

            $scope.pendingRequests++;
            userResource.save($scope.UserData)
                .$promise
                .then((response) => {
                    Notification.notifySuccess(Sentences.userAccountSuccessfullyCreated);
                    setTimeout(() => window.location = <any>(baseUrl + "home/login"), 2100);
                })
                .catch((error) => {
                    $scope.pendingRequests--;
                    Notification.notifyError(Sentences.errorSavingUserAccount,
                                             error.data.errors)
                });
        }
    }
    app.controller('NewAccountController', ["$scope", "UserResource", ($scope, UserResource) =>
        new NewAccountController($scope, UserResource)]);
}