import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RecipeFullInfoResponceDto, RecipeIngredient, RecipeShortInfoResponceDto } from '../models/recipe';
import { RecipeService } from '../services/recipeService';

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.css'],
  providers: [RecipeService]
})
export class RecipeDetailComponent implements OnInit {

  constructor(private route: ActivatedRoute, private recipeService: RecipeService) { }

  public recipeFullInfoResponceDto: RecipeFullInfoResponceDto = null;

  private recipeId: Number;

  ngOnInit(): void {

    this.recipeId = this.recipeId = this.route.snapshot.params['id'];

    this.recipeService.getRecipeFullInfo(this.recipeId).subscribe(
      (data: RecipeFullInfoResponceDto) => {
        this.recipeFullInfoResponceDto = data;
      }
    )

    console.log(this.recipeFullInfoResponceDto);
  }

  getTestData(): RecipeFullInfoResponceDto {

    let short = new RecipeShortInfoResponceDto(
      0,
      "Классическая шарлотка",
      "Важное сладкое блюдо советской и постсоветской истории. Легкое, пышное тесто, максимум яблочной начинки — у шарлотки всегда был образ приятного, простого и при этом лакомого и диетического блюда.",
      40,
      ["выпечка", "вкусно", "нямка"],
      50,
      71,
      0,
      "Коля")

    let steps = new Array<string>("Разогреть духовку. Отделить белки от желтков. Белки взбить в крепкую пену, постепенно добавляя сахар.",
      "Продолжать взбивать, добавляя по одному желтки, затем гашеную соду и муку. Тесто по консистенции должно напоминать сметану.",
      "Смазать противень растительным маслом. Вылить половину теста на противень, разложить равномерно нарезанные дольками яблоки, залить второй половиной теста.");
    let ingr = new Array<RecipeIngredient>();
    ingr.push({ title: "Для панна коты", ingredients: ["Сливки-20-30% - 500мл.", " Молоко - 100мл.", " Желатин - 2ч.л.", "Сахар - 3ст.л.", "Ванильный сахар - 2 ч.л."] },
      { title: "Для клубничного желе", ingredients: ["Сливки-20-30% - 500мл.", " Молоко - 100мл.", " Желатин - 2ч.л.", "Сахар - 3ст.л.", "Ванильный сахар - 2 ч.л."] });

    return { shortInfo: short, cookingSteps: steps, recipeIngridients: ingr };
  }

}

