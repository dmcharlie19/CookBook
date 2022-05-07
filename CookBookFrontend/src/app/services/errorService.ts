import { HttpErrorResponse } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';

@Injectable()
export class ErrorService {

    onErrorOcured: EventEmitter<String> = new EventEmitter();;

    HandleError(error: HttpErrorResponse): void {

        var errorMessage: String = "";

        if (Math.floor(error.status / 100) == 5)
            errorMessage = "На сервере что-то пошло не так"
        else
            errorMessage = error.message;

        this.onErrorOcured.emit(errorMessage);
    }
}