import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  OrderInformationDto,
  ProductRevenueDto,
  CategoryRevenueDto,
  MonthlySalesDto,
  LowStockDto,
  ShippingStatusDto,
  InventoryReportDto
} from '../models/report.model';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  private apiUrl = `${environment.apiUrl}/Report`;

  constructor(private http: HttpClient) { }


  getOrders(): Observable<OrderInformationDto[]> {
    return this.http.get<OrderInformationDto[]>(`${this.apiUrl}/orders`);
  }

  getProductRevenue(): Observable<ProductRevenueDto[]> {
    return this.http.get<ProductRevenueDto[]>(`${this.apiUrl}/product-revenue`);
  }

  getCategoryRevenue(): Observable<CategoryRevenueDto[]> {
    return this.http.get<CategoryRevenueDto[]>(`${this.apiUrl}/category-revenue`);
  }

  getMonthlySales(): Observable<MonthlySalesDto[]> {
    return this.http.get<MonthlySalesDto[]>(`${this.apiUrl}/monthly-sales`);
  }

  getLowStock(): Observable<LowStockDto[]> {
    return this.http.get<LowStockDto[]>(`${this.apiUrl}/low-stock`);
  }

  getShippingStatus(): Observable<ShippingStatusDto[]> {
    return this.http.get<ShippingStatusDto[]>(`${this.apiUrl}/shipping-status`);
  }

  getInventoryReport(): Observable<InventoryReportDto[]> {
    return this.http.get<InventoryReportDto[]>(`${this.apiUrl}/inventory`);
  }
}
