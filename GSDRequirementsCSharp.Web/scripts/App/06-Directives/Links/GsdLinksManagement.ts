﻿module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdLinksManagement {
        public scope = {
            'specificationItem': '=specificationItem'
        };
        private loadLinks($scope, ItemLinkResource, itemId) {
            $scope.pendingRequests++

            ItemLinkResource.query({ id: itemId })
                .$promise
                .then((links): void => {
                    $scope.links = links
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

            CurrentProjectItemResource.query()
                .$promise
                .then((links): void => {
                    $scope.specificationItems = links
                })
                .catch((error): void=> {
                    Notification.notifyError(Sentences.errorLoadingSpecificationItems,
                        error.data.messages)
                })
                .finally((): void => {
                    $scope.pendingRequests--
                })
        }
        public templateUrl = GSDRequirements.baseUrl + 'link/management'
        public controller = ['$scope', 'ItemLinkResource', 'CurrentProjectItemResource',
            ($scope, ItemLinkResource, CurrentProjectItemResource) => {
            $scope.pendingRequests = 0;
            $scope.links = []
            $scope.specificationItems = []

            this.loadSpecificationItems($scope, CurrentProjectItemResource)

            $scope.addNewLink = (): void=> {
                $scope.addingNewLink = true
            }

            $scope.$watch("specificationItem", (newValue, oldValue) => {
                $scope.links = []
                if (!newValue) return;

                this.loadLinks($scope, ItemLinkResource, newValue.id)
            })
        }]
        public static Factory() {
            return new GsdLinksManagement();
        }
    }

    app.directive('gsdLinksManagement', GsdLinksManagement.Factory)
}