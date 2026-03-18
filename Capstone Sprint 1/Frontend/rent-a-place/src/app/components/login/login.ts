import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrls: ['./login.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  isLoading: boolean = false;
  errorMessage: string = '';

  constructor(private auth: AuthService, private router: Router) { }

  onLogin() {
    this.isLoading = true;
    this.errorMessage = '';
    
    const credentials = {
      email: this.email,
      password: this.password
    };

    this.auth.login(credentials).subscribe({
      next: (response: any) => {
        this.isLoading = false;
        if (response.token) {
          this.auth.setToken(response.token);
          this.router.navigate(['/']);
        }
      },
      error: (error: any) => {
        this.isLoading = false;
        console.error('Login failed:', error);
        this.errorMessage = error.error?.message || 'Invalid email or password.';
      }
    });
  }
}
