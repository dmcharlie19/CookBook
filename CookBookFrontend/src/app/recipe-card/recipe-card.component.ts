import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { RecipeShortInfoResponceDto } from '../models/recipe';

@Component({
  selector: 'app-recipe-card',
  templateUrl: './recipe-card.component.html',
  styleUrls: ['./recipe-card.component.css']
})
export class RecipeCardComponent {
  @Input() recipe: RecipeShortInfoResponceDto;
  @Input() showTitle: Boolean;
  constructor(private router: Router) {
    this.showTitle = true;
  }

  onRecipeDetail(): void {
    this.router.navigate(["/recipeDetail", this.recipe.id])
  }
}
