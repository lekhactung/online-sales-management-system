using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTOs
{
    //Date transfer objects
    //DTO là class trung gian — thay vì trả nguyên Entity (có thể vòng lặp vô tận), ta trả DTO gọn gàng hơn
    //DTO cho read ( trả về cho angular)
    public class ProductDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? CategoryName { get; set; }   // Lấy từ bảng Category
        public string? SupplierName { get; set; }   // Lấy từ bảng Supplier
        public int StockQuantity { get; set; }
    }
    //DTO cho create ( nhận từ angular khi thêm mới)
    public class CreateProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
        public string SupplierId { get; set; }
        public string WarehouseId { get; set; }
        public int StockQuantity { get; set; }

    }
}
