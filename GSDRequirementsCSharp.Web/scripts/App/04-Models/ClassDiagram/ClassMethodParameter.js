var Models;
(function (Models) {
    var ClassMethodParameter = (function () {
        function ClassMethodParameter(data) {
            if (data === void 0) { data = null; }
            if (data) {
                for (var prop in data) {
                    this[prop] = data[prop];
                }
            }
        }
        ClassMethodParameter.prototype.getDescription = function () {
            return this.name + " : " + this.type;
        };
        return ClassMethodParameter;
    })();
    Models.ClassMethodParameter = ClassMethodParameter;
})(Models || (Models = {}));
//# sourceMappingURL=ClassMethodParameter.js.map