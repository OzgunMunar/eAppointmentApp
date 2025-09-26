import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-navbar',
  imports: [
    RouterLink
  ],
  templateUrl: './navbar.html',
})
export class Navbar {

  constructor(
    private router: Router
  ){}
  
  signOut() {
    localStorage.removeItem("token")
    this.router.navigateByUrl("/")
  }

}
