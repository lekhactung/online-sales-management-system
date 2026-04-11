using Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IReportServices
    {
        Task<IEnumerable<OrderInformationDto>> GetOrderInformationAsync();
        Task<IEnumerable<ProductRevenueDto>> GetProductRevenueAsync();
        Task<IEnumerable<CategoryRevenueDto>> GetCategoryRevenueAsync();
        Task<IEnumerable<MonthlySalesDto>> GetMonthlySalesAsync();
        Task<IEnumerable<LowStockDto>> GetLowStockAlertAsync();
        Task<IEnumerable<ShippingStatusDto>> GetShippingStatusAsync();
        Task<IEnumerable<InventoryReportDto>> GetInventoryReportAsync();
        Task<IEnumerable<ProductPriceRangeDto>> SearchProductByPriceAsync(decimal minPrice, decimal maxPrice);
        TotalWithVatDto CalculateTotalWithVat(decimal amount, int vatPercent);
    }
}
