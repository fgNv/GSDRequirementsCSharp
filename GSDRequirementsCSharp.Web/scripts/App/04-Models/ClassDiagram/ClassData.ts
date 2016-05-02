module Models {

    declare var _: any

    export class ClassData {
        public id: string
        public type: ClassType
        public classMethods: Array<ClassMethod>
        public classProperties: Array<ClassProperty>
        public name: string
        public x: number
        public y: number
        public cell: any

        public constructor(data: any = null) {
            if (data) {
                for (var prop in data) {
                    this[prop] = data[prop]
                }
                this.classMethods = _.map(data.classMethods, (cm) => new ClassMethod(cm))
                this.classProperties = _.map(data.classProperties, (cp) => new ClassProperty(cp))
            } else {
                this.classMethods = []
                this.classProperties = []
            }
        }

        public addProperty(): void {
            this.classProperties.push(new ClassProperty())
        }

        public addMethod(): void {
            this.classMethods.push(new ClassMethod())
        }

        public removeProperty(property): void {
            this.classProperties = _.filter(this.classProperties, (p) => p != property)
        }

        public removeMethod(method): void {
            this.classMethods = _.filter(this.classMethods, (m) => m != method)
        }
    }
}