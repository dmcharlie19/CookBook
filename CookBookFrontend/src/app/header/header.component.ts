import { Component, Input } from '@angular/core';
import { AuthService } from '../services/authService';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  providers: [AuthService]
})
export class HeaderComponent {
  @Input() userName: string = "";

  @Input() isMain: Boolean;
  @Input() isRecipesList: Boolean;
  @Input() isFavorite: Boolean;
  constructor(public authServise: AuthService) {
  }

}
