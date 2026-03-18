import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  private apiUrl = 'https://localhost:7054/api/reservation';

  constructor(private http: HttpClient) { }

  createReservation(reservation: any): Observable<any> {
    return this.http.post(this.apiUrl, reservation);
  }

  getMyReservations(): Observable<any> {
    return this.http.get(`${this.apiUrl}/my-reservations`);
  }

  cancelReservation(id: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}/cancel`, {});
  }

  getPropertyReservations(propertyId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/property/${propertyId}`);
  }

  confirmReservation(id: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}/confirm`, {});
  }
}
