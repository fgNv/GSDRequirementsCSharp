var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdRequirementDetails = (function () {
        function GsdRequirementDetails() {
            var _this = this;
            this.scope = { 'requirementId': '=requirementId' };
            this.templateUrl = GSDRequirements.baseUrl + 'requirement/details';
            this.controller = ['$scope', 'RequirementResource', function ($scope, RequirementResource) {
                    $scope.requirement = null;
                    $scope.pendingRequests = 0;
                    $scope.availableLanguages = [];
                    $scope.requirementType = Models.RequirementType;
                    $scope.difficulty = Models.Difficulty;
                    if ($scope.requirementId)
                        _this.loadRequirement($scope, RequirementResource, $scope.requirementId);
                    $scope.$watch("requirementId", function (newValue, oldValue) {
                        if (newValue)
                            _this.loadRequirement($scope, RequirementResource, newValue);
                    });
                    $scope.$watch("displayLanguage", function (newValue, oldValue) {
                        if (newValue)
                            $scope.requirement.setLocale(newValue);
                    });
                    $scope.getSentence = function (k) { return Sentences[k]; };
                }];
        }
        GsdRequirementDetails.prototype.loadRequirement = function ($scope, RequirementResource, requirementId) {
            $scope.pendingRequests++;
            $scope.availableLanguages = [];
            RequirementResource.get({ 'id': requirementId })
                .$promise
                .then(function (r) {
                var requirement = new Models.Requirement(r);
                $scope.requirement = requirement;
                $scope.displayLanguage = requirement.locale;
                $scope.availableLanguages =
                    _.chain(requirement.requirementContents)
                        .map(function (r) { return r.locale; })
                        .uniq()
                        .value();
            })
                .catch(function (err) {
                Notification.notifyError(Sentences.errorLoadingRequirement, err.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        GsdRequirementDetails.Factory = function () {
            return new GsdRequirementDetails();
        };
        return GsdRequirementDetails;
    })();
    app.directive('gsdRequirementDetails', GsdRequirementDetails.Factory);
})(Directives || (Directives = {}));
//# sourceMappingURL=GsdRequirementDetails.js.map