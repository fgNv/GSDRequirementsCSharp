var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    function notAlreadyProvided(translationsAlreadyProvided, locale) {
        return _.every(translationsAlreadyProvided, function (provided) { return locale.name != provided; });
    }
    function isInCurrentEdition(translationToEdit, locale) {
        return translationToEdit && translationToEdit.locale == locale.name;
    }
    var ModalRequirementTranslationController = (function () {
        function ModalRequirementTranslationController($scope, $uibModalInstance, translationsAlreadyProvided, translationToEdit) {
            if (translationToEdit) {
                $scope.requirement = translationToEdit;
            }
            else {
                $scope.requirement = {};
            }
            $scope.languageOptions = _.filter(GSDRequirements.localesAvailable, function (l) { return l.name != GSDRequirements.currentLocale &&
                (notAlreadyProvided(translationsAlreadyProvided, l) || isInCurrentEdition(translationToEdit, l)); });
            $scope.conclude = function () {
                $uibModalInstance.close($scope.requirement);
            };
            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
        }
        return ModalRequirementTranslationController;
    })();
    app.controller('ModalRequirementTranslationController', ["$scope", "$uibModalInstance", "translationsAlreadyProvided",
        "translationToEdit", ModalRequirementTranslationController]);
})(Controllers || (Controllers = {}));