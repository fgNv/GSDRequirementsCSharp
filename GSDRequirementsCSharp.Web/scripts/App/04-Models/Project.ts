module Models {

    import GSDRequirementsData = Globals.GSDRequirementsData
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;

    export class Project {
        public projectContents: Array<Object>
        public id : string
        constructor(data: Object) {
            for (var prop in data) {
                this[prop] = data[prop]
            }
        }
        public canAddTranslation() {            
            return _.any(this.projectContents,
                         (c) => !c.isUpdated)
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
