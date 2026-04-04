import { Component, OnInit } from '@angular/core';
import { NgIf, NgFor } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { ProductService } from '../../../core/services/product';
import { ProductCategoryService } from '../../../core/services/product-category';
import { ProductCategory } from '../../../shared/models/product-category.model';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [NgIf, NgFor, ReactiveFormsModule, RouterLink],
  templateUrl: './product-form.html'
})
export class ProductFormComponent implements OnInit {
  form: FormGroup = new FormGroup({});
  isEdit = false;
  productId?: string;
  isLoading = false;
  isSaving = false;
  error = '';
  categories: ProductCategory[] = [];

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private categoryService: ProductCategoryService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      ProductName:   ['', [Validators.required, Validators.minLength(2)]],
      Price:         [0,  [Validators.required, Validators.min(1)]],
      CategoryId:    ['', Validators.required],
      StockQuantity: [0,  [Validators.required, Validators.min(0)]],
    });

    this.categoryService.getAll().subscribe({
      next: (cats) => { this.categories = cats; },
      error: () => { this.error = 'Không tải được danh mục sản phẩm'; }
    });

    this.productId = this.route.snapshot.params['id'];
    if (this.productId) {
      this.isEdit = true;
      this.isLoading = true;
      this.productService.getById(this.productId).subscribe({
        next: (p) => {
          this.form.patchValue(p);
          this.isLoading = false;
        },
        error: () => {
          this.error = 'Không tìm thấy sản phẩm';
          this.isLoading = false;
        }
      });
    }
  }

  submit(): void {
    if (this.form.invalid) return;
    this.isSaving = true;
    this.error = '';

    const formValue = this.form.value;
    const payload = {
      ProductName: formValue.ProductName,
      Price: Number(formValue.Price),
      CategoryId: formValue.CategoryId,
      StockQuantity: Number(formValue.StockQuantity)
    };

    const action = this.isEdit
      ? this.productService.update(this.productId!, payload)
      : this.productService.create(payload);

    action.subscribe({
      next: () => this.router.navigate(['/products']),
      error: (err) => {
        this.error = err?.error?.message ?? err?.error?.error ?? 'Lỗi lưu dữ liệu. Kiểm tra lại thông tin!';
        this.isSaving = false;
      }
    });
  }
}