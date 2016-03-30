(function (app) {
    app.service('PackageResource', ['$resource', function ($resource) {
        return $resource('/api/package/:id',
                        { 'id': '@id' },
                        { 'update': { method: 'PUT' } });
    }]);

    app.service('PackageTranslationResource', ['$resource', function ($resource) {
        return $resource('/api/packageTranslation/:id',
                        { 'id': '@id' });
    }]);

})(angular.module(GSDRequirements.angularModuleName));