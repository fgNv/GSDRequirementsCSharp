(function (app) {
    app.service('PackageResource', ['$resource', function ($resource) {
        return $resource('/api/package/:id/:page/:pageSize',
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

    app.service('PackageTranslationResource', ['$resource', function ($resource) {
        return $resource('/api/packageTranslation/:id',
                        { 'id': '@id' });
    }]);

})(angular.module(GSDRequirements.angularModuleName));