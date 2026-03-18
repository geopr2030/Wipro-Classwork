import { Component } from '@angular/core';
import { PropertyService } from '../../services/property';

@Component({
  selector: 'app-create-property',
  standalone: false,
  templateUrl: './create-property.html',
  styleUrls: ['./create-property.css']
})
export class CreatePropertyComponent {
  title: string = '';
  description: string = '';
  location: string = '';
  propertyType: string = '';
  pricePerNight: number | null = null;
  features: string[] = [];

  constructor(private propertyService: PropertyService) { }

  onCreateProperty() {
    const property = {
      title: this.title,
      description: this.description,
      location: this.location,
      propertyType: this.propertyType,
      pricePerNight: this.pricePerNight,
      features: this.features.join(',')
    };

    this.propertyService.createProperty(property).subscribe(
      (response: any) => {
        console.log('Property created successfully:', response);
        // Add logic to redirect or update UI
      },
      (error: any) => {
        console.error('Error creating property:', error);
      }
    );
  }
}
