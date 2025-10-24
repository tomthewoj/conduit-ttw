import { Component, signal } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})

export class App {
  protected readonly title = signal('ConduitAngular');
  constructor(private router: Router) {
    console.log('Routes:', this.router.config);
  }
}
