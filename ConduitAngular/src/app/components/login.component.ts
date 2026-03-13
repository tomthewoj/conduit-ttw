import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html'
})
export class LoginComponent {
  username = '';
  password = '';
  errorMessage = '';
  successMessage = '';
  isLoggedIn = false;

  constructor(private auth: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.auth.isLoggedIn$.subscribe(state => this.isLoggedIn = state);
    this.username = this.auth.getUsername() ?? '';
  }
  onLogin() {
    this.auth.login(this.username, this.password).subscribe({
      next: () => {
        this.successMessage = "Logged in successfully";
        this.errorMessage = '';
      },
      error: () => {
        this.errorMessage = 'Invalid username or password';
        this.successMessage = '';
      }
    });
  }

  onLogout() {
    this.auth.logout();
    this.successMessage = "Logged out successfully";
    this.errorMessage = '';
  }
}
