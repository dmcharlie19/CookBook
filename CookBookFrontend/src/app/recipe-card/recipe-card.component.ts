import { Component, Input } from '@angular/core';
import { RecipeShortInfoResponceDto } from '../models/recipe';

@Component({
  selector: 'app-recipe-card',
  templateUrl: './recipe-card.component.html',
  styleUrls: ['./recipe-card.component.css']
})
export class RecipeCardComponent {
  @Input() recipe: RecipeShortInfoResponceDto;
  constructor() { }

}
