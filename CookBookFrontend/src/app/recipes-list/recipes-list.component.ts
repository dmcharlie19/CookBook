import { Component, OnInit } from '@angular/core';
import { RecipeService } from '../services/recipeService';
import { Recipe } from '../models/recipe';

@Component({
  selector: 'app-recipes-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: [ 
    '../common-styles.css',
    './recipes-list.component.css'],
  providers: [RecipeService]
})
export class RecipesListComponent implements OnInit {

  constructor(private todoService: RecipeService) {
    this.recipes = new Array<Recipe>();
  }

  // Массив рецептов
  recipes: Recipe[];

  ngOnInit(): void {
    this.LoadTodos();
  }
    
    // получаем данные через сервис
    LoadTodos() {
      this.todoService.GetRecipes().subscribe(
        (data: Recipe[]) => {
          for (var i = 0; i < data.length; i++) {
            var len = this.recipes.push(data[i]);
            console.log(this.recipes[len-1]);
            }
          },
          (error) => {
            console.log("failed load recipes");
        });
    }

}
