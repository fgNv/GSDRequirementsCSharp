module Models {
    export class ClassData {
        public id : string
        public type: ClassType
        public classMethods: Array<ClassMethod>
        public classProperties: Array<ClassProperty>
        public name: string
    }
}