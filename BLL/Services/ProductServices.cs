using DAL.Repositories;
using Model.DTOs;
using Model.Entities;
using OnlineShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _repo;

        public ProductServices(IProductRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<ProductDto>> GetAllAsync()
            => _repo.GetAllWithDetailsAsync();

        public async Task<ProductDto?> GetByIdAsync(string id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return null;

            return new ProductDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                StockQuantity = p.StockQuantity
            };
        }

        public async Task<ProductDto?> CreateAsync(CreateProductDto dto)
        {
            if (dto.Price <= 0)
                throw new ArgumentException("Giá phải lớn hơn 0");

            var entity = new Product
            {
                ProductId = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                ProductName = dto.ProductName,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                SupplierId = dto.SupplierId,
                WarehouseId = dto.WarehouseId,
                StockQuantity = dto.StockQuantity
            };

            await _repo.CreateAsync(entity);

            return new ProductDto
            {
                ProductId = entity.ProductId,
                ProductName = entity.ProductName,
                Price = entity.Price,
                StockQuantity = entity.StockQuantity
            };
        }

        public async Task<bool> UpdateAsync(string id, ProductDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.ProductName = dto.ProductName;
            existing.Price = dto.Price;
            existing.StockQuantity = dto.StockQuantity;

            var updated = await _repo.UpdateAsync(existing);
            return updated != null;
        }

        public Task<bool> DeleteAsync(string id)
            => _repo.DeleteAsync(id);

        public Task<IEnumerable<ProductDto>> SearchAsync(string keyword)
            => _repo.SearchByNameAsync(keyword);
    }
}