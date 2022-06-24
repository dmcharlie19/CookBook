import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { DeleteRecipeDialogComponent } from '../dialog-components/delete-recipe-dialog/delete-recipe-dialog.component';
import { RecipeFullInfoResponceDto, RecipeIngredient, RecipeShortInfoResponceDto } from '../models/recipe';
import { AccountService } from '../services/AccountService';
import { NavigationService } from '../services/navigationSrvice';
import { RecipeService } from '../services/recipeService';

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.css'],
  providers: [RecipeService]
})
export class RecipeDetailComponent implements OnInit {

  constructor(private route: ActivatedRoute,
    private recipeService: RecipeService,
    public navigation: NavigationService,
    private accountServise: AccountService,
    private dialog: MatDialog) { }

  public recipeFullInfoResponceDto: RecipeFullInfoResponceDto = null;
  public isMyRecipe: Boolean = false;
  private recipeId: Number;

  ngOnInit(): void {

    this.recipeId = this.route.snapshot.params['id'];

    this.recipeService.getRecipeFullInfo(this.recipeId).subscribe(
      (data: RecipeFullInfoResponceDto) => {
        this.recipeFullInfoResponceDto = data;

        this.isMyRecipe = (this.accountServise.isLoggedIn() &&
          this.accountServise.getUserId() == this.recipeFullInfoResponceDto.recipeShortInfo.authorId)
      }
    )
  }

  back(): void {
    this.navigation.back()
  }

  onDeleteRecipe() {
    let dialogRef = this.dialog.open(DeleteRecipeDialogComponent, { disableClose: true });

    dialogRef.afterClosed().subscribe(result => {
      if (result.event == 'delete') {
        this.recipeService.deleteRecipe(this.recipeFullInfoResponceDto.recipeShortInfo.id)
          .subscribe(() => this.back());
      }
    });
  }

  // getTestData(): RecipeFullInfoResponceDto {

  //   let short = new RecipeShortInfoResponceDto(
  //     0,
  //     "Классическая шарлотка",
  //     "Важное сладкое блюдо советской и постсоветской истории. Легкое, пышное тесто, максимум яблочной начинки — у шарлотки всегда был образ приятного, простого и при этом лакомого и диетического блюда.",
  //     40,
  //     ["выпечка", "вкусно", "нямка"],
  //     50,
  //     71,
  //     0,
  //     "Коля")

  //   let steps = new Array<string>("Разогреть духовку. Отделить белки от желтков. Белки взбить в крепкую пену, постепенно добавляя сахар.",
  //     "Продолжать взбивать, добавляя по одному желтки, затем гашеную соду и муку. Тесто по консистенции должно напоминать сметану.",
  //     "Смазать противень растительным маслом. Вылить половину теста на противень, разложить равномерно нарезанные дольками яблоки, залить второй половиной теста.");
  //   let ingr = new Array<RecipeIngredient>();
  //   ingr.push({ title: "Для панна коты", ingredients: ["Сливки-20-30% - 500мл.", " Молоко - 100мл.", " Желатин - 2ч.л.", "Сахар - 3ст.л.", "Ванильный сахар - 2 ч.л."] },
  //     { title: "Для клубничного желе", ingredients: ["Сливки-20-30% - 500мл.", " Молоко - 100мл.", " Желатин - 2ч.л.", "Сахар - 3ст.л.", "Ванильный сахар - 2 ч.л."] });

  //   return { recipeShortInfo: short, cookingSteps: steps, recipeIngridients: ingr };
  // }

}
