import { Component } from '@angular/core';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.html',
  styleUrls: ['./home.css'],
})
export class HomeComponent {
  constructor(public auth: AuthService) {}
}
