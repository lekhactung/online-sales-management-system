// src/app/app.routes.ts
import { Routes } from '@angular/router';
 
export const routes: Routes = [
  // Trang chủ → redirect sang /products
  { path: '', redirectTo: '/products', pathMatch: 'full' },
 
  // Lazy loading — chỉ load component khi người dùng vào trang
  // Cú pháp MỚI: loadComponent (không phải loadChildren như Angular cũ)
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
    path: 'customers',
    loadComponent: () =>
      import('./features/customers/customer-list/customer-list')
        .then(m => m.CustomerListComponent)
  },
  { path: '**', redirectTo: '/products' }
];
