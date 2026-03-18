import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PropertyService {
  private apiUrl = 'https://localhost:7054/api/property'; // Update with your backend URL

  constructor(private http: HttpClient) { }

  getAllProperties(): Observable<any> {
    return this.http.get(`${this.apiUrl}/`);
  }

  getPropertyById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  createProperty(property: any): Observable<any> {
    return this.http.post(this.apiUrl, property);
  }

  getMyListings(): Observable<any> {
    return this.http.get(`${this.apiUrl}/my-listings`);
  }

  deleteProperty(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
