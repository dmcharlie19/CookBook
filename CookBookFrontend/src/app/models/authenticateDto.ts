export class AuthenticateRequestDto {
    constructor(
        public login: string,
        public password: string) { }
}

export class AuthenticateResponseDto {
    constructor(
        public id: Number,
        public accesToken: string,
        public userName: string,
        public expiresAt: Date) { }
}