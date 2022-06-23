import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AddRecipeRequestDto } from '../models/recipe';

@Injectable()
export class RecipeService {

    private readonly recipesUrl = "/api/Recipes";

    constructor(private http: HttpClient) {
    }

    getRecipes(page: Number) {
        const url = `${this.recipesUrl}/getRecipes/${page}`;
        return this.http.get(url);
    }

    getRecipesByUserId(userId: Number) {
        const url = `${this.recipesUrl}/users/${userId}`;
        return this.http.get(url);
    }

    getRecipeFullInfo(id: Number) {
        const url = `${this.recipesUrl}/getRecipeFull/${id}`;
        return this.http.get(url);
    }

    addRecipe(addRecipeRequest: AddRecipeRequestDto, imageFile: File): Observable<Object> {

        const formData = new FormData();
        formData.append('image', imageFile);

        const data: string = JSON.stringify(addRecipeRequest);
        formData.append('data', data);

        const url = `${this.recipesUrl}/addRecipe`;
        return this.http.post(url, formData);
    }

    getRecipeImage(id: Number): Observable<Blob> {
        const url = `${this.recipesUrl}/images/${id}`;
        return this.http.get(url, { responseType: 'blob' });
    }

    deleteRecipe(id: Number) {
        const url = `${this.recipesUrl}/delete/${id}`;
        return this.http.delete(url);
    }

    searchRecipe(searchRequest: string) {
        const url = `${this.recipesUrl}/search/${searchRequest}`;
        return this.http.get(url);
    }
}