import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserInfo } from '../models/userInfo';
import { AccountService } from '../services/AccountService';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.css']
})
export class UserCardComponent implements OnInit {

  public userInfo: UserInfo = null;;
  public registrDateStr: string = "";
  private userId: number;

  constructor(
    private route: ActivatedRoute,
    private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.userId = this.route.snapshot.params['id'];
    this.accountService.getUserInfo(this.userId).subscribe(
      (data: UserInfo) => {
        this.userInfo = data;
        this.registrDateStr = new Date(this.userInfo.registrationDate).toLocaleDateString();
      });

  }
}
