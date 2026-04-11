using DAL.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        // ==================== VIEWS ====================

        public async Task<IEnumerable<OrderInformationDto>> GetOrderInformationAsync()
        {
            return await _context.ViewOrderInformation.ToListAsync();
        }

        public async Task<IEnumerable<ProductRevenueDto>> GetProductRevenueAsync()
        {
            return await _context.ViewProductRevenue.ToListAsync();
        }

        public async Task<IEnumerable<CategoryRevenueDto>> GetCategoryRevenueAsync()
        {
            return await _context.ViewProductCategoryRevenue.ToListAsync();
        }

        public async Task<IEnumerable<MonthlySalesDto>> GetMonthlySalesAsync()
        {
            return await _context.ViewMonthlySalesReport.ToListAsync();
        }

        public async Task<IEnumerable<LowStockDto>> GetLowStockAlertAsync()
        {
            return await _context.ViewLowStockQuantityAlert.ToListAsync();
        }

        public async Task<IEnumerable<ShippingStatusDto>> GetShippingStatusAsync()
        {
            return await _context.ViewShippingStatusSummary.ToListAsync();
        }

        public async Task<IEnumerable<InventoryReportDto>> GetInventoryReportAsync()
        {
            return await _context.ViewInventoryReport.ToListAsync();
        }

        // ==================== FUNCTIONS ====================

        // funcSearchProductByPrice — Table-valued function
        public async Task<IEnumerable<ProductPriceRangeDto>> SearchProductByPriceAsync(decimal minPrice, decimal maxPrice)
        {
            return await _context.ProductPriceRangeResults
                .FromSqlRaw(
                    "SELECT * FROM dbo.funcSearchProductByPrice(@MinPrice, @MaxPrice)",
                    new SqlParameter("@MinPrice", minPrice),
                    new SqlParameter("@MaxPrice", maxPrice))
                .ToListAsync();
        }

        // funcCalculateTotalWithVAT — Scalar: computed in C# to avoid extra round-trip
        public decimal CalculateTotalWithVat(decimal amount, int vatPercent)
        {
            return amount * (1 + vatPercent / 100.0m);
        }
    }
}
