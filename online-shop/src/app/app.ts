import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HeaderComponent } from './components/header/header';
import { FooterComponent } from './components/footer/footer';
import { ProductsCardComponent } from './components/products-card/products-card';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    HeaderComponent,
    FooterComponent,
    ProductsCardComponent,
    CommonModule,
    FormsModule,
  ],
  template: `
    <div class="d-flex" style="min-height: 100vh; background-color: #0f172a;">
      <div class="bg-dark text-white p-3 shadow d-flex flex-column" style="width: 250px; border-right: 1px solid #334155;">
        <div class="d-flex align-items-center mb-4 ps-2">
          <i class="bi bi-box-seam-fill fs-3 text-info me-2"></i> 
          <span class="fs-4 fw-bold">Shop<span class="text-info">Tử Thần:))</span></span>
        </div>
        <ul class="nav nav-pills flex-column">
          <li class="nav-item mb-2"><a class="nav-link active bg-info text-dark fw-bold"><i class="bi bi-grid-1x2-fill me-2"></i> Sản phẩm</a></li>
          <li class="nav-item mb-2"><a class="nav-link text-white opacity-75"><i class="bi bi-cart-fill me-2"></i> Đơn hàng</a></li>
        </ul>
      <div class="mt-auto border-top border-secondary pt-3">
        <div *ngIf="!isLoggedIn">
          <button class="btn btn-outline-info w-100 d-flex align-items-center justify-content-center gap-2" (click)="login()">
            <i class="bi bi-box-arrow-in-right"></i> Đăng nhập
          </button>
        </div>

        <div *ngIf="isLoggedIn" class="d-flex flex-column gap-2">
          <div class="d-flex align-items-center gap-2 mb-2 px-2 text-white">
            <div class="bg-info rounded-circle d-flex align-items-center justify-content-center text-dark fw-bold" style="width: 32px; height: 32px;">
              {{ userName.charAt(0) }}
            </div>
            <span class="small fw-bold text-truncate">{{ userName }}</span>
          </div>
          <button class="btn btn-outline-danger btn-sm w-100" (click)="logout()">
            <i class="bi bi-box-arrow-left me-1"></i> Đăng xuất
          </button>
        </div>
      </div>
      </div>

      <div class="flex-grow-1 p-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
          <h2 class="fw-bold text-white mb-0">Quản lý Sản phẩm</h2>
          <button class="btn btn-info btn-sm fw-bold px-3" (click)="openAddModal()">+ THÊM MỚI</button>
        </div>

        <div class="row mb-4">
          <div class="col-md-5">
            <div class="input-group">
              <span class="input-group-text bg-secondary border-0 text-white"><i class="bi bi-search"></i></span>
              <input type="text" class="form-control bg-dark text-white border-0 py-2" 
                     placeholder="Tìm kiếm sản phẩm..." [(ngModel)]="searchText">
            </div>
          </div>
        </div>

        <div class="table-responsive rounded-3 shadow">
          <table class="admin-table w-100">
            <thead>
              <tr>
                <th>ID</th>
                <th>Tên sản phẩm</th>
                <th>Danh mục</th>
                <th>Giá bán</th>
                <th>Kho</th>
                <th>Thao tác</th>
              </tr>
            </thead>
            <tbody>
              <tr *ngFor="let p of filteredProducts()">
                <td class="text-secondary">#00{{p.id}}</td>
                <td class="fw-bold text-info">{{p.name}}</td>
                <td>{{p.category}}</td>
                <td>{{p.price}}</td>
                <td>{{p.stock}}</td>
                <td>
                  <button class="btn btn-sm btn-outline-warning me-2" (click)="editProduct(p)"><i class="bi bi-pencil-square"></i></button>
                  <button class="btn btn-sm btn-outline-danger" (click)="deleteProduct(p.id)"><i class="bi bi-trash3-fill"></i></button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
    <div *ngIf="showModal" class="position-fixed top-0 start-0 w-100 h-100 d-flex align-items-center justify-content-center" style="background: rgba(0,0,0,0.8); z-index: 9999;">
  <div class="bg-dark p-4 rounded-3 shadow-lg border border-secondary" style="width: 400px;">
    <h4 class="text-white mb-4">{{ isEditMode ? 'Cập nhật' : 'Thêm mới' }} Sản phẩm</h4>
    
    <div class="mb-3">
      <label class="text-secondary small">Tên sản phẩm</label>
      <input type="text" class="form-control bg-dark text-white border-secondary shadow-none" [(ngModel)]="currentProduct.name">
    </div>
    <div class="mb-3">
      <label class="text-secondary small">Giá bán</label>
      <input type="text" class="form-control bg-dark text-white border-secondary shadow-none" [(ngModel)]="currentProduct.price">
    </div>
    <div class="mb-3">
      <label class="text-secondary small">Danh mục</label>
      <select class="form-select bg-dark text-white border-secondary shadow-none" [(ngModel)]="currentProduct.category">
        <option value="Điện thoại">Điện thoại</option>
        <option value="Laptop">Laptop</option>
        <option value="Phụ kiện">Phụ kiện</option>
      </select>
    </div>
    <div class="mb-4">
      <label class="text-secondary small">Số lượng kho</label>
      <input type="number" class="form-control bg-dark text-white border-secondary shadow-none" [(ngModel)]="currentProduct.stock">
    </div>

    <div class="d-flex justify-content-end gap-2">
      <button class="btn btn-outline-secondary" (click)="showModal = false">Hủy</button>
      <button class="btn btn-info fw-bold" (click)="saveProduct()">LƯU LẠI</button>
    </div>
  </div>
</div>
  `
})
export class AppComponent {
  title = 'online-shop';
  searchText: string = ''; 
  showModal: boolean = false; // Biến điều khiển ẩn/hiện Form
  isEditMode: boolean = false; // Kiểm tra đang Thêm hay đang Sửa
  isLoggedIn: boolean = false;
userName: string = 'Lê Tùng';

login() { this.isLoggedIn = true; }
logout() { this.isLoggedIn = false; }

  // Biến tạm để chứa dữ liệu đang nhập trên Form
  currentProduct: any = { id: 0, name: '', price: '', category: '', stock: 0 };

  products = [
    { id: 1, name: 'iPhone 14', price: '22,000,000 d', category: 'Điện thoại', stock: 25 },
    { id: 2, name: 'Samsung Galaxy S23', price: '18,000,000 d', category: 'Điện thoại', stock: 36 },
    { id: 3, name: 'Dell XPS 13', price: '35,000,000 d', category: 'Laptop', stock: 5 },
    { id: 4, name: 'Sony Headphone', price: '3,000,000 d', category: 'Phụ kiện', stock: 100 },
    { id: 5, name: 'Sạc nhanh Samsung', price: '500,000 d', category: 'Phụ kiện', stock: 136 }
  ];

  filteredProducts() {
    return this.products.filter(p => p.name.toLowerCase().includes(this.searchText.toLowerCase()));
  }

  // Mở Form để Thêm mới
  openAddModal() {
    this.isEditMode = false;
    this.currentProduct = { id: this.products.length + 1, name: '', price: '', category: 'Điện thoại', stock: 0 };
    this.showModal = true;
  }

  // Mở Form để Sửa
  editProduct(p: any) {
    this.isEditMode = true;
    this.currentProduct = { ...p }; // Copy dữ liệu ra biến tạm để sửa
    this.showModal = true;
  }

  // Lưu dữ liệu (Dùng cho cả Thêm và Sửa)
  saveProduct() {
    console.log('Lưu sản phẩm', this.currentProduct);
  if (this.isEditMode) {
    // Logic cho nút Sửa
    const index = this.products.findIndex(p => p.id === this.currentProduct.id);
    if (index !== -1) {
      this.products[index] = { ...this.currentProduct };
    }
  } else {
    // Logic cho nút Thêm mới (Cái này ông đang bị lỗi nè)
    // Tạo ID mới bằng cách lấy ID lớn nhất cộng 1
    const newId = this.products.length > 0 ? Math.max(...this.products.map(p => p.id)) + 1 : 1;
    const productToAdd = { ...this.currentProduct, id: newId };
    
    this.products.push(productToAdd); // <--- Dòng này để "bơm" sản phẩm vào bảng
  }
  
  this.showModal = false; // Đóng Form sau khi lưu
  // test change
  // test branch
}

  deleteProduct(id: number) {
    if (confirm('Bạn có chắc muốn xóa không?')) {
      this.products = this.products.filter(p => p.id !== id);
    }
  }
}