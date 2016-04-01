(function (app) {
    app.service('ProjectResource', ['$resource', function ($resource) {
        return $resource('/api/project/:id',
                        {
                            'id': '@id'
                        },
                        {
                            'update': { method: 'PUT' },
                            'remove': { method: 'DELETE' }
                        });
    }]);

    app.service('CurrentUserProjectResource', ['$resource', function ($resource) {
        return $resource('/api/currentUser/projects');
    }]);
})(angular.module(GSDRequirements.angularModuleName));