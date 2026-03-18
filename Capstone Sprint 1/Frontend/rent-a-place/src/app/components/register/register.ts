import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.html',
  styleUrls: ['./register.css']
})
export class RegisterComponent {
  fullName: string = '';
  email: string = '';
  password: string = '';
  role: string = 'Renter';
  phoneNumber: string = '';

  constructor(private authService: AuthService, private router: Router) { }

  onRegister() {
    const user = {
      fullName: this.fullName,
      email: this.email,
      password: this.password,
      role: this.role,
      phoneNumber: this.phoneNumber
    };

    this.authService.register(user).subscribe(
      (response: any) => {
        console.log('Registration successful:', response);
        this.router.navigate(['/login']);
      },
      (error: any) => {
        console.error('Registration failed:', error);
      }
    );
  }
}
