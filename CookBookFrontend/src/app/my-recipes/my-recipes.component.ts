import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RecipeShortInfoResponceDto } from '../models/recipe';
import { AccountService } from '../services/AccountService';
import { ErrorService } from '../services/errorService';
import { RecipeService } from '../services/recipeService';

@Component({
  selector: 'app-my-recipes',
  templateUrl: './my-recipes.component.html',
  styleUrls: ['./my-recipes.component.css'],
  providers: [RecipeService]
})
export class MyRecipesComponent implements OnInit {

  private userId: number;
  public isLoaded: boolean = false;
  public isMyPage: boolean = false;

  // Массив рецептов
  recipes: RecipeShortInfoResponceDto[];

  constructor(
    public accountServise: AccountService,
    private recipeService: RecipeService,
    private route: ActivatedRoute) {
    this.recipes = new Array<RecipeShortInfoResponceDto>();
  }

  ngOnInit(): void {
    this.userId = this.route.snapshot.params['id'];
    if (isNaN(this.userId)) {
      console.log("Пользователь не найден");
      return;
    }

    this.isMyPage = (this.accountServise.isLoggedIn() &&
      this.accountServise.getUserId() == this.userId)

    this.recipeService.getRecipesByUserId(this.userId).subscribe(
      (data: RecipeShortInfoResponceDto[]) => {
        this.recipes.push(...data);
        this.isLoaded = true;
      })

  }

}
