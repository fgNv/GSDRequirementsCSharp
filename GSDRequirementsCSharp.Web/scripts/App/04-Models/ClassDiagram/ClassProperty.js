var Models;
(function (Models) {
    var ClassProperty = (function () {
        function ClassProperty() {
        }
        ClassProperty.prototype.getDescription = function () {
            var visibility = this.Visibility == Models.Visibility.Public ? "+" : "-";
            return visibility + " " + this.name + " : " + this.returnType;
        };
        return ClassProperty;
    })();
    Models.ClassProperty = ClassProperty;
})(Models || (Models = {}));
//# sourceMappingURL=ClassProperty.js.map