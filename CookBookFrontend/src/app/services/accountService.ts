import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthenticateRequestDto, AuthenticateResponseDto } from '../models/authenticateDto';
import { RegistrationRequestDto } from '../models/registrationRequestDto';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

export class UserInfoStorage {

    private _userInfoKey = "userInfo";

    constructor() {
    }

    public setUserInfo(userInfo: AuthenticateResponseDto): void {
        localStorage.setItem(this._userInfoKey, JSON.stringify(userInfo));
    }

    public getUserInfo(): AuthenticateResponseDto | null {
        try {
            let userInfo: AuthenticateResponseDto = JSON.parse(localStorage.getItem(this._userInfoKey));
            return userInfo;
        }
        catch {
            return null;
        }
    }

    public clearUserInfo(): void {
        localStorage.setItem(this._userInfoKey, "");
    }
}

@Injectable()
export class AccountService {

    private loginUrl = "/api/account/login";
    private registrationUrl = "/api/account/registration";
    private userInfoStorage: UserInfoStorage;

    constructor(private http: HttpClient) {

        this.userInfoStorage = new UserInfoStorage();
    }

    public getAccesToken(): string | null {
        let userInfo: AuthenticateResponseDto = this.userInfoStorage.getUserInfo();
        return userInfo ? userInfo.accesToken : null;
    }

    public getUserName(): string | null {
        let userInfo: AuthenticateResponseDto = this.userInfoStorage.getUserInfo();
        return userInfo ? userInfo.userName : null;
    }

    public getUserId(): Number | null {
        let userInfo: AuthenticateResponseDto = this.userInfoStorage.getUserInfo();
        return userInfo ? userInfo.id : null;
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

        this.userInfoStorage.setUserInfo(authResponse);
        return true;
    }

    public logout() {
        this.userInfoStorage.clearUserInfo();
    }

    public isLoggedIn(): Boolean {

        let userInfo: AuthenticateResponseDto = this.userInfoStorage.getUserInfo();

        if (userInfo != null) {
            if (userInfo.expiresAt < new Date()) {
                this.logout();
                return false
            }

            return userInfo.accesToken != "";
        }
        return false;
    }

    isLoggedOut(): Boolean {
        return !this.isLoggedIn();
    }
}