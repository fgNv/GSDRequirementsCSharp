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

    app.service('RequirementTranslationResource', ['$resource', function ($resource) {
        return $resource('/api/requirement/:id/translation',
                        { 'id': '@id' });
    }]);

})(angular.module(GSDRequirements.angularModuleName));