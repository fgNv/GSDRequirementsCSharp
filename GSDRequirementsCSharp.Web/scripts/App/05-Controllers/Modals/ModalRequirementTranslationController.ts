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

    function anyData(originalData) {
        return originalData.condition || originalData.subject || originalData.action
    }

    class ModalRequirementTranslationController {
        constructor($scope: any,
            $uibModalInstance: any,
            translationsAlreadyProvided: any,
            translationToEdit: any,
            originalData: any) {

            if (originalData && anyData(originalData)) {
                $scope.conditionPlaceholder = originalData.condition
                $scope.subjectPlaceholder = originalData.subject
                $scope.actionPlaceholder = originalData.action
            } else {
                $scope.conditionPlaceholder = $scope.defaultConditionPlaceholder
                $scope.subjectPlaceholder = $scope.defaultSubjectPlaceholder
                $scope.actionPlaceholder = $scope.defaultActionPlaceholder
            }

            if (translationToEdit) {
                $scope.requirement = translationToEdit
            } else {
                $scope.requirement = {}
            }
            $scope.languageOptions = _.filter(GSDRequirements.localesAvailable,
                (l) => (notAlreadyProvided(translationsAlreadyProvided, l) || isInCurrentEdition(translationToEdit, l)));

            $scope.conclude = function () {
                $uibModalInstance.close($scope.requirement);
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
        }
    }

    app.controller('ModalRequirementTranslationController', ["$scope", "$uibModalInstance", "translationsAlreadyProvided",
        "translationToEdit", "originalData", ModalRequirementTranslationController]);
}