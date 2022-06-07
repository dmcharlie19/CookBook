import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AddRecipeRequestDto, RecipeIngredient } from '../models/recipe';
import { RecipeService } from '../services/recipeService';

class RecipeIngridientKeys {
  titleKey: string;
  bodyKey: string;
}

@Component({
  selector: 'app-add-recipe',
  templateUrl: './add-recipe.component.html',
  styleUrls: ['./add-recipe.component.css'],
  providers: [RecipeService]
})
export class AddRecipeComponent implements OnInit {

  addRecipeForm: FormGroup;
  public recipeTagsKeys: Array<string> = [];
  public recipeStepsKeys: Array<string> = [];
  public ingridientsKeys: RecipeIngridientKeys[] = [];

  constructor(private recipeService: RecipeService, private router: Router) {
  }

  ngOnInit(): void {
    this.addRecipeForm = new FormGroup({
      "title": new FormControl('борщ', [Validators.required, Validators.minLength(3)]),
      "shortDescription": new FormControl('вкусный', [Validators.required, Validators.minLength(3)]),
      "preparingTime": new FormControl('30', [Validators.required, this.numberdValidator]),
      "personCount": new FormControl('4', [Validators.required, this.numberdValidator])
    });

    this.addNewTag();
    this.addNewIngridient();
    this.addNewRecipeStep();
  }

  private numberdValidator(control: FormControl): ValidationErrors {
    const n = parseInt(control.value);

    if (!n || n > 200) {
      return { invalidNumber: true };
    }
    return null;
  }

  // Работа с тэгами
  addNewTag(): void {
    let key = 'recipeStep_' + this.recipeTagsKeys.length.toString();
    this.addRecipeForm.addControl(key, new FormControl("вкусно", Validators.required))
    this.recipeTagsKeys.push(key);
  }

  deleteTag(id: number) {
    if (this.recipeTagsKeys.length > 1) {
      this.addRecipeForm.removeControl(this.recipeTagsKeys[id])
      this.recipeTagsKeys.splice(id, 1);
    }
  }

  // Работа с шагами приготовления
  addNewRecipeStep(): void {
    let key = 'tag_' + this.recipeStepsKeys.length.toString();
    this.addRecipeForm.addControl(key, new FormControl("сделать вкусно красиво", Validators.required))
    this.recipeStepsKeys.push(key);
  }

  deleteRecipeStep(id: number) {
    if (this.recipeStepsKeys.length > 1) {
      this.addRecipeForm.removeControl(this.recipeStepsKeys[id])
      this.recipeStepsKeys.splice(id, 1);
    }
  }

  // Работа с ингридиентами
  addNewIngridient(): void {

    let keys: RecipeIngridientKeys = new RecipeIngridientKeys();
    keys.titleKey = 'recipeIngridientTitle_' + this.ingridientsKeys.length.toString();
    keys.bodyKey = 'recipeIngridientBody_' + this.ingridientsKeys.length.toString();

    this.ingridientsKeys.push(keys);
    this.addRecipeForm.addControl(keys.titleKey, new FormControl("тесто", Validators.required))
    this.addRecipeForm.addControl(keys.bodyKey, new FormControl("капуста\r\nмясо\r\nрыба", Validators.required))
  }

  deleteIngridient(id: number): void {
    if (this.ingridientsKeys.length > 1) {
      this.addRecipeForm.removeControl(this.ingridientsKeys[id].titleKey)
      this.addRecipeForm.removeControl(this.ingridientsKeys[id].bodyKey)
      this.ingridientsKeys.splice(id, 1);
    }
  }

  publishRecipe(): void {

    // Проверка форму на валидность
    if (this.addRecipeForm.invalid) {
      Object.keys(this.addRecipeForm.controls)
        .forEach(controlName => this.addRecipeForm.controls[controlName].markAsTouched());
      return;
    }

    let request = new AddRecipeRequestDto();
    request.title = this.addRecipeForm.controls["title"].value;
    request.shortDescription = this.addRecipeForm.controls["shortDescription"].value;
    request.preparingTime = this.addRecipeForm.controls["preparingTime"].value;
    request.personCount = this.addRecipeForm.controls["personCount"].value;

    for (let i = 0; i < this.recipeTagsKeys.length; i++) {
      request.tags.push(this.addRecipeForm.controls[this.recipeTagsKeys[i]].value);
    }

    for (let i = 0; i < this.ingridientsKeys.length; i++) {
      let ingredient = new RecipeIngredient();
      ingredient.title = this.addRecipeForm.controls[this.ingridientsKeys[i].titleKey].value;
      ingredient.ingredients = this.addRecipeForm.controls[this.ingridientsKeys[i].bodyKey].value.split(/\r|\n/);
      ingredient.ingredients = ingredient.ingredients.filter(function (entry) { return entry.trim() != ''; });

      request.recipeIngridients.push(ingredient);
    }

    for (let i = 0; i < this.recipeStepsKeys.length; i++) {
      request.cookingSteps.push(this.addRecipeForm.controls[this.recipeStepsKeys[i]].value);
    }

    this.recipeService.addRecipe(request).subscribe(
      () => this.router.navigateByUrl('/')
    )
  }

}
