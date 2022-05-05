import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { RecipesListComponent } from './recipes-list/recipes-list.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationComponent } from './authentication/authentication.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { RecipeCardComponent } from './recipe-card/recipe-card.component';

// определение маршрутов
const appRoutes: Routes = [
  { path: '', component: RecipesListComponent },
  { path: 'login', component: AuthenticationComponent },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes)],
  declarations: [AppComponent, AuthenticationComponent, RecipesListComponent, AuthenticationComponent, HeaderComponent, FooterComponent, RecipeCardComponent],
  bootstrap: [AppComponent]
})

export class AppModule { }
