import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthenticateRequestDto, AuthenticateResponseDto } from '../models/authenticateDto';
import { RegistrationRequestDto } from '../models/registrationRequestDto';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable()
export class AccountService {

    private _accesTokenKey = "accesToken";
    private _userNameKey = "userName";

    private loginUrl = "/api/account/login";
    private registrationUrl = "/api/account/registration";

    private expirTimeMinutes: Number = 0;

    constructor(private http: HttpClient) {
    }

    public getAccesToken(): string {
        return localStorage.getItem("accesToken");
    }

    public getUserName(): string {
        return localStorage.getItem(this._userNameKey);
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

        localStorage.setItem(this._accesTokenKey, authResponse.accesToken);
        localStorage.setItem(this._userNameKey, authResponse.userName);
        
        this.expirTimeMinutes = authResponse.expirTimeMinutes;
        return true;
    }

    public logout() {
        localStorage.setItem(this._accesTokenKey, "");
        localStorage.setItem(this._userNameKey, "");
    }

    public isLoggedIn(): Boolean {
        return localStorage.getItem(this._accesTokenKey) != "";
    }

    isLoggedOut(): Boolean {
        return !this.isLoggedIn;
    }
}