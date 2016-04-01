module Models {

    import GSDRequirementsData = Globals.GSDRequirementsData
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;

    export class Requirement {
        public condition: string
        public subject: string
        public action: string
        public locale: string
        public id: string
        public contents: Array<Models.RequirementContent>
        constructor(data: Object) {
            for (var prop in data) {
                this[prop] = data[prop]
            }
            this.defineContent()
        }
        private defineContent() {
            var currentLocale = _.find(this.contents,
                (c) => c.locale == GSDRequirements.currentLocale)
            if (currentLocale) {
                this.fillWithContent(currentLocale)
                return;
            }
            var enUsLocale = _.find(this.contents,
                (c) => c.locale == "en-US")
            if (enUsLocale) {
                this.fillWithContent(enUsLocale)
                return;
            }
            this.fillWithContent(this.contents[0])
        }
        private fillWithContent(content: RequirementContent) {
            this.condition = content.condition
            this.subject = content.subject
            this.action = content.action
            this.locale = content.locale
        }
        public canAddTranslation() {
            return !_.any(this.contents,
                (c) => c.locale == GSDRequirements.currentLocale)
        }
        public canEdit() {
            return _.any(this.contents,
                (c) => c.locale == GSDRequirements.currentLocale)
        }
    }
}
