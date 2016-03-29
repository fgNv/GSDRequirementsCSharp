(function (app) {
    app.service('ProjectResource', ['$resource', function ($resource) {
        return $resource('/api/project/',{}, { 'update': { method: 'PUT' } });
    }]);
})(angular.module(GSDRequirements.angularModuleName));