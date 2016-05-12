module Models {

    declare var _: any
    declare var GSDRequirements: Globals.GSDRequirementsData

    export class UseCaseRelationship {
        public cell: any
        public sourceId: string
        public targetId: string
        public type: UseCasesRelationType
        public contentDictionary: Object
        public getContentProperty(property: string, locale: string = null) {
            var currentLocale = this.contentDictionary[locale || GSDRequirements.currentLocale]
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
        public getDescription(locale: string = null) {
            return this.getContentProperty("description", locale)
        }
        public contents: Array<UseCaseEntityRelationContent>
        public isUseCasesRelation(diagram: UseCaseDiagram) {
            var source = <IUseCaseEntity>_.find(diagram.entities, e => e.cell.id == this.sourceId)
            var target = <IUseCaseEntity>_.find(diagram.entities, e => e.cell.id == this.targetId)

            if (!source || !target) {
                return false
            }

            return source.getType() == UseCaseEntityType.useCase &&
                target.getType() == UseCaseEntityType.useCase
        }
        public constructor(data: Object = null) {
            this.contentDictionary = {}

            _.each(GSDRequirements.localesAvailable, (locale: Models.Locale) => {
                var content = new UseCaseEntityRelationContent(locale.name)
                this.contentDictionary[locale.name] = content
            })

            if (data) {
                for (var p in data)
                    this[p] = data[p]

                _.each(this.contents, (c): void => {
                    this.contentDictionary[c.locale] = c
                })
            }
        }
        public populateContents() {
            if (this.type != Models.UseCasesRelationType.association)
                return

            this.contents = _.chain(this.contentDictionary)
                .map((v, k) => v)
                .filter(v => v.description)
                .value()
        }
    }
}