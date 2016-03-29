module Models {

    import GSDRequirementsData = Globals.GSDRequirementsData
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;

    export class Project {
        public projectContents: Array<Object>
        constructor(data: Object) {
            for (var prop in data) {
                this[prop] = data[prop]
            }
        }
        public canAddTranslation() {            
            return !_.any(this.projectContents,
                         (c) => c.locale == GSDRequirements.currentLocale)
        }
        public canEdit() {
            return _.any(this.projectContents,
                (c) => c.locale == GSDRequirements.currentLocale)
        }
        public getCommandModel() {
            var projectContent = _.find(this.projectContents,
                                        (p) => p.locale == GSDRequirements.currentLocale)

            var result = {
                'name': this['name'],
                'description': projectContent.description,
                'id': this['id']
            }
            
            return result
        }
    }
}
