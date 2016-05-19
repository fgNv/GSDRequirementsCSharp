module Models {

    import GSDRequirementsData = Globals.GSDRequirementsData
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;

    export class Requirement {
        public condition: string
        public subject: string
        public action: string
        public locale: string
        public identifier: number
        public contentDictionary: Object
        public type: RequirementType
        public requirementType: RequirementType
        public difficulty: Difficulty
        public prefix: string
        public currentLocale: string
        public packageId: string
        public package: Models.Package
        public id: string
        public description : string
        public requirementContents: Array<Models.RequirementContent>
        public issues: Array<Object>

        public containsLocale(locale: string) {
            return this.contentDictionary && this.contentDictionary[locale] &&
                (this.contentDictionary[locale].subject ||
                this.contentDictionary[locale].condition ||
                this.contentDictionary[locale].action)
        }
        public getContentProperty(property: string, locale: string = null) {
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
        public getLabel() {
            return `${this.prefix}${this.identifier}`
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
                if (this.requirementContents) {
                    this.defineContent()
                    _.each(this.requirementContents, (c): void => {
                        this.contentDictionary[c.locale] = c
                    })
                }
            }

            var currentContent = _.find(this.requirementContents,
                (pc: RequirementContent) => pc.locale == GSDRequirements.currentLocale)

            if (currentContent)
                this.currentLocale = currentContent.locale
            else
                this.currentLocale = GSDRequirements.currentLocale

            if (!data || !data['package'])
                return;

            this.package = new Models.Package(data['package'])
            switch (this.type) {
                case RequirementType.functional:
                    this.prefix = "FR"
                    break;
                case RequirementType.nonFunction:
                    this.prefix = "NFR"
                    break;
            }

            this.defineContent()
            this.packageId = this.package.id
            this.requirementType = this.type
            this.defineDescription()
        }
        private defineDescription() {
            this.description = `${this.condition || ""} ${this.subject || ""} ${this.action || ""}`
        }
        public setLocale(locale: string) {
            var content = _.find(this.requirementContents, rc => rc.locale == locale)
            
            if (content)
                this.fillWithContent(content)
        }
        private defineContent() {
            var currentLocale = _.find(this.requirementContents,
                (c) => c.locale == GSDRequirements.currentLocale)
            if (currentLocale) {
                this.fillWithContent(currentLocale)
                return;
            }
            var enUsLocale = _.find(this.requirementContents,
                (c) => c.locale == "en-US")
            if (enUsLocale) {
                this.fillWithContent(enUsLocale)
                return;
            }
            this.fillWithContent(this.requirementContents[0])
        }
        private fillWithContent(content: RequirementContent) {
            this.condition = content.condition
            this.subject = content.subject
            this.action = content.action
            this.locale = content.locale

            this.defineDescription()
        }
        public canAddTranslation() {
            return this.requirementContents.length < GSDRequirements.localesAvailable.length
        }
    }
}
