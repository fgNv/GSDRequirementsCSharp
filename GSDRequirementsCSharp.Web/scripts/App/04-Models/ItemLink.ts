module Models {
    export class ItemLink {
        public origin: SpecificationItem
        public target: SpecificationItem
        public id: string
        constructor(data: Object) {
            this.origin = new SpecificationItem(data['origin'])
            this.target = new SpecificationItem(data['target'])
            this.id = data['id']
        }
        public getDescription() {
            return `${this.origin.label}(${this.origin.typeLabel}) -> 
                    ${this.target.label}(${this.target.typeLabel})`
        }
    }
}