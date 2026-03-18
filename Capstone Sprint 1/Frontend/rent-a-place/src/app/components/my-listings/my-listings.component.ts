import { Component, OnInit } from '@angular/core';
import { PropertyService } from '../../services/property';

@Component({
  selector: 'app-my-listings',
  standalone: false,
  templateUrl: './my-listings.component.html',
  styleUrls: ['../my-reservations/my-reservations.component.css'] // re-using dashboard styles
})
export class MyListingsComponent implements OnInit {
  properties: any[] = [];
  isLoading = true;
  error = '';

  constructor(private propertyService: PropertyService) {}

  ngOnInit(): void {
    this.loadListings();
  }

  loadListings() {
    this.isLoading = true;
    this.propertyService.getMyListings().subscribe({
      next: (data) => {
        this.properties = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Failed to load listings', err);
        this.error = 'Failed to load your properties. Please try again.';
        this.isLoading = false;
      }
    });
  }

  deleteListing(id: number) {
    if (confirm('Are you sure you want to delete this property?')) {
      this.propertyService.deleteProperty(id).subscribe({
        next: () => {
          this.loadListings(); // reload
        },
        error: (err) => {
          alert(err.error?.message || 'Failed to delete property.');
        }
      });
    }
  }
}
