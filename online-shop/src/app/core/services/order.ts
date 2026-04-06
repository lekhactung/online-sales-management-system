import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api';
import { Order, CreateOrder, UpdateOrder } from '../../shared/models/order.model';

@Injectable({ providedIn: 'root' })
export class OrderService {
  private endpoint = 'order';

  constructor(private api: ApiService) {}

  getAll(): Observable<Order[]> {
    return this.api.get<Order[]>(this.endpoint);
  }

  getById(id: string): Observable<Order> {
    return this.api.get<Order>(`${this.endpoint}/${id}`);
  }

  create(dto: CreateOrder): Observable<{OrderId: string}> {
    return this.api.post<{OrderId: string}>(this.endpoint, dto);
  }

  update(id: string, dto: UpdateOrder): Observable<void> {
    return this.api.put<void>(`${this.endpoint}/${id}`, dto);
  }

  updateStatus(id: string, statusId: string): Observable<void> {
    return this.api.patch<void>(`${this.endpoint}/${id}/status`, { StatusId: statusId });
  }

  getOrdersByCustomer(customerId: string): Observable<Order[]> {
    return this.api.get<Order[]>(`${this.endpoint}/customer/${customerId}`);
  }

  delete(id: string): Observable<void> {
    return this.api.delete<void>(`${this.endpoint}/${id}`);
  }
}
