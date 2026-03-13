import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-registter',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './registration.component.html'
})
export class RegistrationComponent {
  username = '';
  password = '';
  email = '';
  successMessage = ''
  errorMessage = ''
  isRegistered = false;

  constructor(private auth: AuthService) { }

  onRegister(): void {
    this.auth.register(this.username, this.email, this.password).subscribe({
      next: (response) => {
        localStorage.setItem('token', response.token);
        this.successMessage = "Registered successfully";
        this.errorMessage = '';
        this.isRegistered = true;
      },
      error: () => {
        this.errorMessage = 'Error registering the user';
        this.successMessage = '';
      }
    });
  }
}
