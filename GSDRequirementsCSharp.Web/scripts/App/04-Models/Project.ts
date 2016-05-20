module Models {

    import GSDRequirementsData = Globals.GSDRequirementsData
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;

    export class Project {
        public projectContents: Array<ProjectContent>
        public id: string
        public contentDictionary: Object
        public currentContentLocale: string
        public isOutdated: boolean
        public containsLocale(locale: string) {
            return this.contentDictionary &&
                this.contentDictionary[locale] &&
                this.contentDictionary[locale].name &&
                this.contentDictionary[locale].description
        }
        public getContentProperty(property: string) {
            var currentLocale = this.contentDictionary[this.currentContentLocale]
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
                _.each(this.projectContents, (c): void => {
                    this.contentDictionary[c.locale] = c
                })
            }

            var contentCurrentLocale = _.find(this.projectContents,
                (pc: ProjectContent) => pc.locale == GSDRequirements.currentLocale)
            this.isOutdated = contentCurrentLocale && !contentCurrentLocale.isUpdated
            
            if (contentCurrentLocale)
                this.currentContentLocale = contentCurrentLocale.locale
            else
                this.currentContentLocale = GSDRequirements.currentLocale
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
        public populateContents() {
            this.projectContents = _.chain(this.contentDictionary)
                .filter((c: Models.ProjectContent) => c.name)
                .map((c, k) => c)
                .value()
        }

    }
}
