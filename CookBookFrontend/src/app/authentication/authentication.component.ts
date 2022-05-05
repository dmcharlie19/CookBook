import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticateDto } from '../models/authenticateDto';
import { AuthService } from '../services/authService';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css'],
  providers: [AuthService]
})
export class AuthenticationComponent {

  constructor(private authService: AuthService, private router: Router) {
    this._loginForm = new FormGroup({
      "login": new FormControl('123', [Validators.required, Validators.minLength(3)]),
      "password": new FormControl('123', [Validators.required, Validators.minLength(3)])
    });
  }

  _loginForm: FormGroup;

  onSubmit(): void {

    // Проверка форму на валидность
    if (this._loginForm.invalid) {
      Object.keys(this._loginForm.controls)
        .forEach(controlName => this._loginForm.controls[controlName].markAsTouched());
      return;
    }

    console.log(this._loginForm.value);

    this.authService.login(new AuthenticateDto(this._loginForm.controls["login"].value, this._loginForm.controls["password"].value)).subscribe(
      (data) => {
        console.log("Authentication successed:", data);

        this.router.navigateByUrl('/');
      },
      (error) => {
        console.log("Authentication failed", error);
      });
  }

}
