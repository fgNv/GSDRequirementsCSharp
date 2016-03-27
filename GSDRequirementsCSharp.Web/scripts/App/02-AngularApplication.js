/// <reference path="gsdrequirementsnamespace.js" />

(function (GSDRequirements, angular) {
    "use strict";

    GSDRequirements.angularModuleName = "app";
    GSDRequirements.angularDependencies = ['ngDialog', 'ngResource'];
    angular.module(GSDRequirements.angularModuleName, GSDRequirements.angularDependencies);

})(window.GSDRequirements, angular);