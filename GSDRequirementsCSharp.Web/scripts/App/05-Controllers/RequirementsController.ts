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
        addRequirement(): void
        setCurrentRequirement(r: Models.Requirement): void
        setRequirementToTranslate(r: Models.Requirement): void
        setRequirementToManageLinks(r: Models.Requirement): void
        setRequirementToShowDetails(r: Models.Requirement): void

        inactivateRequirement(r: Models.Requirement): void
        showList(): boolean

        hasEditPermission: boolean

        remove(r: Models.Requirement): void
    }

    var app = angular.module(GSDRequirements.angularModuleName);

    class RequirementsController {
        constructor($scope: IRequirementsControllerScope, RequirementResource, SpecificationItemResource, $rootScope, $location
        ) {
            $scope.currentPage = 1
            $scope.maxPages = 1
            $scope.requirements = []
            $scope.pendingRequests = 0
            $scope.hasEditPermission =
                GSDRequirements.currentProfile == Models.profile.editor ||
                GSDRequirements.currentProfile == Models.profile.projectOwner
            
            window.location.href = "#"

            $rootScope.$on('$locationChangeStart', (event, newUrl, oldUrl): void => {
                var pathValues = $location.path().split('/')
                var step = pathValues[1];

                if (!step) {
                    $scope.currentRequirement = null
                    $scope.requirementToTranslate = null
                    $scope.requirementToManageLinks = null
                    $scope.requirementToShowDetails = null
                }
            });

            var pageSize = 10
            this.SetScopeMethods($scope, RequirementResource, SpecificationItemResource, pageSize)
            this.LoadRequirements(RequirementResource, $scope, pageSize)
        }
        private SetScopeMethods($scope: IRequirementsControllerScope,
            RequirementResource: any,
            SpecificationItemResource: any,
            pageSize: number) {
            $scope.loadPage = (page) => {
                $scope.currentPage = page
                $scope.loadRequirements()
            }
            
            $scope.addRequirement = () => {
                window.location.href = "#/form"
                $scope.currentRequirement = new Models.Requirement({})
            }

            $scope.setCurrentRequirement = (r): void => {
                $scope.currentRequirement = r
                window.location.href = "#/form"
            }
            $scope.setRequirementToTranslate = (r): void => {
                $scope.requirementToTranslate = r
                window.location.href = "#/translate"
            }
            $scope.setRequirementToManageLinks = (r): void => {
                $scope.requirementToManageLinks = r
                window.location.href = "#/links"
            }
            $scope.setRequirementToShowDetails = (r): void => {
                $scope.requirementToShowDetails = r
                window.location.href = `#/details/${r.id}`
            }

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
            $scope.pendingRequests++

            var request = { page: $scope.currentPage, pageSize: pageSize }
            requirementResource.get(request)
                .$promise
                .then((response) => {
                    $scope.requirements = _.map(response.requirements,
                        (p) => new Models.Requirement(p))
                    $scope.maxPages = response.maxPages
                })
                .catch((err) => {
                    Notification.notifyError(Sentences.errorLoadingRequirements, err.data.messages)
                })
                .finally(() => {
                    $scope.pendingRequests--;
                });
        }
        private InactivateRequirement(specificationItemResource: any,
            $scope: IRequirementsControllerScope,
            requirement: Models.Requirement): void {
            if (!confirm(Sentences.areYouCertainYouWishToRemoveThisItem)) {
                return;
            }

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

    app.controller('RequirementsController', ["$scope", "RequirementResource", "SpecificationItemResource",
        "$rootScope", "$location", RequirementsController]);
}