
export interface Product {
  ProductId: string;
  ProductName: string;
  Price: number;
  CategoryName?: string;
  SupplierName?: string;
  StockQuantity: number;
}

export interface CreateProduct {
  ProductName: string;
  Price: number;
  CategoryId: string;
  SupplierId?: string;
  WarehouseId?: string;
  StockQuantity: number;
}
