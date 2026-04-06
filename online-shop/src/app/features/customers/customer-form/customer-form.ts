import { Component, OnInit } from '@angular/core';
import { NgIf } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { CustomerService } from '../../../core/services/customer';

@Component({
  selector: 'app-customer-form',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule, RouterLink],
  templateUrl: './customer-form.html'
})
export class CustomerFormComponent implements OnInit {
  form: FormGroup = new FormGroup({});
  isEdit = false;
  customerId?: string;
  isLoading = false;
  isSaving = false;
  error = '';

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      LastName:  ['', [Validators.required, Validators.minLength(2)]],
      FirstName: ['', [Validators.required, Validators.minLength(2)]],
      Phone:     ['', [Validators.pattern(/^[0-9]{9,11}$/)]],
      Email:     ['', [Validators.email]],
      Address:   ['']
    });

    this.customerId = this.route.snapshot.params['id'];
    if (this.customerId) {
      this.isEdit = true;
      this.isLoading = true;
      this.customerService.getById(this.customerId).subscribe({
        next: (c) => {
          this.form.patchValue(c);
          this.isLoading = false;
        },
        error: () => {
          this.error = 'Không tìm thấy khách hàng';
          this.isLoading = false;
        }
      });
    }
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.isSaving = true;
    this.error = '';

    const payload = this.form.value;

    const action$ = this.isEdit
      ? (this.customerService.update(this.customerId!, payload) as any)
      : (this.customerService.create(payload) as any);

    action$.subscribe({
      next: () => this.router.navigate(['/customers']),
      error: (err: any) => {
        this.error = err?.error?.message ?? 'Lỗi lưu dữ liệu.';
        this.isSaving = false;
      }
    });
  }
}
