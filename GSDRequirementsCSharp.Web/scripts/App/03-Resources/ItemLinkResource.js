(function (app) {
    app.service('ItemLinkResource', ['$resource', function ($resource) {
        return $resource('/api/specificationItem/:id/link',
                        {
                            'id': '@id'
                        },
                        {
                            'remove': { method: 'DELETE' }
                        });
    }]);
    
})(angular.module(GSDRequirements.angularModuleName));