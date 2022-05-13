import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { AddRecipeRequestDto, RecipeIngridient } from '../models/recipe';

class RecipeIngridientKeys {
  titleKey: string;
  bodyKey: string;
}

@Component({
  selector: 'app-add-recipe',
  templateUrl: './add-recipe.component.html',
  styleUrls: ['./add-recipe.component.css']
})
export class AddRecipeComponent implements OnInit {

  addRecipeForm: FormGroup;
  public recipeStepsKeys: Array<string> = [];
  public ingridientsKeys: RecipeIngridientKeys[] = [];

  constructor() {
  }

  ngOnInit(): void {
    this.addRecipeForm = new FormGroup({
      "title": new FormControl('борщ', [Validators.required, Validators.minLength(3)]),
      "shortDescription": new FormControl('вкусный', [Validators.required, Validators.minLength(3)]),
      "preparingTime": new FormControl('55', [Validators.required, Validators.pattern("[0-200]"), this.numberdValidator]),
      "personCount": new FormControl('5', [Validators.required, Validators.pattern("[0-9]"), this.numberdValidator])
    });

    this.addNewIngridient();
    this.addNewRecipeStep();
  }

  private numberdValidator(control: FormControl): ValidationErrors {
    console.log("numberdValidator");

    // Проверка на содержание цифр
    const n = parseInt(control.value);

    if (!n) {
      return { invalidNumber: true };
    }
    if (n > 200)
      return { invalidNumber: true };

    return null;

  }

  // Работа с шагами приготовления
  addNewRecipeStep(): void {
    let key = 'recipeStep_' + this.recipeStepsKeys.length.toString();
    console.log(key);

    this.addRecipeForm.addControl(key, new FormControl("сделать вкусно красиво", Validators.required))
    this.recipeStepsKeys.push(key);
  }

  deleteRecipeStep(id: number) {
    if (this.recipeStepsKeys.length > 1) {
      console.log("remove " + this.recipeStepsKeys[id]);

      this.addRecipeForm.removeControl(this.recipeStepsKeys[id])
      this.recipeStepsKeys.splice(id, 1);
    }
  }

  // Работа с ингридиентами
  addNewIngridient(): void {

    let keys: RecipeIngridientKeys = new RecipeIngridientKeys();
    keys.titleKey = 'recipeIngridientTitle_' + this.ingridientsKeys.length.toString();
    keys.bodyKey = 'recipeIngridientBody_' + this.ingridientsKeys.length.toString();
    console.log(keys);

    this.ingridientsKeys.push(keys);
    this.addRecipeForm.addControl(keys.titleKey, new FormControl("тесто", Validators.required))
    this.addRecipeForm.addControl(keys.bodyKey, new FormControl("капуста", Validators.required))
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

    console.log(this.addRecipeForm.value);

    let request = new AddRecipeRequestDto();
    request.title = this.addRecipeForm.controls["title"].value;
    request.shortDescription = this.addRecipeForm.controls["shortDescription"].value;
    request.preparingTime = this.addRecipeForm.controls["preparingTime"].value;
    request.personCount = this.addRecipeForm.controls["personCount"].value;

    for (let i = 0; i < this.ingridientsKeys.length; i++) {
      let ingridient = new RecipeIngridient();
      ingridient.ingridientTitle = this.addRecipeForm.controls[this.ingridientsKeys[i].titleKey].value;
      ingridient.ingridientBody = this.addRecipeForm.controls[this.ingridientsKeys[i].bodyKey].value;

      request.recipeIngridients.push(ingridient);
    }

    for (let i = 0; i < this.recipeStepsKeys.length; i++) {
      request.cookingSteps.push(this.addRecipeForm.controls[this.recipeStepsKeys[i]].value);
    }

    console.log(request);
  }

}
