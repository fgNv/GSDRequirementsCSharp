(function (app) {
    app.service('ProjectItemResource', ['$resource', function ($resource) {
        return $resource('/api/project/:id/specificationItem',
                        {
                            'id': '@id'
                        });
    }]);

    app.service('CurrentProjectItemResource', ['$resource', function ($resource) {
        return $resource('/api/currentProject/specificationItem');
    }]);

})(angular.module(GSDRequirements.angularModuleName));