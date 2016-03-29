(function (angular, angularModuleName) {

    angular.module(angularModuleName).controller("MainController", ['$scope', 'ngDialog', function ($scope, ngDialog) {
        /// <param name="$scope" type="Object" />

        $scope.modal = {};
        $scope.modal.open = function (id) {
            ngDialog.open({ template: id });
        };

    }]);

})(angular, window.GSDRequirements.angularModuleName);