using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Entities
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
        public string? SupplierId { get; set; }
        public string? WarehouseId { get; set; }
        public int StockQuantity { get; set; }

        public ProductCategory? Category { get; set; }
        public Supplier? Supplier { get; set; }
        public Warehouse? Warehouse { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
