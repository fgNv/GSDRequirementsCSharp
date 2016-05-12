module Models {

    declare var _: any

    export class UseCaseRelationsOnEdit extends Array<UseCaseRelationship> {
        public containsLocale(locale: string) {
            var associations = _.filter(this,
                (r: UseCaseRelationship) => r.type == UseCasesRelationType.association)
            
            return associations.length > 0 && _.all(associations, (r: UseCaseRelationship) => {
                var content = <UseCaseEntityRelationContent>r.contentDictionary[locale]
                return content && content.description
            })
        }

        public constructor(items: Array<UseCaseRelationship> = null) {
            super()
            if (items) {
                _.each(items, ((i: UseCaseRelationship): void => { this.push(i) }))
            }
        }
    }
}