var Models;
(function (Models) {
    var ClassDiagramContent = (function () {
        function ClassDiagramContent(data) {
            for (var prop in data) {
                this[prop] = data[prop];
            }
        }
        return ClassDiagramContent;
    }());
    Models.ClassDiagramContent = ClassDiagramContent;
})(Models || (Models = {}));
//# sourceMappingURL=ClassDiagramContent.js.map