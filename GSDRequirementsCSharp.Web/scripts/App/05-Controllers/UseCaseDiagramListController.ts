module Controllers {

    import UserData = NewAccount.UserData
    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var sentences: any;
    declare var baseUrl: string;
    declare var _: any;

    interface UseCaseListControllerScope {
        addUseCaseDiagram: () => void
        currentPage: number
        currentUseCaseDiagram: Models.UseCaseDiagram
        getPaginationRange: () => any
        hasEditPermission: boolean
        inactivateUseCaseDiagram: (uc) => void
        inactivateClassDiagrams: (cd) => void
        loadPage: (page: number) => void
        loadUseCaseDiagrams: () => void
        maxPages: number
        pendingRequests: number
        setUseCaseToManageLinks: (uc) => void 
        useCaseDiagramToManageLinks: Models.UseCaseDiagram
        useCaseDiagramToTranslate: Models.UseCaseDiagram
        useCasesDiagrams: Array<Models.UseCaseDiagram>
        setCurrentUseCaseDiagram: (cd) => void
        UserData: UserData
    }

    var app = angular.module(GSDRequirements.angularModuleName);

    class UseCaseDiagramListController {
        constructor($scope: UseCaseListControllerScope, UseCaseDiagramResource, $rootScope, $location,
            SpecificationItemResource) {
            $scope.currentPage = 1
            $scope.maxPages = 1
            $scope.useCasesDiagrams = []

            var pageSize = 10
            $scope.pendingRequests = 0
            $scope.hasEditPermission =
                GSDRequirements.currentProfile == Models.profile.editor ||
                GSDRequirements.currentProfile == Models.profile.projectOwner

            $scope.loadPage = (page) => {
                $scope.currentPage = page
                $scope.loadUseCaseDiagrams()
            }

            $scope.addUseCaseDiagram = () => {
                $scope.currentUseCaseDiagram = new Models.UseCaseDiagram()
                window.location.href = "#/diagram"
            }

            $scope.setUseCaseToManageLinks = (uc) => {
                $scope.useCaseDiagramToManageLinks = uc
                window.location.href = "#/links"
            }

            $scope.inactivateUseCaseDiagram = (useCaseDiagram) => {
                if (!confirm(Sentences.areYouCertainYouWishToRemoveThisItem)) {
                    return;
                }

                $scope.pendingRequests++

                SpecificationItemResource
                    .remove({ id: useCaseDiagram.id })
                    .$promise
                    .then((): void => {
                        Notification.notifySuccess(Sentences.useCaseDiagramRemovedSuccessfully);
                        $scope.loadUseCaseDiagrams()
                    })
                    .catch((err): void => {
                        Notification.notifyError(Sentences.errorRemovingUseCaseDiagram, err.data.messages)
                    })
                    .finally((): void=> {
                        $scope.pendingRequests--
                    })
            }

            $rootScope.$on('$locationChangeStart', (event, newUrl, oldUrl): void => {
                var pathValues = $location.path().split('/')
                var step = pathValues[1];

                if (!step) {
                    $scope.currentUseCaseDiagram = null
                    $scope.useCaseDiagramToTranslate = null
                }

                if (pathValues.length == 2) {
                    $scope.currentUseCaseDiagram = null
                }
            });

            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };

            window.location.href = "#"

            $scope.setCurrentUseCaseDiagram = (cd): void => {
                $scope.pendingRequests++

                UseCaseDiagramResource.get({ 'id': cd.id })
                    .$promise
                    .then((response) => {
                        $scope.currentUseCaseDiagram = new Models.UseCaseDiagram(response)
                        window.location.href = "#/diagram"
                    })
                    .catch((err) => {
                        Notification.notifyError(Sentences.errorLoadingClassDiagrams, err.messages)
                    })
                    .finally(() => {
                        $scope.pendingRequests--
                    });
            }

            $scope.loadUseCaseDiagrams = () => this.LoadUseCaseDiagrams(UseCaseDiagramResource,
                $scope,
                pageSize)

            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };
            
            $scope.loadUseCaseDiagrams()
            $scope.UserData = new UserData()
        }
        private LoadUseCaseDiagrams(useCaseDiagramResource: any, $scope: any, size: number): void {
            $scope.pendingRequests++;
            var request = { page: $scope.currentPage, pageSize: size }
            useCaseDiagramResource.get(request)
                .$promise
                .then((response) => {
                    $scope.useCaseDiagrams = _.map(response.useCaseDiagrams,
                        (p) => new Models.UseCaseDiagram(p))
                    $scope.maxPages = response.maxPages
                })
                .catch((err) => {
                    Notification.notifyError(Sentences.errorLoadingClassDiagrams, err.messages)
                })
                .finally(() => {
                    $scope.pendingRequests--
                });
        }
    }
    app.controller('UseCaseDiagramListController', ["$scope", "UseCaseDiagramResource",
        "$rootScope", "$location", "SpecificationItemResource", UseCaseDiagramListController]);
}