var Models;
(function (Models) {
    var ItemLink = (function () {
        function ItemLink(data) {
            this.origin = new Models.SpecificationItem(data['origin']);
            this.target = new Models.SpecificationItem(data['target']);
            this.id = data['id'];
        }
        ItemLink.prototype.getDescription = function () {
            return this.origin.label + "(" + this.origin.type + ") -> " + this.target.label + "(" + this.target.type + ")";
        };
        return ItemLink;
    })();
    Models.ItemLink = ItemLink;
})(Models || (Models = {}));
//# sourceMappingURL=ItemLink.js.map