<div align="center">

<h1>🛒 Online Sales Management System</h1>

<p>Hệ thống quản lý bán hàng trực tuyến nội bộ — xây dựng với kiến trúc N-Layer hiện đại</p>

<p>
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white"/>
  <img src="https://img.shields.io/badge/Angular-19-DD0031?style=for-the-badge&logo=angular&logoColor=white"/>
  <img src="https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white"/>
  <img src="https://img.shields.io/badge/JWT-Auth-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white"/>
  <img src="https://img.shields.io/badge/EF%20Core-10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white"/>
</p>

<p>
  <img src="https://img.shields.io/badge/Nhóm-13-blue?style=flat-square"/>
  <img src="https://img.shields.io/badge/Môn-Lập%20trình%20CSDL-green?style=flat-square"/>
  <img src="https://img.shields.io/badge/HK-8%20·%202025--2026-orange?style=flat-square"/>
</p>

</div>

---

## 📖 Giới thiệu

**Online Sales Management System** là hệ thống quản lý vận hành bán hàng nội bộ (back-office), được xây dựng nhằm giải quyết bài toán thực tế mà các cửa hàng và doanh nghiệp thương mại điện tử quy mô vừa và nhỏ thường gặp phải: **quản lý thủ công, phân tán dữ liệu và thiếu công cụ giám sát tổng thể**.

Hệ thống cung cấp cho đội ngũ quản lý và nhân viên vận hành một **Dashboard tập trung**, cho phép kiểm soát toàn bộ chu trình bán hàng — từ quản lý danh mục sản phẩm, theo dõi tồn kho, xử lý đơn hàng, đến phân tích doanh thu — thông qua giao diện web hiện đại và bảo mật.

---

## Tính năng chính

| Module | Chức năng |
|---|---|
|  **Xác thực & Phân quyền** | Đăng nhập JWT, phân quyền theo 4 Role riêng biệt |
|  **Quản lý Sản phẩm** | CRUD sản phẩm & danh mục, theo dõi tồn kho thời gian thực |
|  **Quản lý Khách hàng** | CRUD khách hàng, tìm kiếm theo tên / số điện thoại |
|  **Quản lý Đơn hàng** | Tạo đơn, kiểm tra tồn kho 2 lớp, cập nhật trạng thái giao hàng |
|  **Báo cáo & Thống kê** | Doanh thu theo sản phẩm / danh mục / tháng, cảnh báo hàng sắp hết |
|  **Quản lý Tài khoản Admin** | SuperAdmin toàn quyền quản lý các tài khoản nội bộ |

---

##  Kiến trúc hệ thống

Dự án được tổ chức theo mô hình **N-Layer Architecture**, tách biệt hoàn toàn giữa giao diện, nghiệp vụ và dữ liệu:

```
online-sales-management-system/
│
├── online-shop/        # Presentation Layer  — Angular 19 (SPA)
│
├── API/                # API Gateway         — ASP.NET Core Web API
│   └── Controllers/    # Auth, Product, Order, Customer, Report, Admin
│
├── BLL/                # Business Logic Layer — Xử lý nghiệp vụ & kiểm duyệt
│   └── Services/
│
├── DAL/                # Data Access Layer   — EF Core + Repository Pattern
│   └── Repositories/
│
└── Module/             # Shared Domain       — Entities & DTOs dùng chung
```

**Luồng dữ liệu:**
```
Angular (Frontend)
    ↕ HTTP / JSON
ASP.NET Core API  →  BLL (Business Rules)  →  DAL (EF Core)  →  SQL Server
```



---

##  Công nghệ sử dụng

### Frontend
| | Công nghệ |
|---|---|
| Framework | Angular 19 — Standalone Components, Lazy Loading |
| Language | TypeScript 5.x (strict mode) |
| Styling | SCSS + Bootstrap 5 |
| HTTP | `HttpClient` + Functional Interceptor (tự động đính JWT) |
| Auth | `auth.guard.ts` + `role.guard.ts` (Route Guard) |
| Forms | Angular Reactive Forms |

### Backend
| | Công nghệ |
|---|---|
| Framework | ASP.NET Core 10.0 Web API |
| Language | C# 13 / .NET 10 |
| ORM | Entity Framework Core 10 + LINQ |
| Auth | JWT Bearer Token (BCrypt password hashing) |
| API Docs | Swagger / OpenAPI |
| DI | `AddScoped` — Repository & Service |

### Database
| | Công nghệ |
|---|---|
| DBMS | Microsoft SQL Server |
| Schema | 10 Tables · 7 Views · 3 Triggers · 4 Functions · 6 Stored Procedures |
| Transaction | `BEGIN TRAN / COMMIT / ROLLBACK` + `TRY...CATCH` |
| Security | EF Core parameterized queries — chống SQL Injection |

---

## Thiết kế Cơ sở dữ liệu

### Các bảng chính

| Bảng | Mô tả |
|---|---|
| `AdminAccount` | Tài khoản nội bộ (phân quyền theo Role) |
| `Customer` | Thông tin khách hàng |
| `Product` | Sản phẩm (liên kết Category, Supplier, Warehouse) |
| `ProductCategory` | Danh mục sản phẩm |
| `Supplier` | Nhà cung cấp |
| `Warehouse` | Kho hàng |
| `Orders` | Đơn hàng (TotalAmount tự tính bởi Trigger) |
| `OrderDetail` | Chi tiết đơn — Composite PK `(OrderId, ProductId)` |
| `OrderStatus` | Bảng trạng thái tĩnh |
| `Shipping` | Thông tin vận chuyển |


---

## Phân quyền

| Tính năng | SuperAdmin | ProductAdmin | OrderAdmin | CustomerAdmin |
|---|:---:|:---:|:---:|:---:|
| Quản lý Sản phẩm | ✅ | ✅ | | |
| Quản lý Đơn hàng | ✅ | | ✅ | |
| Quản lý Khách hàng | ✅ | | | ✅ |
| Xem Báo cáo | ✅ | ✅ | ✅ | ✅ |
| Quản lý Admin | ✅ | | | |

---

##  Hướng dẫn cài đặt

### Yêu cầu
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org/) & Angular CLI 19 (`npm i -g @angular/cli`)
- Microsoft SQL Server (LocalDB / Express / Full)

### 1. Clone dự án
```bash
git clone https://github.com/lekhactung/online-sales-management-system.git
cd online-sales-management-system
```

### 2. Tạo Database

```bash
# Chạy lần lượt trong SQL Server Management Studio:
# 1. OnlineSalesDatabase.sql   — Tạo bảng & dữ liệu mẫu
# 2. View.sql
# 3. Function.sql
# 4. Trigger.sql
# 5. StoreProcedure.sql
```

> Hoặc dùng EF Core Migration (tự động tạo schema + seed data):
> ```bash
> cd API && dotnet ef database update
> ```

### 3. Cấu hình Backend

Chỉnh `API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=OnlineSales;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "your-secret-key-min-32-chars",
    "Issuer": "OnlineSalesAPI",
    "Audience": "OnlineSalesClient"
  }
}
```

### 4. Chạy Backend
```bash
cd API
dotnet run
```
> API: `http://localhost:5206` · Swagger: `http://localhost:5206/swagger`

### 5. Chạy Frontend
```bash
cd online-shop
npm install
ng serve
```
> Angular: `http://localhost:4200`

---

## Nhóm thực hiện

| Họ tên | MSSV | Đóng góp |
|---|---|---|
| Phạm Minh Quân | 2351010176 | Database design, T-SQL Scripts, viết báo cáo |
| Lê Khắc Tùng | 2351010238 | Backend API, Frontend Angular, System Architecture |

**Giảng viên hướng dẫn:** Phạm Hoài An

---

<div align="center">
  <sub> Đồ án môn học: Lập trình Cơ sở Dữ liệu · HK8 · 2025–2026 · Nhóm 13</sub>
</div>
