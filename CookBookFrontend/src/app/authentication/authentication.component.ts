import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticateRequestDto } from '../models/authenticateDto';
import { AccountService } from '../services/AccountService';
import { ErrorService } from '../services/errorService';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent {

  _loginForm: FormGroup;
  errorMsg: string = "";

  constructor(private authService: AccountService, private router: Router, private errorService: ErrorService) {
    this._loginForm = new FormGroup({
      "login": new FormControl('admin', [Validators.required, Validators.minLength(3)]),
      "password": new FormControl('admin', [Validators.required, Validators.minLength(3)])
    });

    this.errorService.onErrorOcured.subscribe(e => {
      console.log(e);
      this.errorMsg = e;
    });

    this.errorService.onErrorClear.subscribe(x => this.errorMsg = "")
  }

  onSubmit(): void {
    // Проверка форму на валидность
    if (this._loginForm.invalid) {
      Object.keys(this._loginForm.controls)
        .forEach(controlName => this._loginForm.controls[controlName].markAsTouched());
      return;
    }

    console.log(this._loginForm.value);

    this.authService.login(new AuthenticateRequestDto(
      this._loginForm.controls["login"].value,
      this._loginForm.controls["password"].value)).subscribe(
        (result: Boolean) => {
          if (result)
            this.router.navigateByUrl('/');
        }
      );


  }

}
