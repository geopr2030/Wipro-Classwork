import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { CreatePropertyComponent } from './create-property';

describe('CreatePropertyComponent', () => {
  let component: CreatePropertyComponent;
  let fixture: ComponentFixture<CreatePropertyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreatePropertyComponent],
      imports: [HttpClientTestingModule, FormsModule, RouterTestingModule]
    }).compileComponents();

    fixture = TestBed.createComponent(CreatePropertyComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
