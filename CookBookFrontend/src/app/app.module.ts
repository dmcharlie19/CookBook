import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RecipesListComponent } from './recipes-list/recipes-list.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationComponent } from './authentication/authentication.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { RecipeCardComponent } from './recipe-card/recipe-card.component';
import { RegistrationComponent } from './registration/registration.component';
import { ErrorInterceptor } from './interceptors/error-interceptor';
import { ErrorDisplayComponent } from './error-display/error-display.component'
import { AccountService } from './services/AccountService';
import { ErrorService } from './services/errorService';
import { AddRecipeComponent } from './add-recipe/add-recipe.component';
//import { RecipeDetailComponent } from './recipe-detail/recipe-detail.component';

// Определение маршрутов
const appRoutes: Routes = [
  { path: '', component: RecipesListComponent },
  { path: 'login', component: AuthenticationComponent },
  { path: 'register', component: RegistrationComponent },
  { path: 'addRecipe', component: AddRecipeComponent },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [BrowserModule, ReactiveFormsModule, HttpClientModule, RouterModule.forRoot(appRoutes)],
  declarations: [
    AppComponent,
    AuthenticationComponent,
    RecipesListComponent,
    AuthenticationComponent,
    HeaderComponent,
    FooterComponent,
    RecipeCardComponent,
    RegistrationComponent,
    ErrorDisplayComponent,
    AddRecipeComponent,
    //RecipeDetailComponent
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    AccountService,
    ErrorService
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
