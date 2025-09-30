import { Component, effect, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { LoginModel } from '../../../models/login.model';
import { AuthService } from '../../../services/authService';

@Component({
  selector: 'app-navbar',
  imports: [
    RouterLink
  ],
  templateUrl: './navbar.html',
})
export class Navbar {

  readonly #router = inject(Router)
  readonly #authService = inject(AuthService)
  readonly userName = this.#authService.getUserName()

  signOut() {
    localStorage.removeItem("token")
    this.#router.navigateByUrl("/")
  }

}
