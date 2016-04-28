var Models;
(function (Models) {
    var ClassDiagram = (function () {
        function ClassDiagram(data) {
            for (var prop in data) {
                this[prop] = data[prop];
            }
        }
        return ClassDiagram;
    })();
    Models.ClassDiagram = ClassDiagram;
})(Models || (Models = {}));
//# sourceMappingURL=ClassDiagram.js.map