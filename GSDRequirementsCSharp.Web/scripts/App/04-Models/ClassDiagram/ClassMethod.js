var Models;
(function (Models) {
    var ClassMethod = (function () {
        function ClassMethod() {
        }
        ClassMethod.prototype.getDescription = function () {
            var visibility = this.Visibility == Models.Visibility.Public ? "+" : "-";
            var parameters = _.map(this.parameters, function (p) { return p.getDescription(); });
            return visibility + " " + this.name + "(" + parameters.join(', ') + ") : " + this.returnType;
        };
        return ClassMethod;
    })();
    Models.ClassMethod = ClassMethod;
})(Models || (Models = {}));
//# sourceMappingURL=ClassMethod.js.map