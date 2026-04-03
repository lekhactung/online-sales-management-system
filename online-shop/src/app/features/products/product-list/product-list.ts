import { Component, OnInit } from '@angular/core';
import { NgFor, NgIf, DecimalPipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../../core/services/product';
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

  constructor(private productService: ProductService) {}

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

  onSearch(): void {
    if (!this.keyword.trim()) {
      this.filteredProducts = this.products;
      return;
    }
    this.productService.search(this.keyword).subscribe({
      next: (data) => {
        this.filteredProducts = data;
      },
      error: () => { this.filteredProducts = []; }
    });
  }

  delete(id: string): void {
    if (!confirm('Xác nhận xoá sản phẩm này?')) return;
    this.productService.delete(id).subscribe({
      next: () => this.loadProducts()
    });
  }
}
