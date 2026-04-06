import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: '/products', pathMatch: 'full' },
  {
    path: 'products',
    loadComponent: () =>
      import('./features/products/product-list/product-list')
        .then(m => m.ProductListComponent)
  },
  {
    path: 'products/create',
    loadComponent: () =>
      import('./features/products/product-form/product-form')
        .then(m => m.ProductFormComponent)
  },
  {
    path: 'products/edit/:id',
    loadComponent: () =>
      import('./features/products/product-form/product-form')
        .then(m => m.ProductFormComponent)
  },
  {
    path: 'products/:id',
    loadComponent: () =>
      import('./features/products/product-detail/product-detail')
        .then(m => m.ProductDetail)
  },
  {
    path: 'orders',
    loadComponent: () =>
      import('./features/orders/order-list/order-list')
        .then(m => m.OrderListComponent)
  },
  {
    path: 'orders/create',
    loadComponent: () =>
      import('./features/orders/order-form/order-form')
        .then(m => m.OrderFormComponent)
  },
  {
    path: 'orders/edit/:id',
    loadComponent: () =>
      import('./features/orders/order-form/order-form')
        .then(m => m.OrderFormComponent)
  },
  {
    path: 'orders/:id',
    loadComponent: () =>
      import('./features/orders/order-detail/order-detail')
        .then(m => m.OrderDetail)
  },
  {
    path: 'customers',
    loadComponent: () =>
      import('./features/customers/customer-list/customer-list')
        .then(m => m.CustomerListComponent)
  },
  {
    path: 'customers/create',
    loadComponent: () =>
      import('./features/customers/customer-form/customer-form')
        .then(m => m.CustomerFormComponent)
  },
  {
    path: 'customers/edit/:id',
    loadComponent: () =>
      import('./features/customers/customer-form/customer-form')
        .then(m => m.CustomerFormComponent)
  },
  { path: '**', redirectTo: '/products' }
];
