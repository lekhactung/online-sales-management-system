import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api';
import { Customer, CreateCustomer } from '../../shared/models/customer.model';

@Injectable({ providedIn: 'root' })
export class CustomerService {
  private endpoint = 'customer';

  constructor(private api: ApiService) {}

  getAll(): Observable<Customer[]> {
    return this.api.get<Customer[]>(this.endpoint);
  }

  getById(id: string): Observable<Customer> {
    return this.api.get<Customer>(`${this.endpoint}/${id}`);
  }

  create(dto: CreateCustomer): Observable<string> {
    return this.api.post<string>(this.endpoint, dto);
  }

  update(id: string, dto: Partial<Customer>): Observable<void> {
    return this.api.put<void>(`${this.endpoint}/${id}`, dto);
  }

  delete(id: string): Observable<void> {
    return this.api.delete<void>(`${this.endpoint}/${id}`);
  }

  searchByName(name: string): Observable<Customer[]> {
    return this.api.get<Customer[]>(`${this.endpoint}/search?name=${name}`);
  }

  searchByPhone(phone: string): Observable<Customer[]> {
    return this.api.get<Customer[]>(`${this.endpoint}/searchByPhone?phone=${phone}`);
  }
}
