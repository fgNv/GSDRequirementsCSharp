var directives;
(function (directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdProjectForm = (function () {
        function GsdProjectForm() {
            this.scope = { 'project': '=project', 'afterSave': '=afterSave' };
            this.templateUrl = GSDRequirements.baseUrl + 'project/form';
            this.controller = ['$scope', 'ProjectResource', '$uibModal', function ($scope, ProjectResource, $uibModal) {
                    $scope.pendingRequests = 0;
                    $scope.translations = [];
                    function openTranslationModal(translationToEdit) {
                        var translationsAlreadProvided = _.map($scope.translations, function (t) { return t.locale; });
                        console.log('translationsAlreadProvided');
                        console.log(translationsAlreadProvided);
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
                    $scope.save = function () {
                        $scope.pendingRequests++;
                        $scope.project.items = [
                            {
                                "name": $scope.project.name,
                                "description": $scope.project.description,
                                "locale": GSDRequirements.currentLocale
                            }
                        ];
                        _.each($scope.translations, function (i) { return $scope.project.items.push(i); });
                        var promise = $scope.project.id ?
                            ProjectResource.update($scope.project).$promise :
                            ProjectResource.save($scope.project).$promise;
                        var successMessage = $scope.project.id ?
                            Sentences.projectUpdatedSuccessfully :
                            Sentences.projectSuccessfullyCreated;
                        promise.then(function () {
                            Notification.notifySuccess(successMessage);
                            $scope.$emit(Globals.EventNames.projectListChanged);
                            if ($scope.afterSave) {
                                $scope.afterSave();
                            }
                            $scope.project = null;
                        })
                            .catch(function (error) {
                            Notification.notifyError(Sentences.errorSavingProject, error.data.messages);
                        })
                            .finally(function () {
                            $scope.pendingRequests--;
                        });
                    };
                }];
        }
        GsdProjectForm.Factory = function () {
            return new GsdProjectForm();
        };
        return GsdProjectForm;
    })();
    app.directive('gsdProjectForm', GsdProjectForm.Factory);
})(directives || (directives = {}));
