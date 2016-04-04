module Controllers {

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: Globals.GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    function notAlreadyProvided(translationsAlreadyProvided: any, locale: any) {
        return _.every(translationsAlreadyProvided, (provided) => locale.name != provided);
    }

    function isInCurrentEdition(translationToEdit: any, locale) {
        return translationToEdit && translationToEdit.locale == locale.name;
    }

    class ModalProjectTranslationController {
        constructor($scope: any, $uibModalInstance: any, translationsAlreadyProvided: any, translationToEdit: any) {
            
            if (translationToEdit) {
                $scope.project = translationToEdit
            } else {
                $scope.project = {}
            }
            $scope.languageOptions = _.filter(GSDRequirements.localesAvailable,
                (l) => (notAlreadyProvided(translationsAlreadyProvided, l) ||
                        isInCurrentEdition(translationToEdit, l)));

            $scope.conclude = function () {
                $scope.project.isUpdated = true
                $uibModalInstance.close($scope.project);
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
        }
    }

    app.controller('ModalProjectTranslationController', ["$scope", "$uibModalInstance", "translationsAlreadyProvided",
        "translationToEdit", ModalProjectTranslationController]);
}