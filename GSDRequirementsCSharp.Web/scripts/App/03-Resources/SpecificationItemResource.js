(function (app) {
    app.service('SpecificationItemResource', ['$resource', function ($resource) {
        return $resource('/api/specificationItem/:id',
                        {
                            'id': '@id'
                        },
                        {
                            'remove': { method: 'DELETE' }
                        });
    }]);

})(angular.module(GSDRequirements.angularModuleName));