// DAL/Repositories/ProductRepository.cs
using DAL.Data;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Entities;
using DAL.Data;
using Model.DTOs;
using Model.Entities;

namespace OnlineShop.DAL.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        // LINQ — JOIN với Category và Supplier, chiếu ra DTO
        public async Task<IEnumerable<ProductDto>> GetAllWithDetailsAsync()
        {
            return await _context.Products
                .Include(p => p.Category)    // EF tự JOIN bảng ProductCategory
                .Include(p => p.Supplier)    // EF tự JOIN bảng Supplier
                .Select(p => new ProductDto  // Chiếu ra DTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    CategoryName = p.Category!.CategoryName,
                    SupplierName = p.Supplier!.SupplierName,
                    StockQuantity = p.StockQuantity
                })
                .ToListAsync();
        }

        // LINQ với Where — tìm kiếm theo tên
        public async Task<IEnumerable<ProductDto>> SearchByNameAsync(string keyword)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.ProductName.Contains(keyword))
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    CategoryName = p.Category!.CategoryName,
                    StockQuantity = p.StockQuantity
                })
                .ToListAsync();
        }
    }
}
