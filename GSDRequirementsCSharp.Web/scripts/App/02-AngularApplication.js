/// <reference path="gsdrequirementsnamespace.js" />

(function (GSDRequirements, angular) {
    "use strict";

    GSDRequirements.angularModuleName = "app";
    GSDRequirements.angularDependencies = ['ngDialog'];
    angular.module(GSDRequirements.angularModuleName, GSDRequirements.angularDependencies);

})(window.GSDRequirements, angular);