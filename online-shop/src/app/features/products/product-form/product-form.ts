import { Component, OnInit } from '@angular/core';
import { NgIf, NgFor, NgClass } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, FormsModule } from '@angular/forms';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { ProductService } from '../../../core/services/product';
import { ProductCategoryService } from '../../../core/services/product-category';
import { ProductCategory } from '../../../shared/models/product-category.model';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [NgIf, NgFor, NgClass, ReactiveFormsModule, FormsModule, RouterLink],
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

  showCategoryForm = false;
  newCategoryName = '';
  isAddingCategory = false;
  categoryError = '';

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

    this.loadCategories();

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

  loadCategories() {
    this.categoryService.getAll().subscribe({
      next: (cats) => { this.categories = cats; },
      error: () => { this.error = 'Không tải được danh mục sản phẩm'; }
    });
  }

  toggleCategoryForm() {
    this.showCategoryForm = !this.showCategoryForm;
    this.newCategoryName = '';
    this.categoryError = '';
  }

  addCategory() {
    if (!this.newCategoryName.trim()) return;
    this.isAddingCategory = true;
    this.categoryError = '';

    this.categoryService.create({ 
      CategoryName: this.newCategoryName.trim()
    }).subscribe({
      next: (res: any) => {
        this.isAddingCategory = false;
        this.showCategoryForm = false;
        this.categoryService.getAll().subscribe(cats => {
          this.categories = cats;
          this.form.patchValue({ CategoryId: res.CreatedId || res.createdId });
        });
      },
      error: (err) => {
        this.isAddingCategory = false;
        this.categoryError = err?.error?.message || 'Lỗi lưu danh mục';
      }
    });
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.isSaving = true;
    this.error = '';

    const formValue = this.form.value;
    const payload: any = {
      ProductName: formValue.ProductName,
      Price: Number(formValue.Price),
      CategoryId: formValue.CategoryId,
      StockQuantity: Number(formValue.StockQuantity)
    };

    if (this.isEdit) {
      payload.ProductId = this.productId;
    }

    const action = this.isEdit
      ? this.productService.update(this.productId!, payload)
      : this.productService.create(payload);

    action.subscribe({
      next: () => this.router.navigate(['/products']),
      error: (err) => {
        let errorMsg = 'Lỗi lưu dữ liệu. Kiểm tra lại thông tin!';
        if (err?.error?.errors) {
          const firstErrorKey = Object.keys(err.error.errors)[0];
          errorMsg = err.error.errors[firstErrorKey][0];
        } else if (err?.error?.message) {
          errorMsg = err.error.message;
        } else if (err?.error?.title) {
          errorMsg = err.error.title;
        } else if (typeof err?.error === 'string') {
          errorMsg = err.error;
        }
        
        this.error = errorMsg;
        this.isSaving = false;
      }
    });
  }
}