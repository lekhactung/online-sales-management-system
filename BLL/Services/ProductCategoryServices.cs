using BLL.Services;
using Model.DTOs;
using Model.Entities;
using OnlineShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductCategoryServices : IProductCategoryServices
    {
        private readonly IProductCategoryRepository _repo;

        public ProductCategoryServices(IProductCategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return entities.Select(c => new ProductCategoryDto 
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            });
        }

        public async Task<string> CreateAsync(CreateProductCategoryDto dto)
        {
            var entity = new ProductCategory
            {
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = dto.CategoryName
            };
            await _repo.CreateAsync(entity);
            return entity.CategoryId;
        }
    }
}
