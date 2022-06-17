import { Component, OnInit } from '@angular/core';
import { RecipeService } from '../services/recipeService';
import { RecipeShortInfoResponceDto } from '../models/recipe';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AccountService } from '../services/AccountService';
import { NotAtentificateComponent } from '../dialog-components/not-atentificate/not-atentificate.component';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-recipes-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.css'],
  providers: [RecipeService, MatDialog]
})
export class RecipesListComponent implements OnInit {

  // Массив рецептов
  recipes: RecipeShortInfoResponceDto[];
  private page: number = 1;
  public searchForm: FormGroup;

  constructor(private recipeService: RecipeService,
    public dialog: MatDialog,
    private router: Router,
    private accountService: AccountService) {
    this.recipes = new Array<RecipeShortInfoResponceDto>();

    this.searchForm = new FormGroup({
      "searchRequest": new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]),
    });
  }

  ngOnInit(): void {
    this.loadRecipes();
  }

  // получаем данные через сервис
  private loadRecipes() {
    this.recipeService.getRecipes(this.page).subscribe(
      (data: RecipeShortInfoResponceDto[]) => {
        this.recipes.push(...data);
        this.page++;
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

  onSearchClick() {
    // Проверка форму на валидность
    if (this.searchForm.invalid) {
      Object.keys(this.searchForm.controls)
        .forEach(controlName => this.searchForm.controls[controlName].markAsTouched());
      return;
    }

    this.recipes = new Array<RecipeShortInfoResponceDto>();
    this.recipeService.searchRecipe(this.searchForm.controls["searchRequest"].value).subscribe(
      (data: RecipeShortInfoResponceDto[]) => {
        this.recipes.push(...data);
      });
  }

  onLoadMoreClick() {
    this.loadRecipes();
  }
}
