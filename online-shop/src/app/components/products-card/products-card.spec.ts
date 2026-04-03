import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductsCard } from './products-card';

describe('ProductsCard', () => {
  let component: ProductsCard;
  let fixture: ComponentFixture<ProductsCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductsCard],
    }).compileComponents();

    fixture = TestBed.createComponent(ProductsCard);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
