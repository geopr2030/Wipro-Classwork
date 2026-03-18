import { Component, OnInit } from '@angular/core';
import { PropertyService } from '../../services/property';

@Component({
  selector: 'app-property-listings',
  standalone: false,
  templateUrl: './property-listings.html',
  styleUrls: ['./property-listings.css']
})
export class PropertyListingsComponent implements OnInit {
  properties: any[] = [];

  constructor(private propertyService: PropertyService) { }

  ngOnInit(): void {
    this.loadProperties();
  }

  loadProperties() {
    this.propertyService.getAllProperties().subscribe(
      (data: any) => {
        this.properties = data;
      },
      (error: any) => {
        console.error('Error loading properties:', error);
      }
    );
  }
}
