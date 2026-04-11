using Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IReportRepository
    {
        // Views
        Task<IEnumerable<OrderInformationDto>> GetOrderInformationAsync();
        Task<IEnumerable<ProductRevenueDto>> GetProductRevenueAsync();
        Task<IEnumerable<CategoryRevenueDto>> GetCategoryRevenueAsync();
        Task<IEnumerable<MonthlySalesDto>> GetMonthlySalesAsync();
        Task<IEnumerable<LowStockDto>> GetLowStockAlertAsync();
        Task<IEnumerable<ShippingStatusDto>> GetShippingStatusAsync();
        Task<IEnumerable<InventoryReportDto>> GetInventoryReportAsync();

        // Stored Procedure / Function
        Task<IEnumerable<ProductPriceRangeDto>> SearchProductByPriceAsync(decimal minPrice, decimal maxPrice);

        // Scalar function (computed in C#)
        decimal CalculateTotalWithVat(decimal amount, int vatPercent);
    }
}
