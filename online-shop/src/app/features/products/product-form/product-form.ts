// product-form.ts
import { Component, OnInit } from '@angular/core';
import { NgIf } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductService } from '../../../core/services/product';
 
@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule],   // ReactiveFormsModule cho formGroup
  templateUrl: './product-form.html'
})
export class ProductFormComponent implements OnInit {
  form!: FormGroup;
  isEdit = false;
  productId?: number;
  isLoading = false;
  error = '';
 
  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private router: Router,
    private route: ActivatedRoute
  ) {}
 
  ngOnInit(): void {
    this.form = this.fb.group({
      ProductName:   ['', [Validators.required, Validators.minLength(2)]],
      Price:         [0,  [Validators.required, Validators.min(1)]],
      CategoryID:   [1,  Validators.required],
      SupplierID:   [1,  Validators.required],
      WarehouseID:  [1,  Validators.required],
      StockQuantity:[0,  [Validators.required, Validators.min(0)]],
    });
 
    // Kiểm tra có phải edit không (URL có :id)
    this.productId = this.route.snapshot.params['id'];
    if (this.productId) {
      this.isEdit = true;
      this.productService.getById(this.productId).subscribe({
        next: (p) => this.form.patchValue(p)
      });
    }
  }
 
  submit(): void {
    if (this.form.invalid) return;
    this.isLoading = true;
    const data = this.form.value;
 
    const action = this.isEdit
      ? this.productService.update(this.productId!, data)
      : this.productService.create(data);
 
    action.subscribe({
      next: () => this.router.navigate(['/products']),
      error: () => { this.error = 'Loi luu du lieu'; this.isLoading = false; }
    });
  }
}