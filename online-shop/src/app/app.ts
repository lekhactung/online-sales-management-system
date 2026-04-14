import { Component, OnInit } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    CommonModule,
    FormsModule,
  ],
  template: `
    <div class="d-flex" style="min-height: 100vh; background-color: #0f172a;">
      <!-- ====== SIDEBAR NAVIGATION ====== -->
      <div *ngIf="isLoggedIn" class="bg-dark text-white p-3 shadow d-flex flex-column sidebar-nav"
           style="width: 250px; border-right: 1px solid #334155; flex-shrink: 0;">

        <div class="d-flex align-items-center mb-4 ps-2 brand-logo">
          <i class="bi bi-box-seam-fill fs-3 text-info me-2"></i>
          <span class="fs-4 fw-bold">Shop<span class="text-info">Manager</span></span>
        </div>

        <ul class="nav nav-pills flex-column gap-1">
          <li class="nav-item" *ngIf="hasRole(['ProductAdmin', 'SuperAdmin'])">
            <a routerLink="/products"
               routerLinkActive="bg-info text-dark fw-bold"
               class="nav-link text-white">
              <i class="bi bi-grid-1x2-fill me-2"></i> Sản phẩm
            </a>
          </li>
          <li class="nav-item" *ngIf="hasRole(['OrderAdmin', 'SuperAdmin'])">
            <a routerLink="/orders"
               routerLinkActive="bg-info text-dark fw-bold"
               class="nav-link text-white">
              <i class="bi bi-cart-fill me-2"></i> Đơn hàng
            </a>
          </li>
          <li class="nav-item" *ngIf="hasRole(['CustomerAdmin', 'SuperAdmin'])">
            <a routerLink="/customers"
               routerLinkActive="bg-info text-dark fw-bold"
               class="nav-link text-white">
              <i class="bi bi-people-fill me-2"></i> Khách hàng
            </a>
          </li>
          <li class="nav-item">
            <a routerLink="/reports"
               routerLinkActive="bg-info text-dark fw-bold"
               class="nav-link text-white">
              <i class="bi bi-graph-up me-2"></i> Báo cáo
            </a>
          </li>
          <li class="nav-item" *ngIf="hasRole('SuperAdmin')">
            <a routerLink="/admins"
               routerLinkActive="bg-info text-dark fw-bold"
               class="nav-link text-white">
              <i class="bi bi-shield-lock-fill me-2"></i> Quản lý Admin
            </a>
          </li>
        </ul>


        <!-- User section -->
        <div class="mt-auto border-top border-secondary pt-3 user-section">
          <div class="d-flex flex-column gap-2">
            <div class="d-flex align-items-center gap-2 px-2 text-white">
              <div class="bg-info rounded-circle d-flex align-items-center justify-content-center text-dark fw-bold"
                   style="width:32px;height:32px;">
                {{ (userName.charAt(0) || 'A').toUpperCase() }}
              </div>
              <div class="d-flex flex-column">
                  <span class="small fw-bold text-truncate lh-1">{{ fullName || userName }}</span>
                  <span class="text-info badge bg-dark align-self-start mt-1" style="font-size: 0.65rem;">{{ userRole }}</span>
              </div>
            </div>
            <button class="btn btn-outline-danger btn-sm w-100 mt-2" (click)="logout()">
              <i class="bi bi-box-arrow-left me-1"></i> Đăng xuất
            </button>
          </div>
        </div>
      </div>

      <!-- ====== MAIN CONTENT ====== -->
      <div class="flex-grow-1 overflow-auto text-white" [ngClass]="isLoggedIn ? 'p-4' : 'p-0'">
        <router-outlet></router-outlet>
      </div>
    </div>
  `,
  styles: [`
    .nav-link { border-radius: 8px; transition: background 0.2s; padding: 12px 16px; }
    .nav-link:hover { background: rgba(255,255,255,0.1); }
    .sidebar-nav {
      transition: all 0.3s ease;
      background: linear-gradient(180deg, #1e293b 0%, #0f172a 100%) !important;
    }
  `]
})
export class AppComponent implements OnInit {
  isLoggedIn = false;
  userName = '';
  fullName = '';
  userRole = '';

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.authService.currentUser$.subscribe(user => {
      this.isLoggedIn = !!user;
      if (user) {
        this.userName = user.Username || '';
        this.fullName = user.FullName || '';
        this.userRole = user.Role || '';
      }
    });
  }

  hasRole(role: string | string[]): boolean {
    return this.authService.hasRole(role);
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}