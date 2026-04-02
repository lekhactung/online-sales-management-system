import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api';
import { Order } from '../../shared/models/order.model';
 
@Injectable({ providedIn: 'root' })
export class OrderService {
  private endpoint = 'orders';
 
  constructor(private api: ApiService) {}
 
  getAll(): Observable<Order[]> {
    return this.api.get<Order[]>(this.endpoint);
  }
 
  getById(id: number): Observable<Order> {
    return this.api.get<Order>(`${this.endpoint}/${id}`);
  }
 
  create(order: Partial<Order>): Observable<Order> {
    return this.api.post<Order>(this.endpoint, order);
  }
 
  updateStatus(id: number, statusId: number): Observable<Order> {
    return this.api.put<Order>(`${this.endpoint}/${id}/status`, { statusId });
  }
}
