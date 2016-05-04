var Models;
(function (Models) {
    var ClassMethod = (function () {
        function ClassMethod(data) {
            if (data === void 0) { data = null; }
            if (data) {
                for (var prop in data) {
                    this[prop] = data[prop];
                }
                this.classMethodParameters = _.map(data.parameters, function (cm) { return new Models.ClassMethodParameter(cm); });
            }
            else {
                this.classMethodParameters = [];
            }
        }
        ClassMethod.prototype.addParameter = function () {
            this.classMethodParameters.push(new Models.ClassMethodParameter());
        };
        ClassMethod.prototype.removeProperty = function (parameter) {
            this.classMethodParameters = _.filter(this.classMethodParameters, function (p) { return p != parameter; });
        };
        ClassMethod.prototype.getDescription = function () {
            var visibility = Models.GetVisibilityUmlRepresentation(this.visibility);
            var parameters = _.map(this.classMethodParameters, function (p) { return p.getDescription(); });
            return visibility + " " + this.name + "(" + parameters.join(', ') + ") : " + this.returnType;
        };
        return ClassMethod;
    }());
    Models.ClassMethod = ClassMethod;
})(Models || (Models = {}));
//# sourceMappingURL=ClassMethod.js.map