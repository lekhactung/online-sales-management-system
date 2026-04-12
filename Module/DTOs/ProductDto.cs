using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTOs
{
    public class ProductDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CategoryId { get; set; } = string.Empty;
        public string? CategoryName { get; set; }
        public string? SupplierName { get; set; }
        public string? WarehouseName { get; set; }
        public int StockQuantity { get; set; }
    }

    public class CreateProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
        public string? SupplierId { get; set; }
        public string? WarehouseId { get; set; }
        public int StockQuantity { get; set; }
    }
}
