import { Component, OnInit } from '@angular/core';
import { UserInfo } from '../models/userInfo';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.css']
})
export class UserCardComponent implements OnInit {

  userInfo: UserInfo;
  registrDateStr: string;

  constructor() {
    this.userInfo = new UserInfo();
    this.userInfo = {
      id: 1, userName: "Иван",
      userDescription: "позитивный человек, повар, любит десерты и мясо \r\n позитивный человек, повар, любит десерты и мясопозитивный человек, повар, любит десерты и мясо \r\n позитивный человек, повар, любит десерты и мясо",
      RecipesCount: 5,
      RegistrationDate: new Date()
    };
  }

  ngOnInit(): void {
    this.registrDateStr = this.userInfo.RegistrationDate.toLocaleDateString();

  }
}
