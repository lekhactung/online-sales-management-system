using System;

namespace Model.DTOs
{
    // viewOrderInformation
    public class OrderInformationDto
    {
        public string OrderId { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
        public string StatusName { get; set; } = string.Empty;
    }

    // viewProductRevenue
    public class ProductRevenueDto
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int TotalSold { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    // viewProductCategoryRevenue
    public class CategoryRevenueDto
    {
        public string CategoryId { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int TotalProductsSold { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    // viewMonthlySalesReport
    public class MonthlySalesDto
    {
        public int SalesYear { get; set; }
        public int SalesMonth { get; set; }
        public int TotalOrders { get; set; }
        public decimal MonthlyRevenue { get; set; }
    }

    // viewLowStockQuantityAlert
    public class LowStockDto
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public string WarehouseName { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public string SupplierPhone { get; set; } = string.Empty;
        public string ProductStatus { get; set; } = string.Empty;
    }

    // viewShippingStatusSummary
    public class ShippingStatusDto
    {
        public string OrderId { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public string? ShippingCompany { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public decimal? ShippingFee { get; set; }
        public string CustomerName { get; set; } = string.Empty;
    }

    // viewInventoryReport
    public class InventoryReportDto
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public string? CategoryName { get; set; }
        public string? SupplierName { get; set; }
        public string? WarehouseName { get; set; }
        public string StockStatus { get; set; } = string.Empty;
    }

    // funcSearchProductByPrice result
    public class ProductPriceRangeDto
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

    // funcCalculateTotalWithVAT result
    public class TotalWithVatDto
    {
        public decimal OriginalAmount { get; set; }
        public int VatPercent { get; set; }
        public decimal TotalWithVat { get; set; }
    }
}
