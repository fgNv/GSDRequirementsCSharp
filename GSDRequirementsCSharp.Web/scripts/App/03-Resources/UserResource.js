(function (app) {
    app.service('UserResource', ['$resource', function ($resource) {
        return $resource('/api/user/:id',
                        { 'id': '@id' },
                        {
                            'update': { method: 'PUT' }
                        });
    }]);

    app.service('UserPasswordResource', ['$resource', function ($resource) {
        return $resource('/api/user/:id/password',
                        { 'id': '@id' },
                        {
                            'update': { method: 'PUT' }
                        });
    }]);

})(angular.module(GSDRequirements.angularModuleName));