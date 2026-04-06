import { Component, OnInit } from '@angular/core';
import { NgFor, NgIf, DatePipe, DecimalPipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { OrderService } from '../../../core/services/order';
import { Order } from '../../../shared/models/order.model';

const ORDER_STATUSES = [
  { id: 'TT01', label: 'Chờ xác nhận', cls: 'status-pending' },
  { id: 'TT02', label: 'Đang chuẩn bị hàng', cls: 'status-processing' },
  { id: 'TT03', label: 'Đang giao hàng', cls: 'status-shipped' },
  { id: 'TT04', label: 'Đã giao', cls: 'status-delivered' },
  { id: 'TT05', label: 'Đã hủy', cls: 'status-cancelled' }
];

@Component({
  selector: 'app-order-list',
  standalone: true,
  imports: [NgFor, NgIf, DatePipe, DecimalPipe, RouterLink, FormsModule],
  templateUrl: './order-list.html',
  styles: [`
    .page-header { display: flex; align-items: center; justify-content: space-between; margin-bottom: 1.5rem; }
    .page-title { font-size: 1.5rem; font-weight: 700; color: #f1f5f9; margin: 0; }
    .btn-create {
      background: linear-gradient(135deg, #06b6d4, #0284c7);
      border: none; color: #fff; border-radius: 10px;
      padding: 0.5rem 1.2rem; font-weight: 600; font-size: 0.9rem;
      display: flex; align-items: center; gap: 6px;
      transition: transform 0.15s, box-shadow 0.15s;
      text-decoration: none;
    }
    .btn-create:hover { transform: translateY(-2px); box-shadow: 0 4px 16px rgba(6,182,212,.4); color: #fff; }

    .table-card {
      background: #1e293b; border-radius: 16px; overflow: hidden;
      box-shadow: 0 4px 24px rgba(0,0,0,.35);
    }
    .order-table { width: 100%; border-collapse: collapse; color: #e2e8f0; }
    .order-table thead { background: #0f172a; }
    .order-table th { padding: 14px 16px; font-size: 0.8rem; text-transform: uppercase; letter-spacing: .05em; color: #64748b; font-weight: 600; border-bottom: 1px solid #334155; }
    .order-table td { padding: 14px 16px; border-bottom: 1px solid #1e3a5f22; vertical-align: middle; }
    .order-table tbody tr:hover { background: rgba(255,255,255,.04); }

    .order-id { font-family: monospace; color: #94a3b8; font-size: 0.85rem; }
    .customer-name { font-weight: 600; color: #7dd3fc; }
    .amount { font-weight: 700; color: #34d399; }

    /* Status badges */
    .status-pending    { background: rgba(251,191,36,.15); color: #fbbf24; border: 1px solid rgba(251,191,36,.3); }
    .status-processing { background: rgba(99,102,241,.15);  color: #a5b4fc; border: 1px solid rgba(99,102,241,.3); }
    .status-shipped    { background: rgba(59,130,246,.15);  color: #93c5fd; border: 1px solid rgba(59,130,246,.3); }
    .status-delivered  { background: rgba(52,211,153,.15);  color: #34d399; border: 1px solid rgba(52,211,153,.3); }
    .status-cancelled  { background: rgba(248,113,113,.15); color: #f87171; border: 1px solid rgba(248,113,113,.3); }
    .status-badge      { border-radius: 20px; padding: 4px 12px; font-size: 0.78rem; font-weight: 600; white-space: nowrap; display: inline-block; }

    .status-select {
      background: #0f172a; color: #e2e8f0; border: 1px solid #334155;
      border-radius: 8px; padding: 4px 8px; font-size: 0.8rem; cursor: pointer;
      transition: border-color .2s;
    }
    .status-select:focus { outline: none; border-color: #06b6d4; }

    .action-btn { background: transparent; border: 1px solid #334155; border-radius: 8px; padding: 5px 12px; font-size: 0.8rem; cursor: pointer; transition: all .2s; display: inline-flex; align-items: center; gap: 4px; }
    .btn-edit  { color: #7dd3fc; } .btn-edit:hover  { background: rgba(125,211,252,.1); border-color: #7dd3fc; }
    .btn-del   { color: #f87171; } .btn-del:hover   { background: rgba(248,113,113,.1); border-color: #f87171; }

    .empty-row td { text-align: center; padding: 3rem; color: #475569; }
    .spinner-wrap { text-align: center; padding: 4rem; }
    .alert-err { background: rgba(248,113,113,.1); border: 1px solid rgba(248,113,113,.3); color: #f87171; border-radius: 12px; padding: 1rem 1.25rem; margin-bottom: 1rem; }
    .saving-dot { display: inline-block; width: 8px; height: 8px; border-radius: 50%; background: #06b6d4; animation: pulse 1s infinite; }
    @keyframes pulse { 0%,100%{opacity:1} 50%{opacity:.3} }
  `]
})
export class OrderListComponent implements OnInit {
  orders: Order[] = [];
  isLoading = true;
  error = '';
  successMsg = '';
  deleteConfirmId: string | null = null;
  isDeleting = false;
  statuses = ORDER_STATUSES;
  statusUpdating: Record<string, boolean> = {};

  constructor(private orderService: OrderService) {}

  ngOnInit(): void { this.loadOrders(); }

  loadOrders(): void {
    this.isLoading = true;
    this.orderService.getAll().subscribe({
      next: (data) => { this.orders = data; this.isLoading = false; },
      error: () => { this.error = 'Không thể tải đơn hàng. Kiểm tra backend!'; this.isLoading = false; }
    });
  }

  onStatusChange(order: Order, newStatusId: string): void {
    this.statusUpdating[order.OrderId] = true;
    this.orderService.updateStatus(order.OrderId, newStatusId).subscribe({
      next: () => {
        order.StatusId = newStatusId;
        const found = this.statuses.find(s => s.id === newStatusId);
        order.StatusName = found?.label ?? newStatusId;
        this.statusUpdating[order.OrderId] = false;
      },
      error: (err) => {
        this.error = err?.error?.message ?? 'Không cập nhật được trạng thái.';
        this.statusUpdating[order.OrderId] = false;
      }
    });
  }

  getStatusClass(statusId: string): string {
    return this.statuses.find(s => s.id === statusId)?.cls ?? 'status-pending';
  }

  getStatusLabel(statusId: string): string {
    return this.statuses.find(s => s.id === statusId)?.label ?? statusId;
  }

  confirmDelete(id: string): void {
    this.deleteConfirmId = id;
  }

  cancelDelete(): void {
    this.deleteConfirmId = null;
  }

  doDelete(): void {
    if (!this.deleteConfirmId) return;
    this.isDeleting = true;
    this.orderService.delete(this.deleteConfirmId).subscribe({
      next: () => {
        this.orders = this.orders.filter(o => o.OrderId !== this.deleteConfirmId);
        this.successMsg = 'Xoá đơn hàng thành công!';
        this.deleteConfirmId = null;
        this.isDeleting = false;
        setTimeout(() => this.successMsg = '', 3000);
      },
      error: (err) => {
        this.error = err?.error?.message ?? 'Không thể xoá đơn hàng. Có thể đơn hàng đang gắn với dữ liệu khác.';
        this.deleteConfirmId = null;
        this.isDeleting = false;
        setTimeout(() => this.error = '', 5000);
      }
    });
  }
}
