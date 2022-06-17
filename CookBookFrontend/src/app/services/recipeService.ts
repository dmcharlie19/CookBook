import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AddRecipeRequestDto } from '../models/recipe';

@Injectable()
export class RecipeService {

    private readonly url = "/api/Recipes";
    private readonly getRecipesUrl = "/api/Recipes/getRecipes";
    private readonly recipesByUserUrl = "/api/Recipes/User";
    private readonly recipeFullUrl = "/api/Recipes";
    private readonly addRecipeUrl = "/api/Recipes/AddRecipe";
    private readonly imageUrl = "/api/Recipes/Image";
    private readonly deleteUrl = "/api/Recipes/delete";

    constructor(private http: HttpClient) {
    }

    getRecipes(page: Number) {
        return this.http.get(`${this.getRecipesUrl}/${page}`);
    }

    getRecipesByUserId(userId: Number) {
        return this.http.get(`${this.recipesByUserUrl}/${userId}`);
    }

    getRecipeFullInfo(id: Number) {
        return this.http.get(`${this.recipeFullUrl}/${id}`);
    }

    addRecipe(registrationDto: AddRecipeRequestDto, imageFile: File): Observable<Object> {

        const formData = new FormData();
        formData.append('image', imageFile);

        const data: string = JSON.stringify(registrationDto);
        formData.append('data', data);

        return this.http.post(this.addRecipeUrl, formData);
    }

    getRecipeImage(id: Number): Observable<Blob> {
        return this.http.get(`${this.imageUrl}/${id}`, { responseType: 'blob' });
    }

    deleteRecipe(id: Number) {
        return this.http.put(this.deleteUrl, id);
    }

    searchRecipe(searchRequest: string) {
        return this.http.get(`${this.url}/Search/${searchRequest}`);
    }
}