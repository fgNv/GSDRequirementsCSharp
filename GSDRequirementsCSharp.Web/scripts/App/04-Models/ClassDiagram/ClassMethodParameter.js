var Models;
(function (Models) {
    var ClassMethodParameter = (function () {
        function ClassMethodParameter() {
        }
        ClassMethodParameter.prototype.getDescription = function () {
            return this.name + " : " + this.type;
        };
        return ClassMethodParameter;
    })();
    Models.ClassMethodParameter = ClassMethodParameter;
})(Models || (Models = {}));
//# sourceMappingURL=ClassMethodParameter.js.map