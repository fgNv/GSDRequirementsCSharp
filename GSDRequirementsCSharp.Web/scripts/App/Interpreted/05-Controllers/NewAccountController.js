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
            this.$scope.UserData = new UserData();
        }
        NewAccountController.prototype.Save = function (userResource, $scope) {
            userResource.save($scope.UserData)
                .$promise
                .then(function (response) {
            })
                .catch(function (error) {
            })
                .finally(function () {
            });
        };
        return NewAccountController;
    })();
    Controllers.NewAccountController = NewAccountController;
    app.controller('NewAccountController', ["$scope", "UserResource", function ($scope, UserResource) {
            return new NewAccountController($scope, UserResource);
        }]);
})(Controllers || (Controllers = {}));
