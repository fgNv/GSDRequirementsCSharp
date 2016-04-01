module Controllers {

    import UserData = NewAccount.UserData
    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var sentences: any;
    declare var baseUrl: string;
    declare var _: any;

    interface IRequirementsControllerScope extends Globals.IListScope {        
        packagesOptions: Array<Models.Package>
        requirements: Array<Models.Requirement>
        loadRequirements() : void
    }

    var app = angular.module(GSDRequirements.angularModuleName);

    class RequirementsController {
        constructor(
            private $scope: IRequirementsControllerScope,
            private RequirementResource: any,
            private PackageResource: any
        ) {
            $scope.currentPage = 1
            $scope.maxPages = 1
            $scope.requirements = []
            $scope.packagesOptions = []
            $scope.pendingRequests = 0

            var pageSize = 10
            this.SetScopeMethods($scope, RequirementResource, pageSize)
            this.LoadRequirements(RequirementResource, $scope, pageSize)
            this.LoadPackagesOptions(PackageResource, $scope)
        }
        private SetScopeMethods($scope: any, RequirementResource: any, pageSize: number) {
            $scope.loadPage = (page) => {
                $scope.currentPage = page
                $scope.loadPackages()
            }

            $scope.setCurrentRequirement = (r): void => { $scope.currentRequirement = r }
            $scope.setRequirementToTranslate = (r): void => { $scope.requirementToTranslate = r }

            $scope.showList = () => {
                return !$scope.currentRequirement &&
                    !$scope.requirementToTranslate &&
                    !$scope.requirementToShowDetails &&
                    !$scope.requirementToAddIssues &&
                    !$scope.requirementToManageLinks;
            }

            $scope.loadRequirements = () => this.LoadRequirements(RequirementResource,
                $scope,
                pageSize)

            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };

            $scope.inactivateRequirement = (r): void => {
                this.InactivateRequirement(RequirementResource, $scope, r)
            }
        }
        private LoadRequirements(requirementResource: any,
                                 $scope: IRequirementsControllerScope,
                                 pageSize: number): void {
            $scope.pendingRequests++;
            var request = { page: $scope.currentPage, pageSize: pageSize }
            requirementResource.get(request)
                .$promise
                .then((response) => {
                    $scope.requirements = _.map(response.requirements,
                        (p) => new Models.Requirement(p))
                    $scope.maxPages = response.maxPages
                })
                .catch((err) => {
                    Notification.notifyError(Sentences.errorLoadingRequirements, err.messages)
                })
                .finally(() => {
                    $scope.pendingRequests--;
                });
        }
        private InactivateRequirement(requirementResource: any,
                                      $scope: IRequirementsControllerScope,
                                      requirement: Models.Requirement): void {
            $scope.pendingRequests++;
            requirementResource.remove({ id: requirement.id })
                .$promise
                .then(r => {
                    Notification.notifySuccess(Sentences.requirementInactivatedSuccessfully)
                    $scope.loadRequirements()
                })
                .catch(error => {
                    Notification.notifyError(Sentences.errorInactivatingRequirement,
                        error.messages)
                })
                .finally(() => {
                    $scope.pendingRequests--;
                });
        }
        private LoadPackagesOptions(packageResource: any,
                                    $scope: IRequirementsControllerScope): void {
            packageResource.query()
                .$promise
                .then((response) => {
                    $scope.packagesOptions = response
                })
                .catch((err) => {
                    Notification.notifyError(Sentences.errorLoadingPackages, err.messages)
                })
                .finally((): void => {
                    $scope.pendingRequests--
                })
        }
    }

    app.controller('RequirementsController', ["$scope", "RequirementResource", "PackageResource",
        ($scope, RequirementResource, PackageResource) =>
            new RequirementsController($scope, RequirementResource, PackageResource)]);
}