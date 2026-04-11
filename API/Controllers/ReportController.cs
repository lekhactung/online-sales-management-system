using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportServices _service;

        public ReportController(IReportServices service)
        {
            _service = service;
        }

        /// <summary>GET /api/report/orders — viewOrderInformation</summary>
        [HttpGet("orders")]
        public async Task<IActionResult> GetOrderInformation()
        {
            var result = await _service.GetOrderInformationAsync();
            return Ok(result);
        }

        /// <summary>GET /api/report/product-revenue — viewProductRevenue</summary>
        [HttpGet("product-revenue")]
        public async Task<IActionResult> GetProductRevenue()
        {
            var result = await _service.GetProductRevenueAsync();
            return Ok(result);
        }

        /// <summary>GET /api/report/category-revenue — viewProductCategoryRevenue</summary>
        [HttpGet("category-revenue")]
        public async Task<IActionResult> GetCategoryRevenue()
        {
            var result = await _service.GetCategoryRevenueAsync();
            return Ok(result);
        }

        /// <summary>GET /api/report/monthly-sales — viewMonthlySalesReport</summary>
        [HttpGet("monthly-sales")]
        public async Task<IActionResult> GetMonthlySales()
        {
            var result = await _service.GetMonthlySalesAsync();
            return Ok(result);
        }

        /// <summary>GET /api/report/low-stock — viewLowStockQuantityAlert</summary>
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStockAlert()
        {
            var result = await _service.GetLowStockAlertAsync();
            return Ok(result);
        }

        /// <summary>GET /api/report/shipping-status — viewShippingStatusSummary</summary>
        [HttpGet("shipping-status")]
        public async Task<IActionResult> GetShippingStatus()
        {
            var result = await _service.GetShippingStatusAsync();
            return Ok(result);
        }

        /// <summary>GET /api/report/inventory — viewInventoryReport</summary>
        [HttpGet("inventory")]
        public async Task<IActionResult> GetInventoryReport()
        {
            var result = await _service.GetInventoryReportAsync();
            return Ok(result);
        }

        /// <summary>GET /api/report/product-price-range?min=100000&amp;max=5000000 — funcSearchProductByPrice</summary>
        [HttpGet("product-price-range")]
        public async Task<IActionResult> SearchProductByPrice(
            [FromQuery] decimal min,
            [FromQuery] decimal max)
        {
            if (min < 0 || max < min)
                return BadRequest(new { message = "Khoảng giá không hợp lệ." });

            var result = await _service.SearchProductByPriceAsync(min, max);
            return Ok(result);
        }

        /// <summary>GET /api/report/total-with-vat?amount=1000000&amp;vat=10 — funcCalculateTotalWithVAT</summary>
        [HttpGet("total-with-vat")]
        public IActionResult CalculateTotalWithVat(
            [FromQuery] decimal amount,
            [FromQuery] int vat = 10)
        {
            if (amount <= 0)
                return BadRequest(new { message = "Số tiền phải lớn hơn 0." });
            if (vat < 0 || vat > 100)
                return BadRequest(new { message = "VAT phải trong khoảng 0-100%." });

            var result = _service.CalculateTotalWithVat(amount, vat);
            return Ok(result);
        }
    }
}
