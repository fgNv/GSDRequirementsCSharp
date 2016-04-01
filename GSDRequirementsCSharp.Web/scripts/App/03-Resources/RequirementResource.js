(function (app) {
    app.service('RequirementResource', ['$resource', function ($resource) {
        return $resource('/api/requirement/:id/:page/:pageSize',
                        {
                            'id': '@id',
                            'page': '@page',
                            'pageSize': '@pageSize'
                        },
                        {
                            'update': { method: 'PUT' },
                            'remove': { method: 'DELETE' }
                        });
    }]);

    app.service('RequirementContentResource', ['$resource', function ($resource) {
        return $resource('/api/requirement/:id/content',
                        { 'id': '@id' });
    }]);

})(angular.module(GSDRequirements.angularModuleName));