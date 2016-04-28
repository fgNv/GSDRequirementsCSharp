module Models {
    export enum Visibility {
        Public = 500,
        Private = 1501,
        Protected = 2057
    }

    export function GetVisibilityUmlRepresentation(visibility: Visibility) {
        switch (visibility) {
            case Visibility.Private:
                return "-"
            case Visibility.Public:
                return "+"
            case Visibility.Protected:
                return "#"
        }
        return "";
    }
}