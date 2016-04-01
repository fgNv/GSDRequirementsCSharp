(function (app) {
    app.service('RequirementResource', ['$resource', function ($resource) {
        return $resource('/api/requirement/:id',
                        { 'id': '@id' },
                        {
                            'update': { method: 'PUT' },
                            'remove': { method: 'DELETE' }
                        });
    }]);

    app.service('RequirementTranslationResource', ['$resource', function ($resource) {
        return $resource('/api/requirementTranslation/:id',
                        { 'id': '@id' });
    }]);

})(angular.module(GSDRequirements.angularModuleName));