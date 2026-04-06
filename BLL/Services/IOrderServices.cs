using Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IOrderServices
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto?> GetByIdAsync(string id);
        Task<string> CreateOrderAsync(CreateOrderDto createDto);
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(string customerId);
        Task<bool> UpdateOrderAsync(string id, UpdateOrderDto updateDto);
        Task<bool> UpdateOrderStatusAsync(string id, string statusId);
        Task<bool> DeleteOrderAsync(string id);
    }
}
