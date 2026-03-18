import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { PropertyListingsComponent } from './property-listings';

describe('PropertyListingsComponent', () => {
  let component: PropertyListingsComponent;
  let fixture: ComponentFixture<PropertyListingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PropertyListingsComponent],
      imports: [HttpClientTestingModule, RouterTestingModule]
    }).compileComponents();

    fixture = TestBed.createComponent(PropertyListingsComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
