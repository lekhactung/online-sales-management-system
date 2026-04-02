using Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IProductCategoryServices
    {
        Task<IEnumerable<ProductCategoryDto>> GetAllAsync();
        Task<string> CreateAsync(CreateProductCategoryDto dto);
    }
}
