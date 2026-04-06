import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { CustomerService } from '../../../core/services/customer';
import { Customer } from '../../../shared/models/customer.model';

@Component({
  selector: 'app-customer-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './customer-list.html',
  styles: [`
    .page-header { display: flex; align-items: center; justify-content: space-between; margin-bottom: 1.5rem; }
    .page-title { font-size: 1.5rem; font-weight: 700; color: #f1f5f9; margin: 0; }
    .page-subtitle { font-size: 0.85rem; color: #64748b; margin: 4px 0 0; }

    .btn-create {
      background: linear-gradient(135deg, #06b6d4, #0284c7);
      border: none; color: #fff; border-radius: 10px;
      padding: 0.5rem 1.2rem; font-weight: 600; font-size: 0.9rem;
      display: inline-flex; align-items: center; gap: 6px;
      transition: transform .15s, box-shadow .15s; text-decoration: none;
    }
    .btn-create:hover { transform: translateY(-2px); box-shadow: 0 4px 16px rgba(6,182,212,.4); color: #fff; }

    .search-bar {
      background: #1e293b; border: 1px solid #334155; border-radius: 12px;
      padding: 0.75rem 1rem; display: flex; gap: 10px; align-items: center;
      margin-bottom: 1.25rem;
    }
    .search-select { background: #0f172a; color: #e2e8f0; border: 1px solid #334155; border-radius: 8px; padding: 6px 10px; font-size: 0.85rem; }
    .search-input  { background: #0f172a; color: #e2e8f0; border: 1px solid #334155; border-radius: 8px; padding: 6px 12px; font-size: 0.85rem; flex: 1; }
    .search-input:focus, .search-select:focus { outline: none; border-color: #06b6d4; }
    .search-input::placeholder { color: #475569; }
    .btn-search {
      background: #0ea5e9; border: none; color: #fff; border-radius: 8px;
      padding: 6px 14px; font-size: 0.85rem; font-weight: 600; cursor: pointer;
      display: flex; align-items: center; gap: 5px; transition: background .2s;
    }
    .btn-search:hover { background: #0284c7; }

    .table-card { background: #1e293b; border-radius: 16px; overflow: hidden; box-shadow: 0 4px 24px rgba(0,0,0,.35); }
    .cust-table { width: 100%; border-collapse: collapse; color: #e2e8f0; }
    .cust-table thead { background: #0f172a; }
    .cust-table th { padding: 14px 16px; font-size: 0.78rem; text-transform: uppercase; letter-spacing: .05em; color: #64748b; font-weight: 600; border-bottom: 1px solid #334155; }
    .cust-table td { padding: 12px 16px; border-bottom: 1px solid #1e3a5f22; vertical-align: middle; }
    .cust-table tbody tr:hover { background: rgba(255,255,255,.035); }

    .avatar-circle {
      width: 40px; height: 40px; border-radius: 50%;
      background: linear-gradient(135deg,#06b6d4,#7c3aed);
      display: flex; align-items: center; justify-content: center;
      font-weight: 700; font-size: 0.9rem; color: #fff; flex-shrink: 0;
    }
    .cust-name { font-weight: 600; color: #f1f5f9; }
    .cust-id   { font-family: monospace; font-size: 0.78rem; color: #475569; }
    .contact-line { font-size: 0.83rem; color: #94a3b8; display: flex; align-items: center; gap: 5px; }
    .address-tag { background: rgba(99,102,241,.12); color: #a5b4fc; border-radius: 20px; padding: 3px 10px; font-size: 0.78rem; }

    .action-btn { background: transparent; border: 1px solid #334155; border-radius: 8px; padding: 5px 12px; font-size: 0.8rem; cursor: pointer; transition: all .2s; display: inline-flex; align-items: center; gap: 4px; text-decoration: none; }
    .btn-edit  { color: #7dd3fc; } .btn-edit:hover  { background: rgba(125,211,252,.1); border-color: #7dd3fc; }
    .btn-del   { color: #f87171; } .btn-del:hover   { background: rgba(248,113,113,.1); border-color: #f87171; }

    .modal-overlay {
      position: fixed; inset: 0; background: rgba(0,0,0,.7); z-index: 9999;
      display: flex; align-items: center; justify-content: center;
    }
    .modal-box {
      background: #1e293b; border: 1px solid #334155; border-radius: 16px;
      padding: 2rem; max-width: 380px; width: 90%; text-align: center;
    }
    .modal-title { font-size: 1.1rem; font-weight: 700; color: #f1f5f9; margin-bottom: .5rem; }
    .modal-sub   { font-size: 0.85rem; color: #94a3b8; margin-bottom: 1.5rem; }
    .modal-actions { display: flex; gap: 10px; justify-content: center; }
    .btn-cancel-modal { background: #1e293b; border: 1px solid #334155; color: #94a3b8; border-radius: 8px; padding: 8px 20px; cursor: pointer; }
    .btn-confirm-del  { background: #dc2626; border: none; color: #fff; border-radius: 8px; padding: 8px 20px; font-weight: 600; cursor: pointer; }

    .empty-row td { text-align: center; padding: 3rem; color: #475569; }
    .spinner-wrap { text-align: center; padding: 4rem; }
    .alert-err { background: rgba(248,113,113,.1); border: 1px solid rgba(248,113,113,.3); color: #f87171; border-radius: 12px; padding: 1rem 1.25rem; margin-bottom: 1rem; }
    .alert-ok  { background: rgba(52,211,153,.1);  border: 1px solid rgba(52,211,153,.3);  color: #34d399; border-radius: 12px; padding: 1rem 1.25rem; margin-bottom: 1rem; }
  `]
})
export class CustomerListComponent implements OnInit {
  customers: Customer[] = [];
  isLoading = true;
  error = '';
  successMsg = '';
  searchQuery = '';
  searchType: 'name' | 'phone' = 'name';
  deleteConfirmId: string | null = null;
  isDeleting = false;

  constructor(private customerService: CustomerService) {}

  ngOnInit(): void { this.loadAllCustomers(); }

  loadAllCustomers() {
    this.isLoading = true;
    this.customerService.getAll().subscribe({
      next: (data) => { this.customers = data; this.isLoading = false; },
      error: () => { this.error = 'Không thể tải danh sách khách hàng. Kiểm tra backend!'; this.isLoading = false; }
    });
  }

  searchCustomers() {
    if (!this.searchQuery.trim()) { this.loadAllCustomers(); return; }
    this.isLoading = true;
    const obs = this.searchType === 'name'
      ? this.customerService.searchByName(this.searchQuery)
      : this.customerService.searchByPhone(this.searchQuery);
    obs.subscribe({
      next: (data) => { this.customers = data; this.isLoading = false; },
      error: () => { this.error = 'Lỗi tìm kiếm'; this.isLoading = false; }
    });
  }

  confirmDelete(id: string) { this.deleteConfirmId = id; }

  cancelDelete() { this.deleteConfirmId = null; }

  doDelete() {
    if (!this.deleteConfirmId) return;
    this.isDeleting = true;
    this.customerService.delete(this.deleteConfirmId).subscribe({
      next: () => {
        this.customers = this.customers.filter(c => c.CustomerId !== this.deleteConfirmId);
        this.successMsg = 'Xoá khách hàng thành công!';
        this.deleteConfirmId = null;
        this.isDeleting = false;
        setTimeout(() => this.successMsg = '', 3000);
      },
      error: (err) => {
        this.error = err?.error?.message ?? 'Không thể xoá khách hàng.';
        this.deleteConfirmId = null;
        this.isDeleting = false;
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
