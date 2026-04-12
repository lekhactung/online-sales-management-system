using DAL.Data;
using DAL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.DAL.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<ProductDto>> GetAllWithDetailsAsync()
        {
            return await _context.ProductDtoResults
                .FromSqlRaw("EXEC spGetAllProducts")
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

        public new async Task<bool> DeleteAsync(string productId)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC spDeleteProduct @ProductID",
                    new SqlParameter("@ProductID", productId)
                );
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
