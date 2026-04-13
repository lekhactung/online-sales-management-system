import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminList } from './admin-list';

describe('AdminList', () => {
  let component: AdminList;
  let fixture: ComponentFixture<AdminList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminList],
    }).compileComponents();

    fixture = TestBed.createComponent(AdminList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
