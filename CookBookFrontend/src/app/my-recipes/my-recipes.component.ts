import { Component, OnInit } from '@angular/core';
import { RecipeShortInfoResponceDto } from '../models/recipe';
import { AccountService } from '../services/AccountService';
import { RecipeService } from '../services/recipeService';

@Component({
  selector: 'app-my-recipes',
  templateUrl: './my-recipes.component.html',
  styleUrls: ['./my-recipes.component.css'],
  providers: [RecipeService]
})
export class MyRecipesComponent implements OnInit {

  recipes: RecipeShortInfoResponceDto[];

  constructor(public accountServise: AccountService, private recipeService: RecipeService) {
    this.recipes = new Array<RecipeShortInfoResponceDto>();
  }

  ngOnInit(): void {
    let userId: Number = this.accountServise.getUserId();

    this.recipeService.getRecipesByUserId(userId).subscribe(
      (data: RecipeShortInfoResponceDto[]) => {
        this.recipes.push(...data);
      })

  }

}
