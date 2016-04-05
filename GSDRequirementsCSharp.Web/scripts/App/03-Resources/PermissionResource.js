(function (app) {
    app.service('PermissionResource', ['$resource', function ($resource) {
        return $resource('/api/permission/');
    }]);
})(angular.module(GSDRequirements.angularModuleName));