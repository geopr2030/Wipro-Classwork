import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PropertyService } from '../../services/property';
import { ReservationService } from '../../services/reservation';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-property-details',
  standalone: false,
  templateUrl: './property-details.html',
  styleUrls: ['./property-details.css']
})
export class PropertyDetailsComponent implements OnInit {
  property: any;
  checkInDate: string = '';
  checkOutDate: string = '';
  bookingMessage: string = '';
  isError: boolean = false;
  isBooking: boolean = false;

  constructor(
    private route: ActivatedRoute, 
    private propertyService: PropertyService,
    private reservationService: ReservationService,
    public auth: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const propertyId = +this.route.snapshot.paramMap.get('id')!;
    this.loadPropertyDetails(propertyId);
  }

  loadPropertyDetails(propertyId: number) {
    this.propertyService.getPropertyById(propertyId).subscribe(
      (data: any) => {
        this.property = data;
      },
      (error: any) => {
        console.error('Error loading property details:', error);
      }
    );
  }

  onBookNow() {
    if (!this.auth.isLoggedIn()) {
      this.router.navigate(['/login']);
      return;
    }

    if (!this.checkInDate || !this.checkOutDate) {
      this.showMessage('Please select both check-in and check-out dates.', true);
      return;
    }

    if (new Date(this.checkInDate) >= new Date(this.checkOutDate)) {
      this.showMessage('Check-out date must be after check-in date.', true);
      return;
    }

    this.isBooking = true;
    const reservation = {
      propertyId: this.property.propertyId,
      checkInDate: this.checkInDate,
      checkOutDate: this.checkOutDate
    };

    this.reservationService.createReservation(reservation).subscribe({
      next: () => {
        this.isBooking = false;
        this.showMessage('Booking successful! View it in My Bookings.', false);
        this.checkInDate = '';
        this.checkOutDate = '';
      },
      error: (err) => {
        this.isBooking = false;
        this.showMessage(err.error?.message || 'Failed to create booking.', true);
      }
    });
  }

  private showMessage(msg: string, isError: boolean) {
    this.bookingMessage = msg;
    this.isError = isError;
    setTimeout(() => this.bookingMessage = '', 5000);
  }
}
