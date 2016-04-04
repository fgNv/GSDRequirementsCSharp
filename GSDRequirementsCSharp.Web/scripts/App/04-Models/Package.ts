module Models {

    import GSDRequirementsData = Globals.GSDRequirementsData
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;

    export class Package {
        public description: string        
        public locale : string
        public id: string
        public identifier: number
        public contents: Array<PackageContent>
        constructor(data: Object) {
            for (var prop in data) {
                this[prop] = data[prop]
            }
            if (this.contents) {
                this.defineContent()
            }
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
        private fillWithContent(content: PackageContent) {
            this.description = content.description
            this.locale = content.locale
        }
        public canAddTranslation() {
            return _.any(this.contents, (c) => !c.isUpdated) ||
                this.contents.length < GSDRequirements.localesAvailable.length
        }
    }
}
