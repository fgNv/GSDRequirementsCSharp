module Controllers {
    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var baseUrl: string;
    declare var _: any;

    interface IRequirementsControllerScope extends Globals.IListScope {
        requirements: Array<Models.Requirement>
        loadRequirements(): void

        currentRequirement: Models.Requirement
        requirementToTranslate: Models.Requirement
        requirementToShowDetails: Models.Requirement
        requirementToAddIssues: Models.Requirement
        requirementToManageLinks: Models.Requirement

        loadPage(page: number): void
        setCurrentRequirement(r: Models.Requirement): void
        setRequirementToTranslate(r: Models.Requirement): void
        inactivateRequirement(r: Models.Requirement): void
        showList(): boolean

        remove(r: Models.Requirement): void
    }

    var app = angular.module(GSDRequirements.angularModuleName);

    class RequirementsController {
        constructor(
            private $scope: IRequirementsControllerScope,
            private RequirementResource: any,
            private SpecificationItemResource: any
        ) {
            $scope.currentPage = 1
            $scope.maxPages = 1
            $scope.requirements = []
            $scope.pendingRequests = 0

            var pageSize = 10
            this.SetScopeMethods($scope, RequirementResource, SpecificationItemResource, pageSize)
            this.LoadRequirements(RequirementResource, $scope, pageSize)
        }
        private SetScopeMethods($scope: IRequirementsControllerScope,
            RequirementResource: any,
            SpecificationItemResource : any,
            pageSize: number) {
            $scope.loadPage = (page) => {
                $scope.currentPage = page
                $scope.loadRequirements()
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
                this.InactivateRequirement(SpecificationItemResource, $scope, r)
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
        private InactivateRequirement(specificationItemResource: any,
            $scope: IRequirementsControllerScope,
            requirement: Models.Requirement): void {
            $scope.pendingRequests++;
            specificationItemResource.remove({ id: requirement.id })
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
    }

    app.controller('RequirementsController', ["$scope", "RequirementResource", "SpecificationItemResource", RequirementsController]);
}