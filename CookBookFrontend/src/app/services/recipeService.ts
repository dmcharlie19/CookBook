import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AddRecipeRequestDto } from '../models/recipe';

@Injectable()
export class RecipeService {

    private readonly url = "/api/Recipes";
    private readonly addRecipeUrl = "/api/Recipes/AddRecipe";

    constructor(private http: HttpClient) {
    }

    getRecipes() {
        return this.http.get(this.url);
    }

    getRecipeFullInfo(id: Number) {
        return this.http.get(`${this.url}/${id}`);
    }

    addRecipe(registrationDto: AddRecipeRequestDto): Observable<Object> {
        return this.http.post(this.addRecipeUrl, registrationDto);
    }
}