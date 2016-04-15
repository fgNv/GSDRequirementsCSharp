module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdRequirementDetails {
        public scope = { 'specificationItem': '=specificationItem' };
        public templateUrl = GSDRequirements.baseUrl + 'requirement/details';
        public controller = ['$scope', 'RequirementResource', ($scope, RequirementResource) => {

        }]
        public static Factory() {
            return new GsdRequirementDetails();
        }
    }

    app.directive('gsdRequirementDetails', GsdRequirementDetails.Factory)
}