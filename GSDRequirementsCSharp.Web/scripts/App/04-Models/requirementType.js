var Models;
(function (Models) {
    (function (requirementType) {
        requirementType[requirementType["functional"] = 50] = "functional";
        requirementType[requirementType["nonFunction"] = 800] = "nonFunction";
    })(Models.requirementType || (Models.requirementType = {}));
    var requirementType = Models.requirementType;
})(Models || (Models = {}));
