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

    class ModalPackageTranslationController {
        constructor($scope: any, $uibModalInstance: any, translationsAlreadyProvided: any, translationToEdit: any) {
            
            if (translationToEdit) {
                $scope.package = translationToEdit
            } else {
                $scope.package = {}
            }
            $scope.languageOptions = _.filter(GSDRequirements.localesAvailable,
                (l) => l.name != GSDRequirements.currentLocale &&
                    (notAlreadyProvided(translationsAlreadyProvided, l) || isInCurrentEdition(translationToEdit, l)));

            $scope.conclude = function () {
                $uibModalInstance.close($scope.package);
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
        }
    }

    app.controller('ModalPackageTranslationController', ["$scope", "$uibModalInstance", "translationsAlreadyProvided",
        "translationToEdit", ModalPackageTranslationController]);
}