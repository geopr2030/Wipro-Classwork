import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../../services/reservation';

@Component({
  selector: 'app-my-reservations',
  standalone: false,
  templateUrl: './my-reservations.component.html',
  styleUrls: ['./my-reservations.component.css']
})
export class MyReservationsComponent implements OnInit {
  reservations: any[] = [];
  isLoading = true;
  error = '';

  constructor(private reservationService: ReservationService) {}

  ngOnInit(): void {
    this.loadReservations();
  }

  loadReservations() {
    this.isLoading = true;
    this.reservationService.getMyReservations().subscribe({
      next: (data) => {
        this.reservations = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Failed to load reservations', err);
        this.error = 'Failed to load your reservations. Please try again.';
        this.isLoading = false;
      }
    });
  }

  cancelReservation(id: number) {
    if (confirm('Are you sure you want to cancel this reservation?')) {
      this.reservationService.cancelReservation(id).subscribe({
        next: () => {
          this.loadReservations(); // reload
        },
        error: (err) => {
          alert(err.error?.message || 'Failed to cancel reservation.');
        }
      });
    }
  }
}
