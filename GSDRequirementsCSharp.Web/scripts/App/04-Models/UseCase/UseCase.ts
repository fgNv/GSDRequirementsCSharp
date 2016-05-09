module Models {

    declare var _: any
    declare var GSDRequirements: Globals.GSDRequirementsData

    export class UseCase implements IDiagramElement, IUseCaseEntity {
        public cell: any
        public x: number
        public y: number
        public contents: Array<UseCaseContent>
        public preConditions: Array<PreCondition>
        public postConditions: Array<PostCondition>
        public contentDictionary: Object
        public currentContentLocale: string
        public getContentProperty(property : string) {
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
            return UseCaseEntityType.useCase
        }
        private setInitialLocale() {

            if (!this.contents) {
                this.currentContentLocale = GSDRequirements.currentLocale
                return
            }

            var currentLocaleAvailable = _.any(this.contents,
                (c: UseCaseContent) => c.locale == GSDRequirements.currentLocale)

            if (currentLocaleAvailable || this.contents.length == 0) {
                this.currentContentLocale = GSDRequirements.currentLocale
                return
            }

            var enUsAvailable = _.any(this.contents,
                (c: UseCaseContent) => c.locale == "en-US")

            if (enUsAvailable) {
                this.currentContentLocale = "en-US"
                return
            }

            this.currentContentLocale = this.contents[0].locale
        }
        public constructor(data: Object = null) {
            this.contentDictionary = {}
            this.preConditions = []
            this.postConditions = []

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
            var content = <UseCaseContent>this.contentDictionary[locale]
            return content && content.name
        }
        public addPostCondition() {
            this.postConditions.push(new PostCondition())
        }
        public addPreCondition() {
            this.preConditions.push(new PreCondition())
        }
        public removePostCondition(postCondition) {
            this.postConditions = _.filter(this.postConditions, pc => pc != postCondition)
        }
        public removePreCondition(preCondition) {
            this.preConditions = _.filter(this.preConditions, pc => pc != preCondition)
        }
    }
}