using DAL.Data;
using DAL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System.Threading.Tasks;

namespace OnlineShop.DAL.Repositories
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(AppDbContext context) : base(context) { }

        public new async Task<ProductCategory> CreateAsync(ProductCategory category)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC spAddCategory @CategoryID, @CategoryName",
                new SqlParameter("@CategoryID", category.CategoryId),
                new SqlParameter("@CategoryName", category.CategoryName ?? (object)System.DBNull.Value)
            );
            return category;
        }

        public new async Task<ProductCategory> UpdateAsync(ProductCategory category)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC spUpdateCategory @CategoryID, @CategoryName",
                new SqlParameter("@CategoryID", category.CategoryId),
                new SqlParameter("@CategoryName", category.CategoryName ?? (object)System.DBNull.Value)
            );
            return category;
        }

        public new async Task<bool> DeleteAsync(string categoryId)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC spDeleteCategory @CategoryID",
                    new SqlParameter("@CategoryID", categoryId)
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
