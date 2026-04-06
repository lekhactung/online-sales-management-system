import { Component, OnInit } from '@angular/core';
import { NgIf, NgFor, DatePipe, DecimalPipe } from '@angular/common';
import { RouterLink, ActivatedRoute } from '@angular/router';
import { OrderService } from '../../../core/services/order';
import { Order } from '../../../shared/models/order.model';

const ORDER_STATUSES = [
  { id: 'TT01', label: 'Chờ xác nhận' },
  { id: 'TT02', label: 'Đang chuẩn bị hàng' },
  { id: 'TT03', label: 'Đang giao hàng' },
  { id: 'TT04', label: 'Đã giao' },
  { id: 'TT05', label: 'Đã hủy' }
];

@Component({
  selector: 'app-order-detail',
  standalone: true,
  imports: [NgIf, NgFor, DatePipe, DecimalPipe, RouterLink],
  templateUrl: './order-detail.html',
  styleUrl: './order-detail.scss',
})
export class OrderDetail implements OnInit {
  order?: Order;
  isLoading = true;
  error = '';
  statusMap: { [key: string]: string } = {};

  constructor(
    private route: ActivatedRoute,
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) {
      this.error = 'Không tìm thấy ID đơn hàng.';
      this.isLoading = false;
      return;
    }

    this.orderService.getById(id).subscribe({
      next: (order) => {
        this.order = order;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Lỗi nạp thông tin đơn hàng hoặc đơn hàng không tồn tại.';
        this.isLoading = false;
      }
    });
  }

  getStatusClass(statusId: string): string {
    switch (statusId) {
      case 'TT01': return 'status-pending';   // Chờ xác nhận
      case 'TT02': return 'status-preparing'; // Đang chuẩn bị
      case 'TT03': return 'status-shipping';  // Đang giao
      case 'TT04': return 'status-delivered'; // Đã giao
      case 'TT05': return 'status-cancelled'; // Đã huỷ
      default: return 'status-default';
    }
  }

  getStatusLabel(statusId: string, statusName?: string): string {
    if (statusName) return statusName;
    return ORDER_STATUSES.find(s => s.id === statusId)?.label || statusId;
  }
}
