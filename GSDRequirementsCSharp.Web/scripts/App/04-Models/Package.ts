module Models {

    import GSDRequirementsData = Globals.GSDRequirementsData
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;

    export class Package {
        public name: string
        public locale : string
        constructor(data: Object) {
            for (var prop in data) {
                this[prop] = data[prop]
            }
        }
        public canAddTranslation() {
            return this.locale != GSDRequirements.currentLocale
        }
        public canEdit() {
            return this.locale == GSDRequirements.currentLocale
        }
    }
}
