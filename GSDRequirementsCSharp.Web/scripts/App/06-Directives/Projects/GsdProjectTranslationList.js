var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdProjectTranslationList = (function () {
        function GsdProjectTranslationList() {
            this.scope = { 'translations': '=translations' };
            this.templateUrl = GSDRequirements.baseUrl + 'project/translationList';
            this.controller = ["$scope", "$uibModal", function ($scope, $uibModal) {
                    $scope.translations = [];
                    function openTranslationModal(translationToEdit) {
                        var translationsAlreadProvided = _.map($scope.translations, function (t) { return t.locale; });
                        console.log('$uibModal');
                        console.log($uibModal);
                        var modal = $uibModal.open({
                            templateUrl: 'translationContent.html',
                            controller: 'ModalProjectTranslationController',
                            size: 'lg',
                            resolve: {
                                translationsAlreadyProvided: function () { return translationsAlreadProvided; },
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
        GsdProjectTranslationList.Factory = function () {
            return new GsdProjectTranslationList();
        };
        return GsdProjectTranslationList;
    })();
    app.directive('gsdProjectTranslationList', GsdProjectTranslationList.Factory);
})(Directives || (Directives = {}));
