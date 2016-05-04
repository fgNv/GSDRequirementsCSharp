var Models;
(function (Models) {
    (function (Visibility) {
        Visibility[Visibility["Public"] = 500] = "Public";
        Visibility[Visibility["Private"] = 1501] = "Private";
        Visibility[Visibility["Protected"] = 2057] = "Protected";
    })(Models.Visibility || (Models.Visibility = {}));
    var Visibility = Models.Visibility;
    function GetVisibilityUmlRepresentation(visibility) {
        switch (visibility) {
            case Visibility.Private:
                return "-";
            case Visibility.Public:
                return "+";
            case Visibility.Protected:
                return "#";
        }
        return "";
    }
    Models.GetVisibilityUmlRepresentation = GetVisibilityUmlRepresentation;
})(Models || (Models = {}));
//# sourceMappingURL=Visibility.js.map