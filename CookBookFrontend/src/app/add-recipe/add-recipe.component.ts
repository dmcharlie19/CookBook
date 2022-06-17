import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { NotAtentificateComponent } from '../dialog-components/not-atentificate/not-atentificate.component';
import { AddRecipeRequestDto, RecipeIngredient } from '../models/recipe';
import { AccountService } from '../services/AccountService';
import { NavigationService } from '../services/navigationSrvice';
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
  private imageFile: File;
  public imageUrl: String = "";
  public isImageAdded: Boolean = false;

  constructor(private recipeService: RecipeService,
    private router: Router,
    public navigation: NavigationService,
    private accountService: AccountService,
    private dialog: MatDialog) {
  }

  ngOnInit(): void {

    if (this.accountService.isLoggedOut()) {
      this.dialog.open(NotAtentificateComponent, { disableClose: true });
    }
    else {
      this.router.navigateByUrl("/addRecipe")
    }

    this.addRecipeForm = new FormGroup({
      "title": new FormControl('', [Validators.required, Validators.minLength(3)]),
      "shortDescription": new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(200)]),
      "preparingTime": new FormControl('', [Validators.required, this.numberdValidator]),
      "personCount": new FormControl('', [Validators.required, this.numberdValidator]),
      "avatar": new FormControl()
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
    this.addRecipeForm.addControl(key, new FormControl("", Validators.required))
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
    this.addRecipeForm.addControl(key, new FormControl("", Validators.required))
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
    this.addRecipeForm.addControl(keys.titleKey, new FormControl("", Validators.required))
    this.addRecipeForm.addControl(keys.bodyKey, new FormControl("", Validators.required))
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

    this.recipeService.addRecipe(request, this.imageFile).subscribe(
      () => this.router.navigateByUrl('/')
    )
  }

  showImagePreview(event) {
    this.imageFile = (event.target as HTMLInputElement).files[0];

    // File Preview
    const reader = new FileReader();
    reader.onload = () => {
      this.imageUrl = reader.result as string;
      this.isImageAdded = true;
      console.log(this.imageUrl);

    }
    reader.readAsDataURL(this.imageFile)
  }

  onDeleteImageClick(): void {
    this.isImageAdded = false;
  }

}
