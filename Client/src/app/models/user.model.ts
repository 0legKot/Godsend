export class IdentityUser {
    constructor(
        public id: string = '',
        public name: string = '',
        public email: string = '',
        public birth: string = '',
        public registrationDate: string = '',
        public rating: number = 0
    ) { }
}
