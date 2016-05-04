var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdProjectTranslationList = (function () {
        function GsdProjectTranslationList() {
            this.scope = { 'translations': '=translations', 'translationsAlreadyProvided': '=translationsAlreadyProvided' };
            this.templateUrl = GSDRequirements.baseUrl + 'project/translationList';
            this.controller = ["$scope", "$uibModal", function ($scope, $uibModal) {
                    $scope.translations = [];
                    if (!$scope.translationsAlreadyProvided) {
                        $scope.translationsAlreadyProvided = [];
                    }
                    function openTranslationModal(translationToEdit) {
                        var translationsAlreadyProvided = _.chain($scope.translations)
                            .filter(function (t) { return t.isUpdated == true; })
                            .map(function (t) { return t.locale; })
                            .value();
                        translationsAlreadyProvided = _.union(translationsAlreadyProvided, $scope.translationsAlreadyProvided);
                        var modal = $uibModal.open({
                            templateUrl: 'translationContent.html',
                            controller: 'ModalProjectTranslationController',
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
                        $scope.translations = _.filter($scope.translations, function (t) { return t != translationToRemove; });
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
        GsdProjectTranslationList.Factory = function () {
            return new GsdProjectTranslationList();
        };
        return GsdProjectTranslationList;
    }());
    app.directive('gsdProjectTranslationList', GsdProjectTranslationList.Factory);
})(Directives || (Directives = {}));
//# sourceMappingURL=GsdProjectTranslationList.js.map