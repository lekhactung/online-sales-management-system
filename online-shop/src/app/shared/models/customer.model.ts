export interface Customer {
  CustomerId?: string; 
  LastName: string;
  FirstName: string;
  Phone?: string;
  Email?: string;
  Address?: string;
}

export interface CreateCustomer {
  CustomerId?: string;
  LastName: string;
  FirstName: string;
  Phone?: string;
  Email?: string;
  Address?: string;
}
