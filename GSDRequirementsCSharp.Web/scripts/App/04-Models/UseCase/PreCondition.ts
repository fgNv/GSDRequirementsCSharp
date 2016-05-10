module Models {

    declare var _: any
    declare var GSDRequirements: Globals.GSDRequirementsData

    export class PreCondition {
        public contents: Array<PreConditionContent>
        public contentDictionary: Object
        public getContentProperty(property: string, locale: string = null) {
            if (locale) {
                var currentLocale = this.contentDictionary[locale]
                if (currentLocale && currentLocale[property])
                    return currentLocale[property]
            }

            var enUs = this.contentDictionary["en-US"]
            if (enUs && enUs[property])
                return enUs[property]

            var anyAvailableLocale = _.find(this.contentDictionary, (val, key) => val[property])
            if (anyAvailableLocale && anyAvailableLocale[property])
                return anyAvailableLocale[property]

            return ""
        }
        public constructor(data: Object = null) {
            this.contentDictionary = {}

            _.each(GSDRequirements.localesAvailable, (locale: Models.Locale) => {
                var c = new PreConditionContent()
                c.locale = locale.name
                this.contentDictionary[locale.name] = c
            })

            if (data) {
                for (var p in data)
                    this[p] = data[p]

                _.each(this.contents, (c) => {
                    this.contentDictionary[c.locale] = c
                })
            }
        }
        public populateContents() {
            this.contents = _.chain(this.contentDictionary)
                .filter((c: Models.PreConditionContent) => c.description)
                .map((c, k) => c)
                .value()
        }
    }
}