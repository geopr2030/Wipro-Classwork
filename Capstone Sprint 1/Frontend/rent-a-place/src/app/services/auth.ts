import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7054/api/auth'; // Update with your backend URL
  private readonly TOKEN_KEY = 'auth_token';

  constructor(private http: HttpClient) { }

  setToken(token: string) {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  getRole(): string | null {
    const token = this.getToken();
    if (!token) return null;
    try {
      const decoded: any = jwtDecode(token);
      return decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || null;
    } catch {
      return null;
    }
  }

  isOwner(): boolean {
    return this.getRole() === 'Owner';
  }

  isAdmin(): boolean {
    return this.getRole() === 'Admin';
  }

  logout() {
    localStorage.removeItem(this.TOKEN_KEY);
  }

  register(user: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, user);
  }

  login(credentials: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, credentials);
  }
}
