﻿module Directives {

    declare var angular: any;
    declare var GSDRequirements: Globals.GSDRequirementsData;
    declare var _: any;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdProjectTranslationList {
        public scope = { 'translations': '=translations', 'translationsAlreadyProvided': '=translationsAlreadyProvided' };
        public templateUrl = GSDRequirements.baseUrl + 'project/translationList'
        public controller = ["$scope", "$uibModal", ($scope: any, $uibModal: any) => {

            $scope.translations = []
            $scope.translationsAlreadyProvided = []

            function openTranslationModal(translationToEdit) {
                var translationsAlreadyProvided = _.map($scope.translations, (t) => t.locale)
                translationsAlreadyProvided = _.union(translationsAlreadyProvided, $scope.translationsAlreadyProvided)

                var modal = $uibModal.open({
                    templateUrl: 'translationContent.html',
                    controller: 'ModalProjectTranslationController',
                    size: 'lg',
                    resolve: {
                        translationsAlreadyProvided: () => translationsAlreadyProvided,
                        translationToEdit: () => translationToEdit
                    }
                });
                return modal;
            }

            $scope.addTranslation = () => {
                var modal = openTranslationModal(null)
                modal.result.then((data): void => $scope.translations.push(data));
            }

            $scope.removeTranslation = (translationToRemove) => {
                $scope.translations = _.filter($scope.translation, (t) => t != translationToRemove)
            }

            $scope.editTranslation = (translation) => {
                var translationClone = {}
                for (var prop in translation) {
                    translationClone[prop] = translation[prop]
                }
                var modal = openTranslationModal(translationClone)

                modal.result.then((data): void => {
                    $scope.removeTranslation(translation)
                    $scope.translations.push(translationClone)
                });
            }
        }]
        public static Factory() {
            return new GsdProjectTranslationList();
        }
    }

    app.directive('gsdProjectTranslationList', GsdProjectTranslationList.Factory)
}