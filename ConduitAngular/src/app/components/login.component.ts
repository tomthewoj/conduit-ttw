import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { GetUserService } from '../services/getuser.service';
import { FormsModule } from '@angular/forms';
import { NgIf, NgFor } from '@angular/common';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, NgIf, NgFor],
  templateUrl: './login.component.html'
})
export class LoginComponent {
  username = '';
  password = '';
  errorMessage = '';
  successMessage = '';
  usernames: string[] = [];

  constructor(private auth: AuthService, private userService: GetUserService) { }

  onLogin() {
    this.auth.login(this.username, this.password).subscribe({
      next: (response) => {
        localStorage.setItem('token', response.token);
        this.successMessage = "Logged in successfully";
        console.log('Logged in successfully');
      },
      error: (err) => {
        this.errorMessage = 'Invalid username or password';
        console.error(err);
      }
    });
  }

  getUsernames() {
    this.userService.getAllUsernames().subscribe({
      next: (response) => {
        this.usernames = response;
      },
      error: (err) => {
        console.error('Failed to fetch usernames', err);
        this.errorMessage = 'Failed to fetch usernames. Make sure you are logged in.';
      }
    });

  }
  logout() {
    if (localStorage.getItem('token')) {
    localStorage.removeItem('token');
    this.successMessage = "Logged out successfully";
    console.log('Logged out successfully');
    }
    this.usernames = [];
    this.errorMessage = '';
  }
}
