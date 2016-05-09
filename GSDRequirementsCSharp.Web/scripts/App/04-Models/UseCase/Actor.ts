module Models {

    declare var _: any
    declare var GSDRequirements: Globals.GSDRequirementsData

    export class Actor implements IDiagramElement, IUseCaseEntity {
        public cell: any
        public x: number
        public y: number
        public contents: Array<ActorContent>
        public contentDictionary: Object
        public currentContentLocale: string
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
        public getName() {
            return this.getContentProperty("name")
        }
        public getType() {
            return UseCaseEntityType.actor
        }

        private setInitialLocale() {
            
            if (!this.contents) {
                this.currentContentLocale = GSDRequirements.currentLocale
                return
            }

            var currentLocaleAvailable = _.any(this.contents,
                (c: ActorContent) => c.locale == GSDRequirements.currentLocale)

            if (currentLocaleAvailable || this.contents.length == 0) {
                this.currentContentLocale = GSDRequirements.currentLocale
                return
            }

            var enUsAvailable = _.any(this.contents,
                (c: ActorContent) => c.locale == "en-US")

            if (enUsAvailable) {
                this.currentContentLocale = "en-US"
                return
            }

            this.currentContentLocale = this.contents[0].locale
        }
        public constructor(data: Object = null) {
            this.contentDictionary = {}

            _.each(GSDRequirements.localesAvailable, (locale) => {
                this.contentDictionary[locale.name] = {}
            })

            if (data) {
                for (var p in data)
                    this[p] = data

                _.each(this.contents, (c) => {
                    this.contentDictionary[c.locale] = c
                })
            }

            this.setInitialLocale()
        }

        public containsLocale(locale: string) {
            var content = <ActorContent>this.contentDictionary[locale]

            return content && content.name
        }
    }
}