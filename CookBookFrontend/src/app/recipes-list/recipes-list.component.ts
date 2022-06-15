import { Component, OnInit } from '@angular/core';
import { RecipeService } from '../services/recipeService';
import { RecipeShortInfoResponceDto } from '../models/recipe';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { NotAtentificateComponent } from '../not-atentificate/not-atentificate.component';
import { AccountService } from '../services/AccountService';

export class imagesUrl {
  recipeId: number;
  imageUrl: string;
}

@Component({
  selector: 'app-recipes-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.css'],
  providers: [RecipeService, MatDialog]
})
export class RecipesListComponent implements OnInit {

  // Массив рецептов
  recipes: RecipeShortInfoResponceDto[];
  images = {};

  constructor(private recipeService: RecipeService,
    public dialog: MatDialog,
    private router: Router,
    private accountService: AccountService) {
    this.recipes = new Array<RecipeShortInfoResponceDto>();
  }

  ngOnInit(): void {
    this.loadRecipes();
  }

  // получаем данные через сервис
  loadRecipes() {
    this.recipeService.getRecipes().subscribe(
      (data: RecipeShortInfoResponceDto[]) => {
        this.recipes.push(...data);
      });
  }

  addRecipeClick() {
    if (this.accountService.isLoggedOut()) {
      this.dialog.open(NotAtentificateComponent, { disableClose: false });
    }
    else {
      this.router.navigateByUrl("/addRecipe")
    }
  }

}
