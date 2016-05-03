var Models;
(function (Models) {
    var ClassRelationship = (function () {
        function ClassRelationship(data) {
            if (data === void 0) { data = null; }
            if (data) {
                for (var prop in data) {
                    this[prop] = data[prop];
                }
            }
        }
        return ClassRelationship;
    })();
    Models.ClassRelationship = ClassRelationship;
})(Models || (Models = {}));
