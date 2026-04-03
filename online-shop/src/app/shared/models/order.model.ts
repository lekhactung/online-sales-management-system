export interface Order {
  OrderId: string;
  OrderDate: string;
  TotalAmount: number;
  CustomerId: string;
  CustomerName?: string;
  StatusId: string;
  StatusName?: string;
  OrderDetails?: OrderDetail[];
}

export interface OrderDetail {
  OrderId: string;
  ProductId: string;
  ProductName?: string;
  Quantity: number;
  UnitPrice: number;
}
