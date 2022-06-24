import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { NotAtentificateComponent } from '../dialog-components/not-atentificate/not-atentificate.component';
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

  private imgPath = "assets/img";
  public imageUrl: string = "";
  public likeUrl: string = "";
  public starUrl: string = "";

  constructor(
    private router: Router,
    private recipeService: RecipeService,
    private accountService: AccountService,
    public dialog: MatDialog) {
    this.showTitle = true;
  }

  ngOnInit(): void {

    this.setupIcons();

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
    if (this.accountService.isLoggedOut()) {
      this.dialog.open(NotAtentificateComponent, { disableClose: false });
      return;
    }
    this.recipeService.addLike(this.recipe.id).subscribe(() => this.updateRecipe());
  }

  onFavorite(): void {
    if (this.accountService.isLoggedOut()) {
      this.dialog.open(NotAtentificateComponent, { disableClose: false });
      return;
    }
    this.recipeService.addFavorite(this.recipe.id).subscribe(() => this.updateRecipe());
  }

  onRecipeDetail(): void {
    this.router.navigate(["/recipeDetail", this.recipe.id])
  }

  onRecipeAuthor(): void {
    this.router.navigate(["/user", this.recipe.authorId])
  }

  private setupIcons(): void {
    this.likeUrl = this.recipe.isUserLikeRecipe ? `${this.imgPath}/RedLike.png` : `${this.imgPath}/EmptyLike.png`;
    this.starUrl = this.recipe.isUserFavoriteRecipe ? `${this.imgPath}/star.png` : `${this.imgPath}/EmptyStar.png`;
  }

  private updateRecipe(): void {
    this.recipeService.getRecipeShort(this.recipe.id).subscribe((data: RecipeShortInfoResponceDto) => {
      this.recipe = data;
      this.setupIcons();
    });
  }
}
