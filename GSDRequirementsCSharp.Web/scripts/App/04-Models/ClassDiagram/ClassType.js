var Models;
(function (Models) {
    (function (ClassType) {
        ClassType[ClassType["Concrete"] = 500] = "Concrete";
        ClassType[ClassType["Abstract"] = 1000] = "Abstract";
        ClassType[ClassType["Interface"] = 2500] = "Interface";
    })(Models.ClassType || (Models.ClassType = {}));
    var ClassType = Models.ClassType;
})(Models || (Models = {}));
