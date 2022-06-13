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
    private _userIdKey = "userId";
    private _expiresAtKey = "expiresAt";

    private loginUrl = "/api/account/login";
    private registrationUrl = "/api/account/registration";

    constructor(private http: HttpClient) {
    }

    public getAccesToken(): string {
        return localStorage.getItem("accesToken");
    }

    public getUserName(): string {
        return localStorage.getItem(this._userNameKey);
    }

    public getUserId(): Number {
        return Number(localStorage.getItem(this._userIdKey));
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
        if (authResponse.accesToken == null)
            return false;
        if (authResponse.userName == null)
            return false;

        let date = new Date();
        if (authResponse.expiresAt < date)
            return false;

        localStorage.setItem(this._accesTokenKey, authResponse.accesToken);
        localStorage.setItem(this._userNameKey, authResponse.userName);
        localStorage.setItem(this._expiresAtKey, authResponse.expiresAt.toString());
        localStorage.setItem(this._userIdKey, authResponse.id.toString());

        return true;
    }

    public logout() {
        localStorage.setItem(this._accesTokenKey, "");
        localStorage.setItem(this._userNameKey, "");
    }

    public isLoggedIn(): Boolean {

        let expiresAtStr = localStorage.getItem(this._expiresAtKey);

        if (expiresAtStr != null && expiresAtStr != "") {
            let expiresAt: Number = Date.parse(expiresAtStr);
            if (expiresAt < Date.now()) {
                this.logout();
                return false
            }

            return localStorage.getItem(this._accesTokenKey) != "";
        }
        return false;
    }

    isLoggedOut(): Boolean {
        return !this.isLoggedIn;
    }
}