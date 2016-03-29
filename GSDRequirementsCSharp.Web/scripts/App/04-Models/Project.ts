module Models {

    import GSDRequirementsData = Globals.GSDRequirementsData
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;

    export class Project {
        public ProjectContents: Array<Object>
        constructor(data: Object) {
            for (var prop in data) {
                this[prop] = data[prop]
            }
        }
        public canAddTranslation() {            
            return !_.any(this.ProjectContents,
                         (c) => c.locale == GSDRequirements.currentLocale)
        }
        public canEdit() {
            return _.any(this.ProjectContents,
                (c) => c.locale == GSDRequirements.currentLocale)
        }
        public getCommandModel() {
            return {
                'name': this['Name'],
                'description': this['Description'],
                'id' : this['Id']
             }
        }
    }
}
