var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    function notAlreadyProvided(translationsAlreadyProvided, locale) {
        return _.every(translationsAlreadyProvided, function (provided) { return locale.name != provided; });
    }
    function isInCurrentEdition(translationToEdit, locale) {
        return translationToEdit && translationToEdit.locale == locale.name;
    }
    function anyData(originalData) {
        return originalData.condition || originalData.subject || originalData.action;
    }
    var ModalRequirementTranslationController = (function () {
        function ModalRequirementTranslationController($scope, $uibModalInstance, translationsAlreadyProvided, translationToEdit, originalData) {
            if (originalData && anyData(originalData)) {
                $scope.conditionPlaceholder = originalData.condition;
                $scope.subjectPlaceholder = originalData.subject;
                $scope.actionPlaceholder = originalData.action;
            }
            else {
                $scope.conditionPlaceholder = $scope.defaultConditionPlaceholder;
                $scope.subjectPlaceholder = $scope.defaultSubjectPlaceholder;
                $scope.actionPlaceholder = $scope.defaultActionPlaceholder;
            }
            if (translationToEdit) {
                $scope.requirement = translationToEdit;
            }
            else {
                $scope.requirement = {};
            }
            $scope.languageOptions = _.filter(GSDRequirements.localesAvailable, function (l) { return (notAlreadyProvided(translationsAlreadyProvided, l) || isInCurrentEdition(translationToEdit, l)); });
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
        "translationToEdit", "originalData", ModalRequirementTranslationController]);
})(Controllers || (Controllers = {}));
