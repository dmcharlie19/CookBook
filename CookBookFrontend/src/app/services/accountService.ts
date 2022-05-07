import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthenticateRequestDto, AuthenticateResponseDto } from '../models/authenticateDto';
import { tap, map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable()
export class AccountService {

    private loginUrl = "/api/account/login";

    private AccesToken: String = null;
    public UserName: string = null;
    private expirTimeMinutes: Number = 0;

    constructor(private http: HttpClient) {
        console.log("create AuthService");
    }

    login(authenticate: AuthenticateRequestDto) {
        return this.http.post(this.loginUrl, authenticate).pipe(
            map(
                (res: AuthenticateResponseDto) => {
                    return this.startSession(res)
                })
        );
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

    // getExpiration() {
    //     const expiration = localStorage.getItem("expires_at");
    //     const expiresAt = JSON.parse(expiration);
    //     return moment(expiresAt);
    // }
}