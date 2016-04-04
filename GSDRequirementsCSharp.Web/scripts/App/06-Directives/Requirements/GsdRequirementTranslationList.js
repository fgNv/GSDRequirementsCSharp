var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdRequirementTranslationList = (function () {
        function GsdRequirementTranslationList() {
            this.scope = { 'translations': '=translations', 'translationsAlreadyProvided': '=translationsAlreadyProvided' };
            this.templateUrl = GSDRequirements.baseUrl + 'requirement/translationList';
            this.controller = ["$scope", "$uibModal", function ($scope, $uibModal) {
                    $scope.translations = [];
                    $scope.translationsAlreadyProvided = [];
                    function openTranslationModal(translationToEdit) {
                        var translationsAlreadyProvided = _.map($scope.translations, function (t) { return t.locale; });
                        translationsAlreadyProvided = _.union(translationsAlreadyProvided, $scope.translationsAlreadyProvided);
                        var modal = $uibModal.open({
                            templateUrl: 'translationContent.html',
                            controller: 'ModalRequirementTranslationController',
                            size: 'lg',
                            resolve: {
                                translationsAlreadyProvided: function () { return translationsAlreadyProvided; },
                                translationToEdit: function () { return translationToEdit; }
                            }
                        });
                        return modal;
                    }
                    $scope.addTranslation = function () {
                        var modal = openTranslationModal(null);
                        modal.result.then(function (data) { return $scope.translations.push(data); });
                    };
                    $scope.removeTranslation = function (translationToRemove) {
                        $scope.translations = _.filter($scope.translation, function (t) { return t != translationToRemove; });
                    };
                    $scope.editTranslation = function (translation) {
                        var translationClone = {};
                        for (var prop in translation) {
                            translationClone[prop] = translation[prop];
                        }
                        var modal = openTranslationModal(translationClone);
                        modal.result.then(function (data) {
                            $scope.removeTranslation(translation);
                            $scope.translations.push(translationClone);
                        });
                    };
                }];
        }
        GsdRequirementTranslationList.Factory = function () {
            return new GsdRequirementTranslationList();
        };
        return GsdRequirementTranslationList;
    })();
    app.directive('gsdRequirementTranslationList', GsdRequirementTranslationList.Factory);
})(Directives || (Directives = {}));
//# sourceMappingURL=GsdRequirementTranslationList.js.map