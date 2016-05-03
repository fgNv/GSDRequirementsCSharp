var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    function notAlreadyProvided(translationsAlreadyProvided, locale) {
        return _.every(translationsAlreadyProvided, function (provided) { return locale.name != provided; });
    }
    function isInCurrentEdition(translationToEdit, locale) {
        return translationToEdit && translationToEdit.locale == locale.name;
    }
    var ModalProjectTranslationController = (function () {
        function ModalProjectTranslationController($scope, $uibModalInstance, translationsAlreadyProvided, translationToEdit) {
            if (translationToEdit) {
                $scope.project = translationToEdit;
            }
            else {
                $scope.project = {};
            }
            $scope.languageOptions = _.filter(GSDRequirements.localesAvailable, function (l) { return (notAlreadyProvided(translationsAlreadyProvided, l) ||
                isInCurrentEdition(translationToEdit, l)); });
            $scope.conclude = function () {
                $scope.project.isUpdated = true;
                $uibModalInstance.close($scope.project);
            };
            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
        }
        return ModalProjectTranslationController;
    })();
    app.controller('ModalProjectTranslationController', ["$scope", "$uibModalInstance", "translationsAlreadyProvided",
        "translationToEdit", ModalProjectTranslationController]);
})(Controllers || (Controllers = {}));
//# sourceMappingURL=ModalProjectTranslationController.js.map