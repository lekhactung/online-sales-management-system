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
  isLoading = true;
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
      error: () => {
        this.error = 'Không thể tải danh sách khách hàng. Kiểm tra backend!';
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
    const obs = this.searchType === 'name'
      ? this.customerService.searchByName(this.searchQuery)
      : this.customerService.searchByPhone(this.searchQuery);

    obs.subscribe({
      next: (data) => {
        this.customers = data;
        this.isLoading = false;
      },
      error: () => {
        this.error = 'Lỗi tìm kiếm';
        this.isLoading = false;
      }
    });
  }

  getInitials(firstName: string, lastName: string): string {
    const f = firstName || '';
    const l = lastName || '';
    if (!f && !l) return 'KH';
    return `${l.charAt(0)}${f.charAt(0)}`.toUpperCase();
  }
}
