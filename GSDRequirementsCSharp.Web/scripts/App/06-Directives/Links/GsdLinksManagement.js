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
            this.controller = ['$scope', 'ItemLinkResource', function ($scope, ItemLinkResource) {
                    $scope.pendingRequests = 0;
                    $scope.links = [];
                    $scope.$watch("specificationItem", function (newValue, oldValue) {
                        $scope.links = [];
                        if (!newValue)
                            return;
                        _this.loadLinks($scope, ItemLinkResource, newValue.id);
                    });
                }];
        }
        GsdLinksManagement.prototype.loadLinks = function ($scope, ItemLinkResource, itemId) {
            $scope.pendingRequests++;
            ItemLinkResource.query({ id: itemId })
                .$promise
                .then(function (links) {
                $scope.links = links;
            })
                .catch(function (error) {
                Notification.notifyError(Sentences.errorLoadingLinks, error.data.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        GsdLinksManagement.Factory = function () {
            return new GsdLinksManagement();
        };
        return GsdLinksManagement;
    })();
    app.directive('gsdLinksManagement', GsdLinksManagement.Factory);
})(Directives || (Directives = {}));
//# sourceMappingURL=GsdLinksManagement.js.map