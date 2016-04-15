var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdLinksManagement = (function () {
        function GsdLinksManagement() {
            var _this = this;
            this.scope = {
                'specificationItem': '=specificationItem'
            };
            this.templateUrl = GSDRequirements.baseUrl + 'link/management';
            this.controller = ['$scope', 'ItemLinkResource', 'CurrentProjectItemResource',
                '$q',
                function ($scope, ItemLinkResource, CurrentProjectItemResource, $q) {
                    $scope.pendingRequests = 0;
                    $scope.links = [];
                    $scope.selected = null;
                    $scope.selectItem = function (item) {
                        $scope.selected = item;
                    };
                    var artifactTypes = Globals.enumerateEnum(Models.ArtifactType);
                    console.log('artifactTypes');
                    console.log(artifactTypes);
                    $scope.artifactTypes = {};
                    _.each(artifactTypes, function (t) {
                        $scope.artifactTypes[t.key] = t.key;
                    });
                    $scope.originalSpecificationItems = [];
                    $scope.specificationItems = [];
                    _this.$q = $q;
                    _this.loadSpecificationItems($scope, CurrentProjectItemResource);
                    $scope.addNewLink = function () {
                        $scope.addingNewLink = true;
                    };
                    $scope.removeLink = function (link) {
                        $scope.pendingRequests++;
                        var request = {
                            id: link.origin.id,
                            targetItemId: link.target.id
                        };
                        ItemLinkResource.remove(request)
                            .$promise
                            .then(function () {
                            Notification.notifySuccess(Sentences.linkRemovedSuccessfully);
                            _this.loadLinks($scope, ItemLinkResource, $scope.specificationItem.id);
                        })
                            .catch(function (error) {
                            Notification.notifyError(Sentences.errorRemovingLink, error.data.messages);
                        })
                            .finally(function () {
                            $scope.pendingRequests--;
                        });
                    };
                    $scope.saveLink = function () {
                        if (!$scope.selected || !$scope.specificationItem)
                            return;
                        $scope.pendingRequests++;
                        var request = {
                            id: $scope.specificationItem.id,
                            targetItemId: $scope.selected.id,
                            isBidirectional: true
                        };
                        ItemLinkResource.save(request)
                            .$promise
                            .then(function () {
                            Notification.notifySuccess(Sentences.linkSavedSuccessfully);
                            _this.loadLinks($scope, ItemLinkResource, $scope.specificationItem.id);
                        })
                            .catch(function (error) {
                            Notification.notifyError(Sentences.errorSavingLink, error.data.messages);
                        })
                            .finally(function () {
                            $scope.pendingRequests--;
                        });
                    };
                    $scope.$watch("specificationItem", function (newValue, oldValue) {
                        $scope.selected = null;
                        $scope.links = [];
                        if (!newValue)
                            return;
                        _this.loadSpecificationItems($scope, CurrentProjectItemResource)
                            .then(function (items) {
                            _this.loadLinks($scope, ItemLinkResource, newValue.id);
                        });
                    });
                }];
        }
        GsdLinksManagement.prototype.loadLinks = function ($scope, ItemLinkResource, itemId) {
            $scope.pendingRequests++;
            ItemLinkResource.query({ id: itemId })
                .$promise
                .then(function (links) {
                $scope.links = _.map(links, function (l) { return new Models.ItemLink(l); });
                if ($scope.originalSpecificationItems.length == 0)
                    return;
                $scope.specificationItems = _.chain($scope.originalSpecificationItems)
                    .filter(function (si) { return si.id != itemId; })
                    .map(function (si) {
                    si.linked = _.any(links, function (l) { return l.target.id == si.id; });
                    return si;
                })
                    .value();
            })
                .catch(function (error) {
                Notification.notifyError(Sentences.errorLoadingLinks, error.data.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        GsdLinksManagement.prototype.loadSpecificationItems = function ($scope, CurrentProjectItemResource) {
            $scope.pendingRequests++;
            var deferred = this.$q.defer();
            CurrentProjectItemResource.query()
                .$promise
                .then(function (items) {
                $scope.originalSpecificationItems = items;
                deferred.resolve(items);
            })
                .catch(function (error) {
                Notification.notifyError(Sentences.errorLoadingSpecificationItems, error.data.messages);
                deferred.reject(error);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
            return deferred.promise;
        };
        GsdLinksManagement.Factory = function () {
            return new GsdLinksManagement();
        };
        return GsdLinksManagement;
    })();
    app.directive('gsdLinksManagement', GsdLinksManagement.Factory);
})(Directives || (Directives = {}));
//# sourceMappingURL=GsdLinksManagement.js.map