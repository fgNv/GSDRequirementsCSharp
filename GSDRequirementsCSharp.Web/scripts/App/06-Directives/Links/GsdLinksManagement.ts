module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdLinksManagement {
        private $q: any
        private loadLinks($scope, ItemLinkResource, itemId) {
            $scope.pendingRequests++

            ItemLinkResource.query({ id: itemId })
                .$promise
                .then((links): void => {
                    $scope.links = _.map(links, l => new Models.ItemLink(l))

                    if ($scope.originalSpecificationItems.length == 0)
                        return

                    $scope.specificationItems = _.chain($scope.originalSpecificationItems)
                        .filter(si => si.id != itemId)
                        .map(si => {
                            si.linked = _.any(links, l => l.target.id == si.id)
                            return si;
                        })
                        .value();
                })
                .catch((error): void=> {
                    Notification.notifyError(Sentences.errorLoadingLinks,
                        error.data.messages)
                })
                .finally((): void => {
                    $scope.pendingRequests--
                })
        }
        private loadSpecificationItems($scope, CurrentProjectItemResource) {
            $scope.pendingRequests++
            var deferred = this.$q.defer()

            CurrentProjectItemResource.query()
                .$promise
                .then((items): void => {
                    $scope.originalSpecificationItems = items
                    deferred.resolve(items);
                })
                .catch((error): void=> {
                    Notification.notifyError(Sentences.errorLoadingSpecificationItems,
                        error.data.messages)
                    deferred.reject(error);
                })
                .finally((): void => {
                    $scope.pendingRequests--
                })

            return deferred.promise
        }
        public scope = {
            'specificationItem': '=specificationItem'
        };
        public templateUrl = GSDRequirements.baseUrl + 'link/management'
        public controller = ['$scope', 'ItemLinkResource', 'CurrentProjectItemResource',
            '$q',
            ($scope, ItemLinkResource, CurrentProjectItemResource, $q) => {
                $scope.pendingRequests = 0
                $scope.links = []

                $scope.selected = null

                $scope.selectItem = (item): void => {
                    $scope.selected = item
                }
                
                $scope.artifactTypes = Models.ArtifactType

                $scope.originalSpecificationItems = []
                $scope.specificationItems = []
                this.$q = $q

                this.loadSpecificationItems($scope, CurrentProjectItemResource)

                $scope.addNewLink = (): void=> {
                    $scope.addingNewLink = true
                }

                $scope.removeLink = (link): void => {
                    $scope.pendingRequests++

                    var request = {
                        id: link.origin.id,
                        targetItemId: link.target.id
                    }
                    
                    ItemLinkResource.remove(request)
                        .$promise
                        .then((): void => {
                            Notification.notifySuccess(Sentences.linkRemovedSuccessfully);
                            this.loadLinks($scope, ItemLinkResource, $scope.specificationItem.id)
                        })
                        .catch((error): void => {
                            Notification.notifyError(Sentences.errorRemovingLink,
                                error.data.messages)
                        })
                        .finally((): void=> {
                            $scope.pendingRequests--
                        });
                }

                $scope.saveLink = (): void=> {
                    if (!$scope.selected || !$scope.specificationItem)
                        return;

                    $scope.pendingRequests++

                    var request = {
                        id: $scope.specificationItem.id,
                        targetItemId: $scope.selected.id,
                        isBidirectional: true
                    }

                    ItemLinkResource.save(request)
                        .$promise
                        .then((): void => {
                            Notification.notifySuccess(Sentences.linkSavedSuccessfully);
                            this.loadLinks($scope, ItemLinkResource, $scope.specificationItem.id)
                        })
                        .catch((error): void => {
                            Notification.notifyError(Sentences.errorSavingLink,
                                error.data.messages)
                        })
                        .finally((): void=> {
                            $scope.pendingRequests--
                        });
                }

                $scope.$watch("specificationItem", (newValue, oldValue) => {
                    $scope.selected = null
                    $scope.links = []
                    if (!newValue) return

                    this.loadSpecificationItems($scope, CurrentProjectItemResource)
                        .then((items): void => {
                            this.loadLinks($scope, ItemLinkResource, newValue.id)
                        })
                })
            }]
        public static Factory() {
            return new GsdLinksManagement();
        }
    }

    app.directive('gsdLinksManagement', GsdLinksManagement.Factory)
}