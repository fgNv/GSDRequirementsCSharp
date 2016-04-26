module Models {
    export class ClassProperty {
        public id: string
        public returnType: string
        public name: string
        public Visibility: Visibility

        public getDescription() {
            var visibility = this.Visibility == Visibility.Public ? "+" : "-"

            return `${visibility} ${this.name} : ${this.returnType}`
        }
    }
}
