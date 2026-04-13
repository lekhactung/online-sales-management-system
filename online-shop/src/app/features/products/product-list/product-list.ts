import { Component, OnInit } from '@angular/core';
import { NgFor, NgIf, DecimalPipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../../core/services/product';
import { ProductCategoryService } from '../../../core/services/product-category';
import { Product } from '../../../shared/models/product.model';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [NgFor, NgIf, DecimalPipe, RouterLink, FormsModule],
  templateUrl: './product-list.html',
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  filteredProducts: Product[] = [];
  keyword = '';
  isLoading = true;
  error = '';

  showCatModal = false;
  newCategoryName = '';
  isAddingCat = false;
  catError = '';
  catSuccess = '';

  constructor(
    private productService: ProductService,
    private categoryService: ProductCategoryService
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.isLoading = true;
    this.error = '';
    this.productService.getAll().subscribe({
      next: (data) => {
        this.products = data;
        this.filteredProducts = data;
        this.isLoading = false;
      },
      error: () => {
        this.error = 'Lỗi kết nối API. Đảm bảo backend đang chạy!';
        this.isLoading = false;
      }
    });
  }

  toggleCategoryModal(): void {
    this.showCatModal = !this.showCatModal;
    this.newCategoryName = '';
    this.catError = '';
    this.catSuccess = '';
  }

  quickAddCategory(): void {
    if (!this.newCategoryName.trim()) return;
    this.isAddingCat = true;
    this.catError = '';
    this.catSuccess = '';

    this.categoryService.create({ 
      CategoryName: this.newCategoryName.trim()
    }).subscribe({
      next: (res: any) => {
        this.isAddingCat = false;
        this.catSuccess = `Đã thêm danh mục "${this.newCategoryName.trim()}" thành công!`;
        this.newCategoryName = '';
        setTimeout(() => { if(this.showCatModal) this.toggleCategoryModal() }, 2000);
      },
      error: (err) => {
        this.isAddingCat = false;
        this.catError = err?.error?.message || 'Có lỗi xảy ra khi lưu danh mục.';
      }
    });
  }

  minPrice: number | null = null;
  maxPrice: number | null = null;
  sortOrder: string = '';

  onSearch(): void {
    this.applyFilters();
  }

  applyFilters(): void {
    let result = [...this.products]; // Tránh làm thay đổi thứ tự mảng gốc

    // 1. Lọc theo từ khóa (Keyword)
    if (this.keyword.trim()) {
      const lowerKeyword = this.keyword.toLowerCase();
      result = result.filter(p => 
        p.ProductName.toLowerCase().includes(lowerKeyword) || 
        p.ProductId.toLowerCase().includes(lowerKeyword) ||
        (p.CategoryName && p.CategoryName.toLowerCase().includes(lowerKeyword))
      );
    }

    // 2. Lọc theo khoảng giá
    if (this.minPrice !== null && this.minPrice >= 0) {
      result = result.filter(p => p.Price >= this.minPrice!);
    }
    if (this.maxPrice !== null && this.maxPrice >= 0) {
      result = result.filter(p => p.Price <= this.maxPrice!);
    }

    // 3. Sắp xếp
    if (this.sortOrder === 'price_asc') {
      result = result.sort((a, b) => a.Price - b.Price);
    } else if (this.sortOrder === 'price_desc') {
      result = result.sort((a, b) => b.Price - a.Price);
    } else if (this.sortOrder === 'name_asc') {
      result = result.sort((a, b) => a.ProductName.localeCompare(b.ProductName));
    } else if (this.sortOrder === 'stock_desc') {
      result = result.sort((a, b) => b.StockQuantity - a.StockQuantity);
    } else if (this.sortOrder === 'stock_asc') {
      result = result.sort((a, b) => a.StockQuantity - b.StockQuantity);
    }

    this.filteredProducts = [...result];
  }


  delete(id: string): void {
    if (!confirm('Xác nhận xoá sản phẩm này?')) return;
    this.productService.delete(id).subscribe({
      next: () => this.loadProducts()
    });
  }
}
