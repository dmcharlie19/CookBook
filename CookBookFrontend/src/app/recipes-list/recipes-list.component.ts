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

    var str = new Array<String>("выпечка", "вкусно", "нямка");

    this.recipes.push(new Recipe(0, "брауни",
      "Главный секрет идеальных сырников — а точнее творожников, — творог нужно протереть через мелкое сито и отжать от влаги. Жирность предпочтительна не больше и не меньше 9%. Тесто должно получиться эластичным, чтобы при надавливании сырник не треснул на сковородке, а сохранил форму. Если все сделать правильно, получатся нежные однородные кругляшки под плотной румяной корочкой. Сырники можно запекать в духовке или готовить на пару. В рецепте не исключаются эксперименты с начинкой — сухофрукты, орехи, свежие фрукты и даже картофель лишними не будут.",
      50,
      str,
      5,
      6));

    this.recipes.push(new Recipe(0, "Классическая шарлотка",
      "Важное сладкое блюдо советской и постсоветской истории. Легкое, пышное тесто, максимум яблочной начинки — у шарлотки всегда был образ приятного, простого и при этом лакомого и диетического блюда.",
      40,
      str,
      50,
      71));
  }

  // Массив рецептов
  recipes: Recipe[];
  // Флаг ошибки сервера
  isServerError: Boolean = false;

  ngOnInit(): void {
    this.loadRecipes();
  }

  // получаем данные через сервис
  loadRecipes() {

    this.todoService.GetRecipes().subscribe(
      (data: Recipe[]) => {
        for (var i = 0; i < data.length; i++) {
          this.recipes.push(data[i]);
        }
        this.isServerError = false;
      },
      (error) => {
        console.log("failed load recipes");
        this.isServerError = true;
      });
  }

}
