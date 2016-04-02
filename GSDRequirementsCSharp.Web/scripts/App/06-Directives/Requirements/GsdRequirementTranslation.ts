module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdRequirementTranslation{
        public scope = { 'requirement': '=requirement', 'afterSave': '=afterSave' };
        public templateUrl = GSDRequirements.baseUrl + 'requirement/translation'
        private defineDisplayDescription = ($scope) => {
            var content = null;
            if (GSDRequirements.currentLocale == 'en-US') {
                content = $scope.requirement.contents[0];
            } else {
                content = _.first($scope.requirement.contents,
                                 (pc) => pc.locale == 'en-US');
                if (!content) {
                    content = $scope.requirement.contents[0]
                }
            }
            $scope.originalDescriptionLocale = content.locale
            $scope.originalDescription = content.description
        }
        public controller = ['$scope', 'RequirementContentResource',
                             ($scope: any, RequirementContentResource: any) => {
            $scope.pendingRequests = 0;
            var self = this
            $scope.$watch('requirement', (newValue, oldValue) => {
                if (newValue) {
                    self.defineDisplayDescription($scope)
                }
            })
            $scope.save = () => {
                $scope.pendingRequests++;
                $scope.data.requirementId = $scope.requirement.id;
                RequirementContentResource.save($scope.data)
                    .$promise
                    .then(function () {
                        Notification.notifySuccess(Sentences.translationAddedSuccessfully);
                        if ($scope.afterSave) { $scope.afterSave() }
                        $scope.requirement = null
                    })
                    .catch(function (error) {
                        Notification.notifyError(Sentences.errorAddingTranslation, error.data.messages)
                    })
                    .finally(function () {
                        $scope.pendingRequests--;
                    });
            }
        }]
        public static Factory() {
            return new GsdRequirementTranslation();
        }
    }
    app.directive('gsdRequirementTranslation', GsdRequirementTranslation.Factory)
}