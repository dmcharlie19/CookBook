import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthenticateRequestDto, AuthenticateResponseDto } from '../models/authenticateDto';
import { RegistrationRequestDto } from '../models/registrationRequestDto';
import { tap, map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable()
export class AccountService {

    private loginUrl = "/api/account/login";
    private registrationUrl = "/api/account/registration";

    private AccesToken: string = null;
    private UserName: string = null;
    private expirTimeMinutes: Number = 0;

    constructor(private http: HttpClient) {
        console.log("create AuthService");
    }

    public getAccesToken(): string {
        return this.AccesToken;
    }

    public getUserName(): string {
        return this.UserName;
    }

    login(authenticate: AuthenticateRequestDto): Observable<Boolean> {
        return this.http.post(this.loginUrl, authenticate).pipe(
            map(
                (res: AuthenticateResponseDto) => {
                    return this.startSession(res)
                })
        );
    }

    registrate(registrationDto: RegistrationRequestDto): Observable<Object> {
        return this.http.post(this.registrationUrl, registrationDto);
    }

    private startSession(authResponse: AuthenticateResponseDto): Boolean {
        console.log("setSession");

        if (authResponse.accesToken == null)
            return false;
        if (authResponse.userName == null)
            return false;
        if (authResponse.expirTimeMinutes == 0)
            return false;

        this.AccesToken = authResponse.accesToken;
        this.UserName = authResponse.userName;
        this.expirTimeMinutes = authResponse.expirTimeMinutes;
        return true;
    }

    public logout() {

        this.AccesToken = null;
        this.UserName = null;
    }

    public isLoggedIn(): Boolean {
        return this.AccesToken != null;
    }

    isLoggedOut(): Boolean {
        return this.AccesToken == null;
    }
}