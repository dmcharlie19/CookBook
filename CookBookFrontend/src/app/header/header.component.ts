import { Component, Input } from '@angular/core';
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
  constructor(public authServise: AccountService) {

    this.userName = this.authServise.UserName;
  }

}
