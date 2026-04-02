import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api';
import { ProductCategory, CreateProductCategory } from '../../shared/models/product-category.model';

@Injectable({ providedIn: 'root' })
export class ProductCategoryService {
  private endpoint = 'productcategory'; // C# ControllerRoute "[controller]" -> ProductCategory -> productcategory

  constructor(private api: ApiService) {}

  getAll(): Observable<ProductCategory[]> {
    return this.api.get<ProductCategory[]>(this.endpoint);
  }

  create(dto: CreateProductCategory): Observable<{createdId: string}> {
    // Backend trả về Object Ok(new { CreatedId = id });
    return this.api.post<{createdId: string}>(this.endpoint, dto);
  }
}
