module Controllers {
    
    declare var angular: any;
    declare var GSDRequirements: Globals.GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class ModalProjectTranslationController {
        constructor($scope: any, $uibModalInstance : any) {

            $scope.project = {}

            $scope.conclude = function () {
                $uibModalInstance.close($scope.project);
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
        }
    }
    
    app.controller('ModalProjectTranslationController', ["$scope", "$uibModalInstance", ($scope, $uibModalInstance) =>
        new ModalProjectTranslationController($scope, $uibModalInstance)]);
}