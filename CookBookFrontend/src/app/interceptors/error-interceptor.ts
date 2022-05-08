import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { ErrorService } from '../services/errorService';
import { AccountService } from '../services/AccountService';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(private errorService: ErrorService, private accountService: AccountService) {
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        console.log("intercept!");
        this.errorService.clearError();

        var idToken = this.accountService.getAccesToken();

        if (idToken) {
            const cloned = request.clone({
                headers: request.headers.set("Authorization",
                    "Bearer " + idToken)
            });

            return next.handle(cloned).pipe(
                catchError((error: HttpErrorResponse) => {
                    this.errorService.handleError(error);
                    return throwError("error!");
                })
            );
        }
        else {
            return next.handle(request).pipe(
                catchError((error: HttpErrorResponse) => {
                    this.errorService.handleError(error);
                    return throwError("error!");
                })
            );

        }
    }
}
