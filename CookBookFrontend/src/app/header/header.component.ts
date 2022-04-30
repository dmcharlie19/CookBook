import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: [
    '../common-styles.css',
    './header.component.css']
})
export class HeaderComponent {
  @Input() userName: string = "";

  @Input() isMain: Boolean;
  @Input() isRecipesList: Boolean;
  @Input() isFavorite: Boolean;
  constructor() { }

}
