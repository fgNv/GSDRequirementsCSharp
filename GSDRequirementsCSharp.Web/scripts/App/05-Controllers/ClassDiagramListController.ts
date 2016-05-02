module Controllers {

    import UserData = NewAccount.UserData
    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var sentences: any;
    declare var baseUrl: string;
    declare var _: any;

    var app = angular.module(GSDRequirements.angularModuleName);

    class ClassDiagramListController {
        constructor($scope, ClassDiagramResource, $rootScope, $location,
            SpecificationItemResource) {
            $scope.currentPage = 1
            $scope.maxPages = 1
            $scope.classDiagrams = []
            $scope.currentClass = null
            $scope.editingRelations = false

            var pageSize = 10
            $scope.pendingRequests = 0
            $scope.hasEditPermission =
                GSDRequirements.currentProfile == Models.profile.editor ||
                GSDRequirements.currentProfile == Models.profile.projectOwner

            $scope.loadPage = (page) => {
                $scope.currentPage = page
                $scope.loadClassDiagrams()
            }

            $scope.addClassDiagram = () => {
                $scope.currentClassDiagram = new Models.ClassDiagram()
                window.location.href = "#/diagram"
            }

            $scope.inactivateClassDiagram = (classDiagram) => {
                if (!confirm(Sentences.areYouCertainYouWishToRemoveThisItem)) {
                    return;
                }

                $scope.pendingRequests++

                SpecificationItemResource
                    .remove({ id: classDiagram.id })
                    .$promise
                    .then((): void => {
                        Notification.notifySuccess(Sentences.classDiagramRemovedSuccessfully);
                        $scope.loadClassDiagrams()
                    })
                    .catch((err) : void => {
                        Notification.notifyError(Sentences.errorRemovingClassDiagram, err.data.messages)
                    })
                    .finally((): void=> {
                        $scope.pendingRequests--
                    })
            }

            $rootScope.$on('$locationChangeStart', (event, newUrl, oldUrl): void => {
                var pathValues = $location.path().split('/')
                var step = pathValues[1];

                if (!step) {
                    $scope.currentClassDiagram = null
                    $scope.classDiagramToTranslate = null
                }

                if (pathValues.length == 2) {
                    $scope.currentClass = null
                    $scope.editingRelations = false
                }
            });

            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };

            window.location.href = "#"

            $scope.setCurrentClassDiagram = (cd): void => {
                $scope.pendingRequests++

                ClassDiagramResource.get({ 'id': cd.id })
                    .$promise
                    .then((response) => {
                        $scope.currentClassDiagram = new Models.ClassDiagram(response)
                        window.location.href = "#/diagram"
                    })
                    .catch((err) => {
                        Notification.notifyError(Sentences.errorLoadingClassDiagrams, err.messages)
                    })
                    .finally(() => {
                        $scope.pendingRequests--
                    });
            }

            $scope.loadClassDiagrams = () => this.LoadClassDiagrams(ClassDiagramResource,
                $scope,
                pageSize)

            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };

            $scope.inactivateClassDiagrams = (p): void => {
                this.InactivateClassDiagram(ClassDiagramResource, $scope, p)
            }

            $scope.loadClassDiagrams()
            $scope.UserData = new UserData()
        }
        private InactivateClassDiagram(classDiagramResource: any, $scope: any,
            classDiagram: Models.ClassDiagram): void {
            if (!confirm(Sentences.areYouCertainYouWishToRemoveThisItem)) {
                return;
            }

            $scope.pendingRequests++;
            classDiagramResource.remove({ id: classDiagram.id })
                .$promise
                .then(r => {
                    Notification.notifySuccess(Sentences.classDiagramInactivatedSuccessfully)
                    $scope.loadClassDiagrams()
                })
                .catch(error => {
                    Notification.notifyError(Sentences.errorInactivatingClassDiagram, error.data.messages)
                })
                .finally(() => {
                    $scope.pendingRequests--;
                });
        }
        private LoadClassDiagrams(classDiagramResource: any, $scope: any, size: number): void {
            $scope.pendingRequests++;
            var request = { page: $scope.currentPage, pageSize: size }
            classDiagramResource.get(request)
                .$promise
                .then((response) => {
                    $scope.classDiagrams = _.map(response.classDiagrams,
                        (p) => new Models.ClassDiagram(p))
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
    app.controller('ClassDiagramListController', ["$scope", "ClassDiagramResource",
        "$rootScope", "$location", "SpecificationItemResource", ClassDiagramListController]);
}