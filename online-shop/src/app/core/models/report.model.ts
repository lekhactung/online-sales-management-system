export interface OrderInformationDto {
  OrderId: string;
  OrderDate: string; // ISO Datetime
  CustomerName: string;
  ProductName: string;
  Quantity: number;
  UnitPrice: number;
  SubTotal: number;
  StatusName: string;
}

export interface ProductRevenueDto {
  ProductId: string;
  ProductName: string;
  TotalSold: number;
  TotalRevenue: number;
}

export interface CategoryRevenueDto {
  CategoryId: string;
  CategoryName: string;
  TotalProductsSold: number;
  TotalQuantity: number;
  TotalRevenue: number;
}

export interface MonthlySalesDto {
  SalesYear: number;
  SalesMonth: number;
  TotalOrders: number;
  MonthlyRevenue: number;
}

export interface LowStockDto {
  ProductId: string;
  ProductName: string;
  StockQuantity: number;
  WarehouseName: string;
  SupplierName: string;
  SupplierPhone: string;
  ProductStatus: string;
}

export interface ShippingStatusDto {
  OrderId: string;
  OrderDate: string;
  StatusName: string;
  ShippingCompany?: string;
  EstimatedDeliveryDate?: string;
  ShippingFee?: number;
  CustomerName: string;
}

export interface InventoryReportDto {
  ProductId: string;
  ProductName: string;
  StockQuantity: number;
  CategoryName?: string;
  SupplierName?: string;
  WarehouseName?: string;
  StockStatus: string;
}
