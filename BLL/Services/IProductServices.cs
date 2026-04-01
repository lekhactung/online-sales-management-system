using Model.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(string id);
        Task<ProductDto?> CreateAsync(CreateProductDto dto);
        Task<bool> DeleteAsync (string id);
        Task<IEnumerable<ProductDto>> SearchAsync(string keyword);

    }
}
