var Models;
(function (Models) {
    var UseCaseDiagram = (function () {
        function UseCaseDiagram(data) {
            if (data === void 0) { data = null; }
            if (data) {
                for (var prop in data) {
                    this[prop] = data[prop];
                }
            }
        }
        return UseCaseDiagram;
    }());
    Models.UseCaseDiagram = UseCaseDiagram;
})(Models || (Models = {}));
//# sourceMappingURL=UseCaseDiagram.js.map