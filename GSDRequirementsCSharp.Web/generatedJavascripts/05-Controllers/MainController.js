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
            setTimeout(function () {
                $('.hidden-pre-load').fadeIn('slow', function () { });
            }, 110);
        }
        return MainController;
    }());
    app.controller('MainController', ["$scope", MainController]);
})(Controllers || (Controllers = {}));
//# sourceMappingURL=MainController.js.map