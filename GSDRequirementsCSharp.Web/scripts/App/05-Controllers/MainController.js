var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var MainController = (function () {
        function MainController($scope) {
            this.$scope = $scope;
            $scope.pendingRequests = 0;
            $scope.$on(Globals.EventNames.projectListChanged, function () {
                $scope.$broadcast(Globals.EventNames.updateProjectList);
            });
        }
        return MainController;
    })();
    app.controller('MainController', ["$scope", function ($scope) {
            return new MainController($scope);
        }]);
})(Controllers || (Controllers = {}));
//# sourceMappingURL=MainController.js.map