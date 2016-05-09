module Models {

    declare var _ : any

    export class UseCaseRelationship {
        public cell: any
        public sourceId: string
        public targetId: string
        public type: UseCasesRelationType
        public label: string
        public isUseCasesRelation(diagram: UseCaseDiagram) {
            var source = <IUseCaseEntity>_.find(diagram.entities, e => e.cell.id == this.sourceId)
            var target = <IUseCaseEntity>_.find(diagram.entities, e => e.cell.id == this.targetId)

            if (!source || !target) {
                return false
            }
            
            return source.getType() == UseCaseEntityType.useCase &&
                   target.getType() == UseCaseEntityType.useCase
        }
    }
}