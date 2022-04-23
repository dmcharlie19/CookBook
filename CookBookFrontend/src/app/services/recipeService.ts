import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable()
export class RecipeService {

    private url = "/api/Recipe/";

    constructor(private http: HttpClient) {
    }

    GetRecipes() {
        return this.http.get(this.url + "get-all").pipe(catchError(err => {  
            console.log("ERROR!");
            return throwError(err);
        }));
    }
}