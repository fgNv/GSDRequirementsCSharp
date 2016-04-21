(function (app) {
    app.service('ItemIssuesResource', ['$resource', function ($resource) {
        return $resource('/api/specificationItem/:id/issues',
                         { 'id': '@id' },
                        {
                            'update': { method: 'PUT' }
                        });
    }]);
})(angular.module(GSDRequirements.angularModuleName));