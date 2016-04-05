module Models {
    export class Permission {
        public id: string
        public profile: profile
        public user: Object
        constructor(data: Object) {
            this.id = data['id']
            this.profile = data['profile']
            this.user = {
                'name':data['user']['contact']['name'],
                'email': data['user']['contact']['email'],
                'id': data['user']['id']
            }            
        }
    }
}