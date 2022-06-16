import { HttpClient } from '@angular/common/http';
import { Token } from '../models/token';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { API_URL } from '../app-injection-tokens';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http: HttpClient,
    @Inject(API_URL) private apiUrl: string,
    private jwtHelper: JwtHelperService,
    private router: Router
  ) { }

  logIn(login: string, password: string): Observable<Token> {
    return this.http.post<Token>(`${this.apiUrl}api/account/login`, {
      login, password
    }).pipe(
      tap((token) => {
        localStorage.setItem('accessToken', token.accessToken),
        localStorage.setItem('role', token.role)
      })
    )
  }

  isAuthenticated(): string | boolean | null {
    var token = localStorage.getItem('accessToken');
    return token && !this.jwtHelper.isTokenExpired(token);
  }

  isAdmin(){
    return localStorage.getItem('role');
  }

  logOut(): void {
    localStorage.removeItem('accessToken');
    this.router.navigate(['']);
  }
}
