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
        artifactsToManageLinks : any
        currentUseCase : any
        currentPage: number
        currentUseCaseDiagram: Models.UseCaseDiagram
        getPaginationRange: () => any
        hasEditPermission: boolean
        inactivateUseCaseDiagram: (uc) => void       
        loadPage: (page: number) => void
        loadUseCaseDiagrams: () => void
        maxPages: number
        modelToEditLinks : any
        pendingRequests: number
        removeUseCaseDiagram(useCaseDiagram) : void
        setUseCaseDiagramToManageLinks: (uc) => void
        setUseCaseDiagramToManageVersions: (uc) => void 
        useCaseDiagramToManageLinks: Models.UseCaseDiagram
        useCaseDiagramToManageVersions: Models.UseCaseDiagram
        useCasesDiagrams: Array<Models.UseCaseDiagram>
        setCurrentUseCaseDiagram: (cd) => void
        UserData: UserData,
        $watch : any
    }

    var app = angular.module(GSDRequirements.angularModuleName);

    class UseCaseDiagramListController {
        constructor($scope: UseCaseListControllerScope, UseCaseDiagramResource, $rootScope, $location,
            SpecificationItemResource, UseCasesByDiagramResource) {
            $scope.currentPage = 1
            $scope.maxPages = 1
            $scope.useCasesDiagrams = []
            $scope.currentUseCase = null
            $scope.useCaseDiagramToManageVersions = null

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
                $scope.currentUseCaseDiagram = new Models.UseCaseDiagram
                $scope.currentUseCase = null
                window.location.href = "#/diagram"
            }

            function loadArtifactsToManageLinks(useCaseDiagram) {
                $scope.pendingRequests++

                UseCasesByDiagramResource
                    .query({ id: useCaseDiagram.id })
                    .$promise
                    .then((items): void => {
                        items = _.map(items, i => new Models.SpecificationItem(i.specificationItem))
                        items.push(useCaseDiagram)
                        $scope.artifactsToManageLinks = items
                        $scope.modelToEditLinks = useCaseDiagram
                    })
                    .catch((err): void => {

                    })
                    .finally(() => {
                        $scope.pendingRequests--
                    })
            }

            $scope.$watch('useCaseDiagramToManageLinks', (newValue, oldValue) => {
                if (newValue)
                    loadArtifactsToManageLinks(newValue)
            })

            $scope.setUseCaseDiagramToManageVersions = (uc) => {
                $scope.useCaseDiagramToManageVersions = uc
            }

            $scope.setUseCaseDiagramToManageLinks = (uc) => {
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
                    $scope.useCaseDiagramToManageLinks = null
                }
            });

            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };

            window.location.href = "#"

            $scope.setCurrentUseCaseDiagram = (cd): void => {
                $scope.pendingRequests++
                $scope.currentUseCase = null
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

            $scope.removeUseCaseDiagram = (ucd) => {
                if (!confirm(Sentences.areYouCertainYouWishToRemoveThisItem)) {
                    return;
                }

                $scope.pendingRequests++

                UseCaseDiagramResource.remove({ id: ucd.id })
                    .$promise
                    .then((): void => {
                        Notification.notifySuccess(Sentences.useCaseDiagramRemovedSuccessfully)
                        $scope.loadUseCaseDiagrams()
                    })
                    .catch((err) => {
                        Notification.notifyError(Sentences.errorRemovingUseCaseDiagram, err.data.messages)
                    })
                    .finally((): void => { $scope.pendingRequests-- })
            }

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
        "$rootScope", "$location", "SpecificationItemResource", "UseCasesByDiagramResource",
        UseCaseDiagramListController]);
}