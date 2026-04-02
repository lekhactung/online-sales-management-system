import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
 
@Component({
  selector: 'app-root',
  standalone: true,   // ← BẮT BUỘC phải có
  // imports: khai báo những gì dùng trong TEMPLATE của component này
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  template: `
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
      <div class="container">
        <a class="navbar-brand" routerLink="/">OnlineShop</a>
        <div class="navbar-nav ms-auto">
          <a class="nav-link" routerLink="/products" routerLinkActive="active">
            San Pham
          </a>
          <a class="nav-link" routerLink="/orders" routerLinkActive="active">
            Don Hang
          </a>
          <a class="nav-link" routerLink="/customers" routerLinkActive="active">
            Khach Hang
          </a>
        </div>
      </div>
    </nav>
    <router-outlet></router-outlet>
  `
})
export class AppComponent {
  title = 'online-shop';
}
