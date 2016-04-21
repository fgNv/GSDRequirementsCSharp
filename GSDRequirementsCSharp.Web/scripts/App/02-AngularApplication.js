/// <reference path="01-GSDRequirementsNamespace.js" />

(function (GSDRequirements, angular) {
    "use strict";

    GSDRequirements.angularModuleName = "app";
    GSDRequirements.angularDependencies = ['ngDialog', 'ngResource', 'ngMask', 'ui.bootstrap',
                                           'ui.select', 'ngSanitize'];
    angular.module(GSDRequirements.angularModuleName, GSDRequirements.angularDependencies);

})(window.GSDRequirements, angular);