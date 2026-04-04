using DAL.Data;
using DAL.Repositories;
using Model.Entities;

namespace OnlineShop.DAL.Repositories
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(AppDbContext context) : base(context) { }
    }
}
