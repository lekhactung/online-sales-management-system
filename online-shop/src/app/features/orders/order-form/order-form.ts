import { Component, OnInit } from '@angular/core';
import { NgIf, NgFor, DecimalPipe } from '@angular/common';
import {
  ReactiveFormsModule, FormBuilder, FormGroup, FormArray, Validators, AbstractControl
} from '@angular/forms';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { OrderService } from '../../../core/services/order';
import { CustomerService } from '../../../core/services/customer';
import { ProductService } from '../../../core/services/product';
import { Customer } from '../../../shared/models/customer.model';
import { Product } from '../../../shared/models/product.model';
import { Order } from '../../../shared/models/order.model';

const ORDER_STATUSES = [
  { id: 'TT01', label: 'Chờ xác nhận' },
  { id: 'TT02', label: 'Đang chuẩn bị hàng' },
  { id: 'TT03', label: 'Đang giao hàng' },
  { id: 'TT04', label: 'Đã giao' },
  { id: 'TT05', label: 'Đã hủy' }
];

@Component({
  selector: 'app-order-form',
  standalone: true,
  imports: [NgIf, NgFor, DecimalPipe, ReactiveFormsModule, RouterLink],
  templateUrl: './order-form.html'
})
export class OrderFormComponent implements OnInit {
  form: FormGroup = new FormGroup({});
  isEdit = false;
  orderId?: string;
  isLoading = false;
  isSaving = false;
  error = '';

  customers: Customer[] = [];
  products: Product[] = [];
  statuses = ORDER_STATUSES;

  constructor(
    private fb: FormBuilder,
    private orderService: OrderService,
    private customerService: CustomerService,
    private productService: ProductService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      CustomerId: ['', Validators.required],
      StatusId:   ['TT01', Validators.required],
      orderDetails: this.fb.array([])
    });

    this.customerService.getAll().subscribe({
      next: (c) => { this.customers = c; },
      error: () => { this.error = 'Không tải được danh sách khách hàng.'; }
    });

    this.productService.getAll().subscribe({
      next: (p) => { this.products = p; },
      error: () => { this.error = 'Không tải được danh sách sản phẩm.'; }
    });

    this.orderId = this.route.snapshot.params['id'];
    if (this.orderId) {
      this.isEdit = true;
      this.isLoading = true;
      this.orderService.getById(this.orderId).subscribe({
        next: (order: Order) => {
          this.form.patchValue({
            CustomerId: order.CustomerId,
            StatusId: order.StatusId
          });
          (order.OrderDetails ?? []).forEach(d => {
            this.orderDetails.push(this.createDetailRow(d.ProductId, d.Quantity, d.UnitPrice));
          });
          if (this.orderDetails.length === 0) this.addLine();
          this.isLoading = false;
        },
        error: () => {
          this.error = 'Không tìm thấy đơn hàng.';
          this.isLoading = false;
        }
      });
    } else {
      this.addLine();
    }
  }

  get orderDetails(): FormArray {
    return this.form.get('orderDetails') as FormArray;
  }

  createDetailRow(productId = '', quantity = 1, unitPrice = 0): FormGroup {
    return this.fb.group({
      ProductId: [productId, Validators.required],
      Quantity:  [quantity,  [Validators.required, Validators.min(1)]],
      UnitPrice: [unitPrice, [Validators.required, Validators.min(0)]]
    });
  }

  addLine(): void {
    this.orderDetails.push(this.createDetailRow());
  }

  removeLine(i: number): void {
    this.orderDetails.removeAt(i);
  }

  onProductChange(i: number): void {
    const row = this.orderDetails.at(i) as FormGroup;
    const pid = row.get('ProductId')?.value;
    const product = this.products.find(p => p.ProductId === pid);
    if (product) {
      row.get('UnitPrice')?.setValue(product.Price);
    }
  }

  getRowTotal(ctrl: AbstractControl): number {
    const row = ctrl as FormGroup;
    return (row.get('Quantity')?.value || 0) * (row.get('UnitPrice')?.value || 0);
  }

  get grandTotal(): number {
    return this.orderDetails.controls.reduce((s, c) => s + this.getRowTotal(c), 0);
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.isSaving = true;
    this.error = '';

    const val = this.form.value;
    const payload = {
      CustomerId:   val.CustomerId,
      StatusId:     val.StatusId,
      OrderDetails: val.orderDetails.map((d: any) => ({
        ProductId: d.ProductId,
        Quantity:  Number(d.Quantity),
        UnitPrice: Number(d.UnitPrice)
      }))
    };

    const action$ = this.isEdit
      ? (this.orderService.update(this.orderId!, payload) as any)
      : (this.orderService.create(payload as any) as any);

    action$.subscribe({
      next: () => this.router.navigate(['/orders']),
      error: (err: any) => {
        this.error = err?.error?.message ?? 'Lỗi lưu dữ liệu.';
        this.isSaving = false;
      }
    });
  }

  getProductName(pid: string): string {
    return this.products.find(p => p.ProductId === pid)?.ProductName ?? pid;
  }
}
