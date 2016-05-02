var Models;
(function (Models) {
    var ClassProperty = (function () {
        function ClassProperty(data) {
            if (data === void 0) { data = null; }
            if (data) {
                for (var prop in data) {
                    this[prop] = data[prop];
                }
            }
        }
        ClassProperty.prototype.getDescription = function () {
            var visibility = Models.GetVisibilityUmlRepresentation(this.visibility);
            return visibility + " " + this.name + " : " + this.type;
        };
        return ClassProperty;
    })();
    Models.ClassProperty = ClassProperty;
})(Models || (Models = {}));
