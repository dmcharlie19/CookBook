import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RecipeShortInfoResponceDto } from '../models/recipe';
import { AccountService } from '../services/AccountService';
import { RecipeService } from '../services/recipeService';

@Component({
  selector: 'app-recipe-card',
  templateUrl: './recipe-card.component.html',
  styleUrls: ['./recipe-card.component.css']
})
export class RecipeCardComponent implements OnInit {
  @Input() recipe: RecipeShortInfoResponceDto;
  @Input() showTitle: Boolean;

  public imageUrl: string = "";

  constructor(
    private router: Router,
    private recipeService: RecipeService,
    private accountService: AccountService) {
    this.showTitle = true;
  }

  ngOnInit(): void {

    // Загрузка изображения
    this.recipeService.getRecipeImage(this.recipe.id).subscribe(data => {
      this.createImageFromBlob(data);
    })
  }

  createImageFromBlob(imageFile: Blob) {
    const reader = new FileReader();
    reader.onload = () => {
      this.imageUrl = reader.result as string;
      if (this.imageUrl.length < 6) {
        this.imageUrl = "assets/img/defaultRecipeImg.jpg"
      }
    }
    reader.readAsDataURL(imageFile)
  }

  onLike(): void {
    this.recipeService.addLike(this.recipe.id).subscribe(() => this.updateRecipe());
  }

  onFavorite(): void {
    this.recipeService.addFavorite(this.recipe.id).subscribe(() => this.updateRecipe());
  }


  onRecipeDetail(): void {
    this.router.navigate(["/recipeDetail", this.recipe.id])
  }

  onRecipeAuthor(): void {
    this.router.navigate(["/user", this.recipe.authorId])
  }

  private updateRecipe(): void {
    this.recipeService.getRecipeShort(this.recipe.id).subscribe((data: RecipeShortInfoResponceDto) => this.recipe = data);
  }
}
