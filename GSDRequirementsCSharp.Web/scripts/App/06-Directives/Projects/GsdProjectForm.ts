module directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdProjectForm {
        public scope = { 'project': '=project', 'afterSave': '=afterSave' };
        public templateUrl = GSDRequirements.baseUrl + 'project/form'
        public controller = ['$scope', 'ProjectResource', '$uibModal', ($scope: any, ProjectResource: any, $uibModal: any) => {
            $scope.pendingRequests = 0;

            $scope.translations = []

            function openTranslationModal(translationToEdit) {
                var translationsAlreadProvided = _.map($scope.translations, (t) => t.locale)

                console.log('translationsAlreadProvided')
                console.log(translationsAlreadProvided)

                var modal = $uibModal.open({
                    templateUrl: 'translationContent.html',
                    controller: 'ModalProjectTranslationController',
                    size: 'lg',
                    resolve: {
                        translationsAlreadyProvided: () => translationsAlreadProvided,
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

            $scope.save = () => {
                $scope.pendingRequests++;

                $scope.project.items = [
                    {
                        "name": $scope.project.name,
                        "description": $scope.project.description,
                        "locale": GSDRequirements.currentLocale
                    }
                ]

                _.each($scope.translations, (i): void=> $scope.project.items.push(i))

                var promise = $scope.project.id ?
                    ProjectResource.update($scope.project).$promise :
                    ProjectResource.save($scope.project).$promise

                var successMessage = $scope.project.id ?
                    Sentences.projectUpdatedSuccessfully :
                    Sentences.projectSuccessfullyCreated;

                promise.then(function () {
                    Notification.notifySuccess(successMessage);
                    $scope.$emit(Globals.EventNames.projectListChanged)
                    if ($scope.afterSave) { $scope.afterSave() }
                    $scope.project = null
                })
                    .catch(function (error) {
                        Notification.notifyError(Sentences.errorSavingProject, error.data.messages)
                    })
                    .finally(function () {
                        $scope.pendingRequests--;
                    });
            }
        }]
        public static Factory() {
            return new GsdProjectForm();
        }
    }
    app.directive('gsdProjectForm', GsdProjectForm.Factory)
}