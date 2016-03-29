(function (app) {
    app.service('ProjectResource', ['$resource', function ($resource) {
        return $resource('/api/project/');
    }]);
})(angular.module(GSDRequirements.angularModuleName));