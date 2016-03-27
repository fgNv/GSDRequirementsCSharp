var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var NewAccountController = (function () {
        function NewAccountController($scope, UserResource) {
            this.$scope = $scope;
            this.UserResource = UserResource;
            // todo
        }
        NewAccountController.prototype.Save = function () {
            this.UserResource
                .save(this.UserData)
                .$promise
                .then(function (response) {
            })
                .catch(function (error) {
            })
                .finally(function () {
            });
        };
        NewAccountController.$inject = ["$scope", "UserResource"];
        return NewAccountController;
    })();
    Controllers.NewAccountController = NewAccountController;
    app.controller('NewAccountController', NewAccountController);
})(Controllers || (Controllers = {}));
