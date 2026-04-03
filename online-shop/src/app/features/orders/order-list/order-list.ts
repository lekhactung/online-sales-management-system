import { Component, OnInit } from '@angular/core';
import { NgFor, NgIf, DatePipe, DecimalPipe } from '@angular/common';
import { OrderService } from '../../../core/services/order';
import { Order } from '../../../shared/models/order.model';

@Component({
  selector: 'app-order-list',
  standalone: true,
  imports: [NgFor, NgIf, DatePipe, DecimalPipe],
  templateUrl: './order-list.html'
})
export class OrderListComponent implements OnInit {
  orders: Order[] = [];
  isLoading = true;
  error = '';

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.orderService.getAll().subscribe({
      next: (data) => {
        this.orders = data;
        this.isLoading = false;
      },
      error: () => {
        this.error = 'Không thể tải đơn hàng. Kiểm tra backend!';
        this.isLoading = false;
      }
    });
  }
}
