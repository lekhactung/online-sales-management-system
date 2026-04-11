using DAL.Repositories;
using Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReportServices : IReportServices
    {
        private readonly IReportRepository _repo;

        public ReportServices(IReportRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<OrderInformationDto>> GetOrderInformationAsync()
            => _repo.GetOrderInformationAsync();

        public Task<IEnumerable<ProductRevenueDto>> GetProductRevenueAsync()
            => _repo.GetProductRevenueAsync();

        public Task<IEnumerable<CategoryRevenueDto>> GetCategoryRevenueAsync()
            => _repo.GetCategoryRevenueAsync();

        public Task<IEnumerable<MonthlySalesDto>> GetMonthlySalesAsync()
            => _repo.GetMonthlySalesAsync();

        public Task<IEnumerable<LowStockDto>> GetLowStockAlertAsync()
            => _repo.GetLowStockAlertAsync();

        public Task<IEnumerable<ShippingStatusDto>> GetShippingStatusAsync()
            => _repo.GetShippingStatusAsync();

        public Task<IEnumerable<InventoryReportDto>> GetInventoryReportAsync()
            => _repo.GetInventoryReportAsync();

        public Task<IEnumerable<ProductPriceRangeDto>> SearchProductByPriceAsync(decimal minPrice, decimal maxPrice)
            => _repo.SearchProductByPriceAsync(minPrice, maxPrice);

        public TotalWithVatDto CalculateTotalWithVat(decimal amount, int vatPercent)
        {
            var total = _repo.CalculateTotalWithVat(amount, vatPercent);
            return new TotalWithVatDto
            {
                OriginalAmount = amount,
                VatPercent = vatPercent,
                TotalWithVat = total
            };
        }
    }
}
