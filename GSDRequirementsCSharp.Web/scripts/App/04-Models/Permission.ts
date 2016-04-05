module Models {
    export class Permission {
        public id: string
        public profile: profile
        public user: Object
        public userId: number
        constructor(data: Object) {
            this.id = data['id']
            this.profile = data['profile']
            this.user = {
                'name':data['user']['contact']['name'],
                'email': data['user']['contact']['email']
            }            
            this.userId = data['user']['id']
        }
    }
}