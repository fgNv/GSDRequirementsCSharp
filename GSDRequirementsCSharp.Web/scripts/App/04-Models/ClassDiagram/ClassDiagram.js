var Models;
(function (Models) {
    var ClassDiagram = (function () {
        function ClassDiagram(data) {
            if (data === void 0) { data = null; }
            if (data) {
                for (var prop in data) {
                    this[prop] = data[prop];
                }
                this.contents = _.map(data['contents'], function (c) { return new Models.ClassDiagramContent(c); });
                this.classes = _.map(data['classes'], function (c) { return new Models.ClassData(c); });
                this.relations = _.map(data['relations'], function (c) { return new Models.ClassRelationship(c); });
                this.defineContent();
            }
            else {
                this.classes = [];
                this.contents = [];
                this.relations = [];
            }
        }
        ClassDiagram.prototype.defineContent = function () {
            var currentLocale = _.find(this.contents, function (c) { return c.locale == GSDRequirements.currentLocale; });
            if (currentLocale) {
                this.fillWithContent(currentLocale);
                return;
            }
            var enUsLocale = _.find(this.contents, function (c) { return c.locale == "en-US"; });
            if (enUsLocale) {
                this.fillWithContent(enUsLocale);
                return;
            }
            this.fillWithContent(this.contents[0]);
        };
        ClassDiagram.prototype.fillWithContent = function (content) {
            this.name = content.name;
            this.locale = content.locale;
        };
        ClassDiagram.prototype.getLabel = function () {
            return "CD" + this.identifier;
        };
        return ClassDiagram;
    })();
    Models.ClassDiagram = ClassDiagram;
})(Models || (Models = {}));
//# sourceMappingURL=ClassDiagram.js.map