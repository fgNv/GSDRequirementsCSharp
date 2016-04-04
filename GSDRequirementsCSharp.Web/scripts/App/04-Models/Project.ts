module Models {

    import GSDRequirementsData = Globals.GSDRequirementsData
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;

    export class Project {
        public projectContents: Array<ProjectContent>
        public id: string
        public isOutdated: boolean
        constructor(data: Object) {
            for (var prop in data) {
                this[prop] = data[prop]
            }
            var contentCurrentLocale = _.find(this.projectContents,
                (pc: ProjectContent) => pc.locale == GSDRequirements.currentLocale)
            this.isOutdated = contentCurrentLocale && !contentCurrentLocale.isUpdated
        }
        public canAddTranslation() {
            return _.any(this.projectContents, (c) => !c.isUpdated) ||
                this.projectContents.length < GSDRequirements.localesAvailable.length
        }
        public getCommandModel() {
            var projectContent = <ProjectContent>_.find(this.projectContents,
                (p) => p.locale == GSDRequirements.currentLocale)

            var result = { 'id': this['id'] }

            if (projectContent != null) {
                result['name'] = projectContent.name;
                result['description'] = projectContent.description;
            } else {
                result['name'] = this['name'];
                result['description'] = this['description'];
            }

            return result
        }
    }
}
