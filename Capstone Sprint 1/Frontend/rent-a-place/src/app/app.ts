import { Component, signal } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './services/auth';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('rent-a-place');

  constructor(public auth: AuthService, private router: Router) { }

  logout() {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}
