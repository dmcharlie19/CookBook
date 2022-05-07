import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable()
export class RecipeService {

    private url = "/api/Recipes";

    constructor(private http: HttpClient) {
    }

    GetRecipes() {
        return this.http.get(this.url);
    }
}