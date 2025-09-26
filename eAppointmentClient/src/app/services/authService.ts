import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { TokenModel } from '../models/token.model';
import { jwtDecode, JwtPayload } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  constructor(
    private router: Router
  ) {}

  tokenDecode: TokenModel = new TokenModel()
  
  isAuthenticated() {

    const token: string | null = localStorage.getItem("token")
    if(token) {

      const decode: JwtPayload | any = jwtDecode(token)

      this.tokenDecode.id = decode["user-id"]
      this.tokenDecode.name = decode["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
      this.tokenDecode.email = decode["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"]
      this.tokenDecode.userName = decode["UserName"]

      const expires = decode.exp;
      const now = new Date().getTime() / 1000;

      if(now > expires) {

        localStorage.removeItem("token")
        this.router.navigateByUrl("/")
        return false

      }

      return true

    }

    this.router.navigateByUrl("/login")
    return false;

  }

}
