import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

export interface AdminAccount {
  AdminId: number;
  Username: string;
  FullName: string;
  Role: string;
}

@Component({
  selector: 'app-admin-list',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="card bg-dark text-white shadow-sm border-secondary mb-4">
      <div class="card-header border-secondary d-flex justify-content-between align-items-center py-3">
        <h5 class="mb-0 text-info fw-bold">
          <i class="bi bi-shield-lock-fill me-2"></i> Quản lý Admin
        </h5>
      </div>
      <div class="card-body p-0">
        <div class="table-responsive">
          <table class="table table-dark table-hover mb-0 align-middle">
            <thead class="table-dark text-white">
              <tr>
                <th class="ps-4">ID</th>
                <th>Username</th>
                <th>Full Name</th>
                <th>Role</th>
                <th class="text-end pe-4">Thao tác</th>
              </tr>
            </thead>
            <tbody>
              <tr *ngFor="let admin of admins">
                <td class="ps-4 fw-bold">#{{ admin.AdminId }}</td>
                <td>{{ admin.Username }}</td>
                <td>{{ admin.FullName }}</td>
                <td>
                  <span class="badge" [ngClass]="{
                    'bg-danger': admin.Role === 'SuperAdmin',
                    'bg-primary': admin.Role === 'ProductAdmin',
                    'bg-warning text-dark': admin.Role === 'OrderAdmin',
                    'bg-success': admin.Role === 'CustomerAdmin'
                  }">{{ admin.Role }}</span>
                </td>
                <td class="text-end pe-4">
                  <button class="btn btn-outline-danger btn-sm" (click)="deleteAdmin(admin.AdminId)" [disabled]="admin.Role === 'SuperAdmin'">
                    <i class="bi bi-trash"></i>
                  </button>
                </td>
              </tr>
              <tr *ngIf="admins.length === 0 && !isLoading">
                <td colspan="5" class="text-center py-4 text-muted">Không có dữ liệu</td>
              </tr>
              <tr *ngIf="isLoading">
                <td colspan="5" class="text-center py-4 text-info">
                  <div class="spinner-border spinner-border-sm me-2" role="status"></div>
                  Đang tải...
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  `
})
export class AdminListComponent implements OnInit {
  admins: AdminAccount[] = [];
  isLoading = false;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadAdmins();
  }

  loadAdmins() {
    this.isLoading = true;
    this.http.get<AdminAccount[]>(`${environment.apiUrl}/Admin`).subscribe({
      next: (data) => {
        this.admins = data;
        this.isLoading = false;
      },
      error: () => this.isLoading = false
    });
  }

  deleteAdmin(id: number) {
    if (confirm('Bạn có chắc chắn muốn xóa Admin này?')) {
      this.http.delete(`${environment.apiUrl}/Admin/${id}`).subscribe({
        next: () => this.loadAdmins(),
        error: (err) => alert('Lỗi khi xóa.')
      });
    }
  }
}
