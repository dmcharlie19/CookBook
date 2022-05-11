export class RegistrationRequestDto {
    constructor(
        public login: string,
        public password: string,
        public name: string) { }
}