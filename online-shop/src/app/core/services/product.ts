// src/app/core/services/product.service.ts
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api';
import { Product, CreateProduct } from '../../shared/models/product.model';
 
@Injectable({ providedIn: 'root' })
export class ProductService {
  private endpoint = 'product';
 
  constructor(private api: ApiService) {}
 
  getAll(): Observable<Product[]> {
    return this.api.get<Product[]>(this.endpoint);
  }
 
  getById(id: number): Observable<Product> {
    return this.api.get<Product>(`${this.endpoint}/${id}`);
  }
 
  search(keyword: string): Observable<Product[]> {
    return this.api.get<Product[]>(`${this.endpoint}/search?keyword=${keyword}`);
  }
 
  create(dto: CreateProduct): Observable<Product> {
    return this.api.post<Product>(this.endpoint, dto);
  }
 
  update(id: number, dto: Partial<Product>): Observable<Product> {
    return this.api.put<Product>(`${this.endpoint}/${id}`, dto);
  }
 
  delete(id: string): Observable<void> {
    return this.api.delete<void>(`${this.endpoint}/${id}`);
  }
 
  getAsXml(): Observable<string> {
    return this.api.getAsXml(this.endpoint);
  }
}
