module Controllers {

    import UserData = NewAccount.UserData
    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var sentences: any;
    declare var baseUrl: string;

    var app = angular.module(GSDRequirements.angularModuleName);

    class NewAccountController {
        constructor(
            private $scope,
            private UserResource,
            private $cookies
        ) {
            $scope.save = () => this.Save(UserResource, $scope, $cookies)
            $scope.pendingRequests = 0
            this.$scope.UserData = new UserData()
        }
        private Save(userResource, $scope, $cookies): void {

            if ($scope.passwordConfirmation != $scope.UserData.Password) {
                Notification.notifyWarning(Sentences.errorSavingUserAccount,
                    [Sentences.passwordAndConfirmationMustMatch]);
                return;
            }

            $scope.pendingRequests++;
            userResource.save($scope.UserData)
                .$promise
                .then((response) => {
                    Notification.notifySuccess(Sentences.userAccountSuccessfullyCreated)
                    $cookies.remove("projectContext")
                    setTimeout(() => window.location = <any>(baseUrl + "project/setContext"), 2100)
                })
                .catch((error) => {
                    $scope.pendingRequests--;
                    Notification.notifyError(Sentences.errorSavingUserAccount, error.data.messages)
                });
        }
    }
    app.controller('NewAccountController', ["$scope", "UserResource", "$cookies", NewAccountController]);
}