using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Entities;

namespace DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Bảng chính
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<AdminAccount> AdminAccounts { get; set; }

        // Views (Keyless)
        public DbSet<OrderInformationDto> ViewOrderInformation { get; set; }
        public DbSet<ProductRevenueDto> ViewProductRevenue { get; set; }
        public DbSet<CategoryRevenueDto> ViewProductCategoryRevenue { get; set; }
        public DbSet<MonthlySalesDto> ViewMonthlySalesReport { get; set; }
        public DbSet<LowStockDto> ViewLowStockQuantityAlert { get; set; }
        public DbSet<ShippingStatusDto> ViewShippingStatusSummary { get; set; }
        public DbSet<InventoryReportDto> ViewInventoryReport { get; set; }

        // Stored Procedure / Function result sets (Keyless)
        public DbSet<ProductDto> ProductDtoResults { get; set; }
        public DbSet<ProductPriceRangeDto> ProductPriceRangeResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tables
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Order>().ToTable("Orders", tb => tb.HasTrigger("trgUpdateTotalAmount"));
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail", tb =>
            {
                tb.HasTrigger("trgAfterInsertOrderDetail");
                tb.HasTrigger("trgAfterDeleteOrderDetail");
                tb.HasTrigger("trgUpdateTotalAmount");
            });
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<Warehouse>().ToTable("Warehouse");
            modelBuilder.Entity<Shipping>().ToTable("Shipping");
            modelBuilder.Entity<OrderStatus>().ToTable("OrderStatus");

            // Composite keys
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.ProductId });

            // Column types
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            // Views — Keyless Entity Types
            modelBuilder.Entity<OrderInformationDto>().HasNoKey().ToView("viewOrderInformation");
            modelBuilder.Entity<ProductRevenueDto>().HasNoKey().ToView("viewProductRevenue");
            modelBuilder.Entity<CategoryRevenueDto>().HasNoKey().ToView("viewProductCategoryRevenue");
            modelBuilder.Entity<MonthlySalesDto>().HasNoKey().ToView("viewMonthlySalesReport");
            modelBuilder.Entity<LowStockDto>().HasNoKey().ToView("viewLowStockQuantityAlert");
            modelBuilder.Entity<ShippingStatusDto>().HasNoKey().ToView("viewShippingStatusSummary");
            modelBuilder.Entity<InventoryReportDto>().HasNoKey().ToView("viewInventoryReport");

            // SP / Function result DTOs — Keyless (dùng với FromSqlRaw)
            modelBuilder.Entity<ProductDto>().HasNoKey();
            modelBuilder.Entity<ProductPriceRangeDto>().HasNoKey();

            // Seed Admins securely using BCrypt hashes
            modelBuilder.Entity<AdminAccount>().HasData(
                new AdminAccount { AdminId = 1, Username = "superadmin", PasswordHash = "$2b$10$BsLfKnfnrXX70kD1DAIQKuCdvxfjO5Ij75bmE9fNmgNrxK8T5/xYm", FullName = "Super Administrator", Role = "SuperAdmin" },
                new AdminAccount { AdminId = 2, Username = "productadmin", PasswordHash = "$2b$10$BsLfKnfnrXX70kD1DAIQKuCdvxfjO5Ij75bmE9fNmgNrxK8T5/xYm", FullName = "Product Manager", Role = "ProductAdmin" },
                new AdminAccount { AdminId = 3, Username = "orderadmin", PasswordHash = "$2b$10$BsLfKnfnrXX70kD1DAIQKuCdvxfjO5Ij75bmE9fNmgNrxK8T5/xYm", FullName = "Order Manager", Role = "OrderAdmin" },
                new AdminAccount { AdminId = 4, Username = "customeradmin", PasswordHash = "$2b$10$BsLfKnfnrXX70kD1DAIQKuCdvxfjO5Ij75bmE9fNmgNrxK8T5/xYm", FullName = "Customer Care", Role = "CustomerAdmin" }
            );
            // Seed OrderStatus
            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus { StatusId = "TT01", StatusName = "Chờ xác nhận" },
                new OrderStatus { StatusId = "TT02", StatusName = "Đang chuẩn bị hàng" },
                new OrderStatus { StatusId = "TT03", StatusName = "Đang giao hàng" },
                new OrderStatus { StatusId = "TT04", StatusName = "Đã giao" },
                new OrderStatus { StatusId = "TT05", StatusName = "Đã hủy" }
            );
        }
    }
}
