using Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ICustomerServices
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> GetByIdAsync(string id);
        Task<string> CreateAsync(CreateCustomerDto createDto);
        Task<bool> UpdateAsync(string id, CustomerDto updateDto);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable<CustomerDto>> SearchByNameAsync(string name);
        Task<IEnumerable<CustomerDto>> SearchByPhoneAsync(string phone);
    }
}
