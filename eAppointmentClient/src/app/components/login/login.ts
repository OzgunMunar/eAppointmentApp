import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { LoginModel } from '../../models/login.model';
import { FormValidateDirective } from 'form-validate-angular';
import { HttpService } from '../../services/httpService';
import { LoginResponseModel } from '../../models/login-response.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [
    CommonModule, 
    FormsModule,
    FormValidateDirective
  ],
  templateUrl: './login.html',
})

export default class Login {

  constructor(
    private http: HttpService,
    private router: Router
  ) {}

  loginModel: LoginModel = new LoginModel()

  @ViewChild('passwordField') password?: ElementRef<HTMLInputElement>

  isPasswordVisible = false

  showHideOrShow() {

    if(!this.password) return

    this.isPasswordVisible = !this.isPasswordVisible
    this.password.nativeElement.type = this.isPasswordVisible ? "text" : "password"

  }

  signIn(form: NgForm) {

    if(form.valid) {
      
      this.http.post<LoginResponseModel>("Auth/Login", this.loginModel, (res) => {
        
        if(res.data !== undefined) {
          
          localStorage.setItem("token", res.data?.accessToken)
          this.router.navigateByUrl("/")

        }


      })

    }

  }

}
