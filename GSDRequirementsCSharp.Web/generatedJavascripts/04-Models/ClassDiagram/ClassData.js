var Models;
(function (Models) {
    var ClassData = (function () {
        function ClassData(data) {
            if (data === void 0) { data = null; }
            if (data) {
                for (var prop in data) {
                    this[prop] = data[prop];
                }
                this.classMethods = _.map(data.classMethods, function (cm) { return new Models.ClassMethod(cm); });
                this.classProperties = _.map(data.classProperties, function (cp) { return new Models.ClassProperty(cp); });
            }
            else {
                this.classMethods = [];
                this.classProperties = [];
            }
        }
        ClassData.prototype.addProperty = function () {
            this.classProperties.push(new Models.ClassProperty());
        };
        ClassData.prototype.addMethod = function () {
            this.classMethods.push(new Models.ClassMethod());
        };
        ClassData.prototype.removeProperty = function (property) {
            this.classProperties = _.filter(this.classProperties, function (p) { return p != property; });
        };
        ClassData.prototype.removeMethod = function (method) {
            this.classMethods = _.filter(this.classMethods, function (m) { return m != method; });
        };
        return ClassData;
    }());
    Models.ClassData = ClassData;
})(Models || (Models = {}));
//# sourceMappingURL=ClassData.js.map