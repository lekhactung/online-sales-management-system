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

export interface CreateOrderDetail {
  ProductId: string;
  Quantity: number;
  UnitPrice: number;
}

export interface CreateOrder {
  CustomerId: string;
  OrderDetails: CreateOrderDetail[];
}

export interface UpdateOrder {
  CustomerId: string;
  StatusId: string;
  OrderDetails: CreateOrderDetail[];
}
