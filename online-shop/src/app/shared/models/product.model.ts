
// Interface tương ứng với ProductDto từ backend
export interface Product {
  ProductId: string;
  ProductName: string;
  Price: number;
  CategoryName?: string;
  SupplierName?: string;
  StockQuantity: number;
}
 
// Interface cho tạo mới sản phẩm
export interface CreateProduct {
  ProductName: string;
  Price: number;
  CategoryID: string;
  SupplierID: string;
  WarehouseID: string;
  StockQuantity: number;
}
