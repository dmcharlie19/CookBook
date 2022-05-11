import { HttpErrorResponse } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';

@Injectable()
export class ErrorService {

    public onErrorOcured: EventEmitter<String> = new EventEmitter();
    public onErrorClear: EventEmitter<void> = new EventEmitter();

    public clearError(): void {
        this.onErrorClear.emit();
    }

    public handleError(error: HttpErrorResponse): void {

        var errorMessage: string = "";

        if (Math.floor(error.status / 100) == 5)
            errorMessage = "На сервере что-то пошло не так"
        else {
            if (error.error)
                errorMessage = error.error;
            else
                errorMessage = error.message;
        }


        this.onErrorOcured.emit(errorMessage);
    }
}