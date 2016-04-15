var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdRequirementDetails = (function () {
        function GsdRequirementDetails() {
            this.scope = { 'specificationItem': '=specificationItem' };
            this.templateUrl = GSDRequirements.baseUrl + 'requirement/details';
            this.controller = ['$scope', 'RequirementResource', function ($scope, RequirementResource) {
                }];
        }
        GsdRequirementDetails.Factory = function () {
            return new GsdRequirementDetails();
        };
        return GsdRequirementDetails;
    })();
    app.directive('gsdRequirementDetails', GsdRequirementDetails.Factory);
})(Directives || (Directives = {}));
//# sourceMappingURL=GsdRequirementDetails.js.map