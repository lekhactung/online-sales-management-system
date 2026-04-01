using DAL.Repositories;
using Model.DTOs;
// DAL/Repositories/IProductRepository.cs
using Model.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.DAL.Repositories
{
    public interface IProductRepository : IRepository<Model.Entities.Product>
    {
        // Thêm method đặc biệt cho Product
        Task<IEnumerable<ProductDto>> GetAllWithDetailsAsync();
        Task<IEnumerable<ProductDto>> SearchByNameAsync(string keyword);
    }
}
