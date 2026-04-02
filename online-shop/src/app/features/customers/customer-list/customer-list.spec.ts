import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerListComponent } from './customer-list';

describe('CustomerList', () => {
  let component: CustomerListComponent;
  let fixture: ComponentFixture<CustomerListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerListComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CustomerListComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
