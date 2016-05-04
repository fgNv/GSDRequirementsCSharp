var Models;
(function (Models) {
    (function (RelationType) {
        RelationType[RelationType["Association"] = 10] = "Association";
        RelationType[RelationType["Inheritance"] = 300] = "Inheritance";
        RelationType[RelationType["Composition"] = 500] = "Composition";
        RelationType[RelationType["Aggregation"] = 700] = "Aggregation";
        RelationType[RelationType["Realization"] = 900] = "Realization";
    })(Models.RelationType || (Models.RelationType = {}));
    var RelationType = Models.RelationType;
})(Models || (Models = {}));
//# sourceMappingURL=RelationType.js.map