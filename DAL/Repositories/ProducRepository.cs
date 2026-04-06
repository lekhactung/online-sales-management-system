using DAL.Data;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Entities;

namespace OnlineShop.DAL.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<ProductDto>> GetAllWithDetailsAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.CategoryName : null,
                    SupplierName = p.Supplier != null ? p.Supplier.SupplierName : null,
                    StockQuantity = p.StockQuantity
                })
                .ToListAsync();
        }

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
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.CategoryName : null,
                    StockQuantity = p.StockQuantity
                })
                .ToListAsync();
        }
    }
}
