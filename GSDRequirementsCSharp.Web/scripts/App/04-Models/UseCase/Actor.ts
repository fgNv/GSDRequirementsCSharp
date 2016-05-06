module Models {
    export class Actor implements IDiagramElement, IUseCaseEntity {
        public cell: any
        public name: string
        public x: number
        public y: number
        public getType() {
            return UseCaseEntityType.actor
        }
    }
}