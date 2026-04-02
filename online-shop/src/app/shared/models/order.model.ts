export interface Order {
  OrderID: string;
  OrderDate: string;
  TotalAmount: number;
  CustomerID: string;
  CustomerName?: string;
  StatusID: string;
  StatusName?: string;
  OrderDetails?: OrderDetail[];
}
 
export interface OrderDetail {
  OrderID: string;
  ProductID: string;
  ProductName?: string;
  Quantity: number;
  UnitPrice: number;
}
