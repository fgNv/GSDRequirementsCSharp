var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    function notAlreadyProvided(translationsAlreadyProvided, locale) {
        return _.every(translationsAlreadyProvided, function (provided) { return locale.name != provided; });
    }
    function isInCurrentEdition(translationToEdit, locale) {
        return translationToEdit && translationToEdit.locale == locale.name;
    }
    var ModalPackageTranslationController = (function () {
        function ModalPackageTranslationController($scope, $uibModalInstance, translationsAlreadyProvided, translationToEdit) {
            if (translationToEdit) {
                $scope.package = translationToEdit;
            }
            else {
                $scope.package = {};
            }
            $scope.languageOptions = _.filter(GSDRequirements.localesAvailable, function (l) { return l.name != GSDRequirements.currentLocale &&
                (notAlreadyProvided(translationsAlreadyProvided, l) || isInCurrentEdition(translationToEdit, l)); });
            $scope.conclude = function () {
                $uibModalInstance.close($scope.package);
            };
            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
        }
        return ModalPackageTranslationController;
    })();
    app.controller('ModalPackageTranslationController', ["$scope", "$uibModalInstance", "translationsAlreadyProvided",
        "translationToEdit", ModalPackageTranslationController]);
})(Controllers || (Controllers = {}));
//# sourceMappingURL=ModalPackageTranslationController.js.map