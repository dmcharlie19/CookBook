import { Component, OnInit } from '@angular/core';
import { RecipeService } from '../services/recipeService';
import { RecipeShortInfoResponceDto } from '../models/recipe';

@Component({
  selector: 'app-recipes-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.css'],
  providers: [RecipeService]
})
export class RecipesListComponent implements OnInit {
  constructor(private recipeService: RecipeService) {
    this.recipes = new Array<RecipeShortInfoResponceDto>();

    var str = new Array<string>("выпечка", "вкусно", "нямка");

    this.recipes.push(new RecipeShortInfoResponceDto(0, "брауни",
      "Главный секрет идеальных сырников — а точнее творожников, — творог нужно протереть через мелкое сито и отжать от влаги. Жирность предпочтительна не больше и не меньше 9%. Тесто должно получиться эластичным, чтобы при надавливании сырник не треснул на сковородке, а сохранил форму. Если все сделать правильно, получатся нежные однородные кругляшки под плотной румяной корочкой. Сырники можно запекать в духовке или готовить на пару. В рецепте не исключаются эксперименты с начинкой — сухофрукты, орехи, свежие фрукты и даже картофель лишними не будут.",
      50,
      str,
      5,
      6));

    this.recipes.push(new RecipeShortInfoResponceDto(0, "Классическая шарлотка",
      "Важное сладкое блюдо советской и постсоветской истории. Легкое, пышное тесто, максимум яблочной начинки — у шарлотки всегда был образ приятного, простого и при этом лакомого и диетического блюда.",
      40,
      str,
      50,
      71));
  }

  // Массив рецептов
  recipes: RecipeShortInfoResponceDto[];

  ngOnInit(): void {
    this.loadRecipes();
  }

  // получаем данные через сервис
  loadRecipes() {
    this.recipeService.GetRecipes().subscribe(
      (data: RecipeShortInfoResponceDto[]) => {
        this.recipes.push(...data);
      })
  }

}
