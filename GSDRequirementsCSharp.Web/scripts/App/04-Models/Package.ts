module Models {

    import GSDRequirementsData = Globals.GSDRequirementsData
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;

    export class Package {
        public description: string        
        public locale : string
        public id: string
        public currentLocale: string
        public contentDictionary: Object
        public identifier: number
        public isOutdated: boolean
        public containsLocale(locale: string) {
            return this.contentDictionary &&
                this.contentDictionary[locale] &&
                this.contentDictionary[locale].description
        }
        public getContentProperty(property: string, locale : string = null) {
            var currentLocale = this.contentDictionary[locale || this.currentLocale]
            if (currentLocale && currentLocale[property])
                return currentLocale[property]
            var enUs = this.contentDictionary["en-US"]
            if (enUs && enUs[property])
                return enUs[property]

            var anyAvailableLocale = _.find(this.contentDictionary, (val, key) => val[property])
            if (anyAvailableLocale && anyAvailableLocale[property])
                return anyAvailableLocale[property]

            return ""
        }
        public contents: Array<PackageContent>
        constructor(data: Object = null) {
            this.contentDictionary = {}

            _.each(GSDRequirements.localesAvailable, (locale: Models.Locale) => {
                var content = new ProjectContent()
                content.locale = locale.name
                this.contentDictionary[locale.name] = content
            })

            if (data) {
                for (var prop in data) {
                    this[prop] = data[prop]
                }
                if (this.contents) {
                    this.defineContent()
                    _.each(this.contents, (c): void => {
                        this.contentDictionary[c.locale] = c
                    })
                }
            }

            var currentContent = _.find(this.contents,
                (pc: PackageContent) => pc.locale == GSDRequirements.currentLocale)

            if (currentContent)
                this.currentLocale = currentContent.locale
            else
                this.currentLocale = GSDRequirements.currentLocale
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
            this.isOutdated = !content.isUpdated
        }
        public canAddTranslation() {
            return _.any(this.contents, (c) => !c.isUpdated) ||
                this.contents.length < GSDRequirements.localesAvailable.length
        }
    }
}
