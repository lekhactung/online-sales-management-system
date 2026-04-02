import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CustomerService } from '../../../core/services/customer';
import { Customer } from '../../../shared/models/customer.model';

@Component({
  selector: 'app-customer-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './customer-list.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
  customers: Customer[] = [];
  isLoading = false;
  error = '';
  searchQuery = '';
  searchType: 'name' | 'phone' = 'name';

  constructor(private customerService: CustomerService) {}

  ngOnInit(): void {
    this.loadAllCustomers();
  }

  loadAllCustomers() {
    this.isLoading = true;
    this.customerService.getAll().subscribe({
      next: (data) => {
        this.customers = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Không thể tải danh sách khách hàng. Máy chủ C# có đang chạy?';
        this.isLoading = false;
      }
    });
  }

  searchCustomers() {
    if (!this.searchQuery.trim()) {
      this.loadAllCustomers();
      return;
    }

    this.isLoading = true;
    if (this.searchType === 'name') {
      this.customerService.searchByName(this.searchQuery).subscribe({
        next: (data) => {
          this.customers = data;
          this.isLoading = false;
        },
        error: () => {
          this.error = 'Lỗi tìm kiếm theo tên';
          this.isLoading = false;
        }
      });
    } else {
      this.customerService.searchByPhone(this.searchQuery).subscribe({
        next: (data) => {
          this.customers = data;
          this.isLoading = false;
        },
        error: () => {
          this.error = 'Lỗi tìm kiếm theo số điện thoại';
          this.isLoading = false;
        }
      });
    }
  }

  getInitials(firstName: string, lastName: string): string {
    return `${lastName.charAt(0)}${firstName.charAt(0)}`.toUpperCase();
  }
}
