import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportService } from '../../../core/services/report.service';
import {
  OrderInformationDto,
  ProductRevenueDto,
  CategoryRevenueDto,
  MonthlySalesDto,
  LowStockDto,
  ShippingStatusDto,
  InventoryReportDto
} from '../../../core/models/report.model';

@Component({
  selector: 'app-report-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './report-dashboard.html',
})
export class ReportDashboardComponent implements OnInit {
  activeTab: string = 'productRevenue';

  productRevenue: ProductRevenueDto[] = [];
  categoryRevenue: CategoryRevenueDto[] = [];
  monthlySales: MonthlySalesDto[] = [];
  lowStock: LowStockDto[] = [];
  inventoryReport: InventoryReportDto[] = [];

  isLoading: boolean = false;
  errorMessage: string | null = null;

  constructor(private reportService: ReportService) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.isLoading = true;
    this.errorMessage = null;
    
    // We can load specific data when tab is clicked, but for simplicity we load some key ones here.
    if (this.activeTab === 'productRevenue') {
      this.reportService.getProductRevenue().subscribe({
        next: (res) => { this.productRevenue = res; this.isLoading = false; },
        error: (err) => { this.handleError(err); }
      });
    } else if (this.activeTab === 'categoryRevenue') {
      this.reportService.getCategoryRevenue().subscribe({
        next: (res) => { this.categoryRevenue = res; this.isLoading = false; },
        error: (err) => { this.handleError(err); }
      });
    } else if (this.activeTab === 'monthlySales') {
      this.reportService.getMonthlySales().subscribe({
        next: (res) => { this.monthlySales = res; this.isLoading = false; },
        error: (err) => { this.handleError(err); }
      });
    } else if (this.activeTab === 'stock') {
      this.reportService.getLowStock().subscribe({
        next: (res) => { this.lowStock = res; this.isLoading = false; },
        error: (err) => { this.handleError(err); }
      });
    } else if (this.activeTab === 'inventory') {
      this.reportService.getInventoryReport().subscribe({
        next: (res) => { this.inventoryReport = res; this.isLoading = false; },
        error: (err) => { this.handleError(err); }
      });
    }
  }

  setTab(tabId: string) {
    this.activeTab = tabId;
    this.loadData();
  }

  handleError(error: any) {
    console.error(error);
    this.errorMessage = 'Failed to load report data.';
    this.isLoading = false;
  }
}
