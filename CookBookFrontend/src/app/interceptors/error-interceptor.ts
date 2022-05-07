import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { ErrorService } from '../services/errorService';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(private errorService: ErrorService) {
        console.log("create ErrorInterceptor");

    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        console.log("intercept!");

        return next.handle(request)
            .pipe(
                catchError((error: HttpErrorResponse) => {
                    this.errorService.HandleError(error);
                    return throwError("error!");
                })
            );

    }
}