import { Component, Input } from '@angular/core';
import { Recipe } from '../models/recipe';

@Component({
  selector: 'app-recipe-card',
  templateUrl: './recipe-card.component.html',
  styleUrls: [
    '../common-styles.css',
    './recipe-card.component.css']
})
export class RecipeCardComponent {
  @Input() recipe: Recipe;
  constructor() { }

}
