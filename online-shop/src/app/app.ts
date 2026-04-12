import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

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
      <div class="bg-dark text-white p-3 shadow d-flex flex-column"
           style="width: 250px; border-right: 1px solid #334155; flex-shrink: 0;">

        <div class="d-flex align-items-center mb-4 ps-2">
          <i class="bi bi-box-seam-fill fs-3 text-info me-2"></i>
          <span class="fs-4 fw-bold">Shop<span class="text-info">Manager</span></span>
        </div>

        <ul class="nav nav-pills flex-column gap-1">
          <li class="nav-item">
            <a routerLink="/products"
               routerLinkActive="bg-info text-dark fw-bold"
               class="nav-link text-white">
              <i class="bi bi-grid-1x2-fill me-2"></i> Sản phẩm
            </a>
          </li>
          <li class="nav-item">
            <a routerLink="/orders"
               routerLinkActive="bg-info text-dark fw-bold"
               class="nav-link text-white">
              <i class="bi bi-cart-fill me-2"></i> Đơn hàng
            </a>
          </li>
          <li class="nav-item">
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
        </ul>


        <!-- User section -->
        <div class="mt-auto border-top border-secondary pt-3">
          <div *ngIf="!isLoggedIn">
            <button class="btn btn-outline-info w-100 d-flex align-items-center justify-content-center gap-2"
                    (click)="login()">
              <i class="bi bi-box-arrow-in-right"></i> Đăng nhập
            </button>
          </div>
          <div *ngIf="isLoggedIn" class="d-flex flex-column gap-2">
            <div class="d-flex align-items-center gap-2 px-2 text-white">
              <div class="bg-info rounded-circle d-flex align-items-center justify-content-center text-dark fw-bold"
                   style="width:32px;height:32px;">
                {{ userName.charAt(0).toUpperCase() }}
              </div>
              <span class="small fw-bold text-truncate">{{ userName }}</span>
            </div>
            <button class="btn btn-outline-danger btn-sm w-100" (click)="logout()">
              <i class="bi bi-box-arrow-left me-1"></i> Đăng xuất
            </button>
          </div>
        </div>
      </div>

      <!-- ====== MAIN CONTENT — router-outlet renders feature pages ====== -->
      <div class="flex-grow-1 p-4 overflow-auto text-white">
        <router-outlet></router-outlet>
      </div>
    </div>
  `,
  styles: [`
    .nav-link { border-radius: 8px; transition: background 0.2s; }
    .nav-link:hover { background: rgba(255,255,255,0.1); }
  `]
})
export class AppComponent {
  title = 'online-shop';
  isLoggedIn = false;
  userName = 'Admin';

  login()  { this.isLoggedIn = true; }
  logout() { this.isLoggedIn = false; }
}