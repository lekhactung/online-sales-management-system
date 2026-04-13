import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/products', pathMatch: 'full' },
  {
    path: 'login',
    loadComponent: () =>
      import('./features/auth/login/login').then(m => m.LoginComponent)
  },
  {
    path: 'products',
    canActivate: [authGuard, roleGuard],
    data: { roles: ['ProductAdmin', 'SuperAdmin'] },
    loadComponent: () =>
      import('./features/products/product-list/product-list')
        .then(m => m.ProductListComponent)
  },
  {
    path: 'products/create',
    canActivate: [authGuard, roleGuard],
    data: { roles: ['ProductAdmin', 'SuperAdmin'] },
    loadComponent: () =>
      import('./features/products/product-form/product-form')
        .then(m => m.ProductFormComponent)
  },
  {
    path: 'products/edit/:id',
    canActivate: [authGuard, roleGuard],
    data: { roles: ['ProductAdmin', 'SuperAdmin'] },
    loadComponent: () =>
      import('./features/products/product-form/product-form')
        .then(m => m.ProductFormComponent)
  },
  {
    path: 'products/:id',
    canActivate: [authGuard, roleGuard],
    data: { roles: ['ProductAdmin', 'SuperAdmin'] },
    loadComponent: () =>
      import('./features/products/product-detail/product-detail')
        .then(m => m.ProductDetail)
  },
  {
    path: 'orders',
    canActivate: [authGuard, roleGuard],
    data: { roles: ['OrderAdmin', 'SuperAdmin'] },
    loadComponent: () =>
      import('./features/orders/order-list/order-list')
        .then(m => m.OrderListComponent)
  },
  {
    path: 'orders/create',
    canActivate: [authGuard, roleGuard],
    data: { roles: ['OrderAdmin', 'SuperAdmin'] },
    loadComponent: () =>
      import('./features/orders/order-form/order-form')
        .then(m => m.OrderFormComponent)
  },
  {
    path: 'orders/edit/:id',
    canActivate: [authGuard, roleGuard],
    data: { roles: ['OrderAdmin', 'SuperAdmin'] },
    loadComponent: () =>
      import('./features/orders/order-form/order-form')
        .then(m => m.OrderFormComponent)
  },
  {
    path: 'orders/:id',
    canActivate: [authGuard, roleGuard],
    data: { roles: ['OrderAdmin', 'SuperAdmin'] },
    loadComponent: () =>
      import('./features/orders/order-detail/order-detail')
        .then(m => m.OrderDetail)
  },
  {
    path: 'customers',
    canActivate: [authGuard, roleGuard],
    data: { roles: ['CustomerAdmin', 'SuperAdmin'] },
    loadComponent: () =>
      import('./features/customers/customer-list/customer-list')
        .then(m => m.CustomerListComponent)
  },
  {
    path: 'customers/create',
    canActivate: [authGuard, roleGuard],
    data: { roles: ['CustomerAdmin', 'SuperAdmin'] },
    loadComponent: () =>
      import('./features/customers/customer-form/customer-form')
        .then(m => m.CustomerFormComponent)
  },
  {
    path: 'customers/edit/:id',
    canActivate: [authGuard, roleGuard],
    data: { roles: ['CustomerAdmin', 'SuperAdmin'] },
    loadComponent: () =>
      import('./features/customers/customer-form/customer-form')
        .then(m => m.CustomerFormComponent)
  },
  {
    path: 'reports',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./features/reports/report-dashboard/report-dashboard')
        .then(m => m.ReportDashboardComponent)
  },
  {
    path: 'admins',
    canActivate: [authGuard, roleGuard],
    data: { roles: ['SuperAdmin'] },
    loadComponent: () =>
      import('./features/admins/admin-list/admin-list')
        .then(m => m.AdminListComponent)
  },
  { path: '**', redirectTo: '/products' }
];
