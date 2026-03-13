import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { loginResponse } from '../models/loginResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7255/api/users';
  private loggedIn; //make this observable for reuse
  isLoggedIn$;
  constructor(private http: HttpClient)
  {
    const token = localStorage.getItem('token');
    this.loggedIn = new BehaviorSubject<boolean>(!!token);
    this.isLoggedIn$ = this.loggedIn.asObservable();
  }

  login(username: string, password: string): Observable<loginResponse> {
    return this.http.post<loginResponse>(`${this.apiUrl}/login`, { username, password }).pipe(
      tap(response => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('username', username);
        this.loggedIn.next(true);
      })
    );
  }
  register(username: string, email: string, password: string): Observable<any> {
    return this.http.post(this.apiUrl, { username, password, email });
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
  isLoggedIn(): boolean {
    return !!this.getToken();
  }
  getUsername(): string | null {
    return localStorage.getItem('username');
  }
  getCurrentUser() {
    return {
      isLoggedIn: this.isLoggedIn(),
      username: this.getUsername()
    };
  }
  logout() {
    this.loggedIn.next(false);
    localStorage.removeItem('token');
    localStorage.removeItem('username');
  }
}
