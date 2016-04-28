var Controllers;
(function (Controllers) {
    var UserData = NewAccount.UserData;
    var app = angular.module(GSDRequirements.angularModuleName);
    var NewAccountController = (function () {
        function NewAccountController($scope, UserResource) {
            var _this = this;
            this.$scope = $scope;
            this.UserResource = UserResource;
            $scope.save = function () { return _this.Save(UserResource, $scope); };
            $scope.pendingRequests = 0;
            this.$scope.UserData = new UserData();
        }
        NewAccountController.prototype.Save = function (userResource, $scope) {
            if ($scope.passwordConfirmation != $scope.UserData.Password) {
                Notification.notifyWarning(Sentences.errorSavingUserAccount, [Sentences.passwordAndConfirmationMustMatch]);
                return;
            }
            $scope.pendingRequests++;
            userResource.save($scope.UserData)
                .$promise
                .then(function (response) {
                Notification.notifySuccess(Sentences.userAccountSuccessfullyCreated);
                setTimeout(function () { return window.location = (baseUrl + "home/login"); }, 2100);
            })
                .catch(function (error) {
                $scope.pendingRequests--;
                Notification.notifyError(Sentences.errorSavingUserAccount, error.data.errors);
            });
        };
        return NewAccountController;
    })();
    app.controller('NewAccountController', ["$scope", "UserResource", NewAccountController]);
})(Controllers || (Controllers = {}));
