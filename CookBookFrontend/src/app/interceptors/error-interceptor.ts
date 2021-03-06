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
        this.errorService.clearError();

        const accesToken: string = this.accountService.getAccesToken();

        if (accesToken) {
            const cloned = request.clone({
                headers: request.headers.set("Authorization",
                    "Bearer " + accesToken)
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
