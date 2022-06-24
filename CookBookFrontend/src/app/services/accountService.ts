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

    private baseUrl = "/api/account"

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
        const url = this.baseUrl + "/login"
        return this.http.post(url, authenticate).pipe(
            map(
                (res: AuthenticateResponseDto) => {
                    return this.startSession(res)
                })
        );
    }

    registrate(registrationDto: RegistrationRequestDto): Observable<Object> {
        const url = this.baseUrl + "/registration";
        return this.http.post(url, registrationDto);
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

    public isLoggedOut(): Boolean {
        return !this.isLoggedIn();
    }

    public addLike(recipeId: Number): Observable<Object> {
        const url = `${this.baseUrl}/addLike/${recipeId}`;
        return this.http.post(url, null);
    }

    public addFavorite(recipeId: Number): Observable<Object> {
        const url = `${this.baseUrl}/addFavorite/${recipeId}`;
        return this.http.post(url, null);
    }
}