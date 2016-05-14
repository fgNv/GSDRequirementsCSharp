module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdRequirementDetails {
        public scope = {
            'requirementLink': '=requirementLink',
            'version': '=?'
        };
        public templateUrl = GSDRequirements.baseUrl + 'requirement/details';
        private loadRequirement($scope, RequirementResource, requirementId) {
            $scope.pendingRequests++
            $scope.availableLanguages = []

            RequirementResource.get({ 'id': requirementId, version: $scope.version })
                .$promise
                .then((r): void => {
                    var requirement = new Models.Requirement(r)
                    $scope.requirement = requirement
                    $scope.displayLanguage = requirement.locale
                    $scope.availableLanguages =
                        _.chain(requirement.requirementContents)
                            .map((r: Models.Requirement) => r.locale)
                            .uniq()
                            .value();
                })
                .catch((err): void => {
                    Notification.notifyError(Sentences.errorLoadingRequirement, err.data.messages)
                })
                .finally((): void => {
                    $scope.pendingRequests--
                })
        }
        public controller = ['$scope', 'RequirementResource',
            ($scope, RequirementResource) => {
                $scope.requirement = null
                $scope.pendingRequests = 0
                $scope.availableLanguages = []
                $scope.requirementType = Models.RequirementType
                $scope.difficulty = Models.Difficulty

                $scope.$watch("requirementLink", (newValue, oldValue) => {
                    if (newValue && newValue.id)
                        this.loadRequirement($scope, RequirementResource, newValue.id)
                });
                
                $scope.$watch("displayLanguage", (newValue, oldValue) => {
                    if (newValue)
                        $scope.requirement.setLocale(newValue)
                });

                $scope.getSentence = (k) => Sentences[k]
            }]
        public static Factory() {
            return new GsdRequirementDetails();
        }
    }

    app.directive('gsdRequirementDetails', GsdRequirementDetails.Factory)
}