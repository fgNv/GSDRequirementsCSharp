module Models {
    declare var _: any;

    export class ClassMethodParameter {
        public id: string
        public type: string
        public name: string

        public getDescription(): string {
            return `${this.name} : ${this.type}`
        }
    }
}