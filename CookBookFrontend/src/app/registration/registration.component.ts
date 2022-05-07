import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {

  _registrationForm: FormGroup;

  constructor() {
    this._registrationForm = new FormGroup({
      "name": new FormControl('', [Validators.required, Validators.minLength(3)]),
      "login": new FormControl('', [Validators.required, Validators.minLength(3)]),
      "password": new FormControl('', [Validators.required, Validators.minLength(5)]),
      "confirmPassword": new FormControl('', [Validators.required, Validators.minLength(5)])
    },
      this.passwordsValidator()
    );
  }

  public passwordsValidator(): ValidatorFn {
    return (group: FormGroup): ValidationErrors => {
      const control1 = group.controls['password'];
      const control2 = group.controls['confirmPassword'];

      if (control1.value !== control2.value) {
        control2.setErrors({ notEqual: true });
      }
      return;
    };
  }

  showPasswordError(): String {
    const password = this._registrationForm.controls["password"]
    if (password.invalid && password.touched) {
      return "Некорректный пароль";
    }

    const confirmPassword = this._registrationForm.controls["confirmPassword"]
    if (confirmPassword.invalid && confirmPassword.touched) {
      if (confirmPassword.errors["notEqual"]) {
        return "Пароли не совпадают"
      }
      else
        return "Некорректный пароль";
    }
    return "";
  }

  onSubmit(): void {

    // Проверка форму на валидность
    if (this._registrationForm.invalid) {
      Object.keys(this._registrationForm.controls)
        .forEach(controlName => this._registrationForm.controls[controlName].markAsTouched());
      return;
    }

    console.log(this._registrationForm.value);

    // TODO: Взаимодействие с сервисом
  }

}
