module Models {
    
    declare var _: any
    declare var GSDRequirements: Globals.GSDRequirementsData

    export class PostCondition {
        public contents: Array<PostConditionContent>
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

            _.each(GSDRequirements.localesAvailable, (locale) => {
                this.contentDictionary[locale.name] = new PostConditionContent()
            })

            if (data) {
                for (var p in data)
                    this[p] = data

                _.each(this.contents, (c) => {
                    this.contentDictionary[c.locale] = c
                })
            }
        }
    }
}