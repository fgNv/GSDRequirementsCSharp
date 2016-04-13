(function (app) {
    app.service('IssueConclusionResource', ['$resource', function ($resource) {
        return $resource('/api/issue/:id/conclude',
                         { 'id': '@id' },
                        {
                            'update': { method: 'PUT' }
                        });
    }]);
})(angular.module(GSDRequirements.angularModuleName));