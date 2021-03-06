﻿(function (app) {
    app.service('VersionResource', ['$resource', function ($resource) {
        return $resource('/api/:artifact/:id/versions',
                         {
                             'id': '@id',
                             'artifact': '@artifact',
                         });
    }]);

})(angular.module(GSDRequirements.angularModuleName));