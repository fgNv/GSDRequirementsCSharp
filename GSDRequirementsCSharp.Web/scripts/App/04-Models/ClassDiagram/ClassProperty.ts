module Models {
    export class ClassProperty {
        public id: string
        public type: string
        public name: string
        public Visibility: Visibility

        public getDescription() {
            var visibility = this.Visibility == Visibility.Public ? "+" : "-"

            return `${visibility} ${this.name} : ${this.type}`
        }

        public constructor(data: any = null) {
            if (data) {
                for (var prop in data) {
                    this[prop] = data[prop]
                }
            }
        }
    }
}
