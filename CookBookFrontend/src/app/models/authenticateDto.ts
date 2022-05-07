export class AuthenticateRequestDto {
    constructor(
        public login: String,
        public password: String) { }
}

export class AuthenticateResponseDto {
    constructor(
        public accesToken: String,
        public userName: string,
        public expirTimeMinutes: Number) { }
}