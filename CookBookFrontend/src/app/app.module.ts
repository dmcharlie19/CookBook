import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { RecipesListComponent } from './recipes-list/recipes-list.component';
import { RouterModule, Routes } from '@angular/router';

// определение маршрутов
const appRoutes: Routes = [
  { path: '', component: RecipesListComponent },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes)],
  declarations: [AppComponent, RecipesListComponent],
  bootstrap: [AppComponent]
})

export class AppModule { }
