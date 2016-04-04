module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdRequirementForm {
        public scope = { 'requirement': '=requirement', 'afterSave': '=afterSave' };
        public templateUrl = GSDRequirements.baseUrl + 'requirement/form'
        private LoadPackagesOptions(packageResource: any,
            $scope: any): void {
            packageResource.query()
                .$promise
                .then((response) => {
                    $scope.packagesOptions = _.map(response, (r) => new Models.Package(r))
                })
                .catch((err) => {
                    Notification.notifyError(Sentences.errorLoadingPackages, err.messages)
                })
                .finally((): void => {
                    $scope.pendingRequests--
                })
        }
        public controller = ['$scope', 'RequirementResource', 'PackageResource',
            ($scope: any, RequirementResource: any, PackageResource: any) => {
                $scope.pendingRequests = 0
                $scope.translations = []

                this.LoadPackagesOptions(PackageResource, $scope)

                $scope.difficultyOptions = Globals.enumerateEnum(Models.difficulty)
                $scope.requirementTypeOptions = Globals.enumerateEnum(Models.requirementType)
                
                $scope.$watch("project", (newValue, oldValue) => {
                    if (!newValue) return

                    $scope.translations = []
                })

                $scope.save = () => {
                    $scope.pendingRequests++;
                    
                    $scope.requirement.items = [
                        {
                            "action": $scope.requirement.action,
                            "subject": $scope.requirement.subject,
                            "condition": $scope.requirement.condition,
                            "locale": GSDRequirements.currentLocale
                        }
                    ]

                    _.each($scope.translations, (i): void=> $scope.requirement.items.push(i))

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