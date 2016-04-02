var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var ModalProjectTranslationController = (function () {
        function ModalProjectTranslationController($scope, $uibModalInstance) {
            $scope.project = {};
            $scope.conclude = function () {
                $uibModalInstance.close($scope.project);
            };
            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
        }
        return ModalProjectTranslationController;
    })();
    app.controller('ModalProjectTranslationController', ["$scope", "$uibModalInstance", function ($scope, $uibModalInstance) {
            return new ModalProjectTranslationController($scope, $uibModalInstance);
        }]);
})(Controllers || (Controllers = {}));
