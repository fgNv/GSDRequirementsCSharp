module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdRequirementDetails {
        public scope = { 'requirementId': '=requirementId' };
        public templateUrl = GSDRequirements.baseUrl + 'requirement/details';
        private loadRequirement($scope, RequirementResource, requirementId) {
            $scope.pendingRequests++
            $scope.availableLanguages = []

            RequirementResource.get({ 'id': requirementId })
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
                .catch((err): void=> {
                    Notification.notifyError(Sentences.errorLoadingRequirement, err.messages)
                })
                .finally((): void => {
                    $scope.pendingRequests--
                })
        }
        public controller = ['$scope', 'RequirementResource', ($scope, RequirementResource) => {
            $scope.requirement = null
            $scope.pendingRequests = 0
            $scope.availableLanguages = []
            $scope.requirementType = Models.RequirementType
            $scope.difficulty = Models.Difficulty

            if ($scope.requirementId)
                this.loadRequirement($scope, RequirementResource, $scope.requirementId)

            $scope.$watch("requirementId", (newValue, oldValue): void=> {
                if (newValue)
                    this.loadRequirement($scope, RequirementResource, newValue)
            });

            $scope.$watch("displayLanguage", (newValue, oldValue): void=> {
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