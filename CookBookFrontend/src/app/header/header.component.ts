import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../services/AccountService';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  userName: string = "";

  @Input() isMain: Boolean;
  @Input() isRecipesList: Boolean;
  @Input() isFavorite: Boolean;

  constructor(public authServise: AccountService, private router: Router) {
    this.userName = this.authServise.getUserName();
  }

  public onMyRecipes(): void {
    this.router.navigateByUrl("/myRecipes")
  }

}
