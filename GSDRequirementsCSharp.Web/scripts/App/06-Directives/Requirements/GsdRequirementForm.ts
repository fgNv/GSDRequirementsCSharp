module directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdRequirementForm {
        public scope = { 'requirement': '=requirement', 'afterSave': '=afterSave' };
        public templateUrl = GSDRequirements.baseUrl + 'requirement/form'
        public controller = ['$scope', 'RequirementResource', ($scope: any, RequirementResource: any) => {
            $scope.pendingRequests = 0;

            $scope.save = () => {
                $scope.pendingRequests++;
                var promise = $scope.requirement.id ?
                    RequirementResource.update($scope.requirement).$promise :
                    RequirementResource.save($scope.requirement).$promise

                var successMessage = $scope.requirement.id ?
                    Sentences.requirementUpdatedSuccessfully :
                    Sentences.requirementSuccessfullyCreated;

                promise.then(function () {
                    Notification.notifySuccess(successMessage);
                    
                    if ($scope.afterSave) { $scope.afterSave() }
                        $scope.requirement = null
                    })
                    .catch(function (error) {
                        Notification.notifyError(Sentences.errorSavingRequirement, error.data.messages)
                    })
                    .finally(function () {
                        $scope.pendingRequests--;
                    });
            }
        }]
        public static Factory() {
            return new GsdRequirementForm();
        }
    }
    app.directive('gsdRequirementForm', GsdRequirementForm.Factory)
}