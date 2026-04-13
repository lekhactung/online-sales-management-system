using DAL.Repositories;
using Model.DTOs;
using Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.DAL.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersWithDetailsAsync();
        Task<OrderDto?> GetOrderByIdWithDetailsAsync(string orderId);
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(string customerId);
        Task<Order?> GetOrderEntityWithDetailsAsync(string orderId);
        Task ClearOrderDetailsAsync(string orderId);
        Task<bool> UpdateOrderWithDetailsAsync(Order order, IEnumerable<OrderDetail> newDetails);
    }
}
