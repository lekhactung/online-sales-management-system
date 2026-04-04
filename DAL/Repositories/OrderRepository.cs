using DAL.Data;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.DAL.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersWithDetailsAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Status)
                .Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer != null ? o.Customer.LastName + " " + o.Customer.FirstName : null,
                    StatusId = o.StatusId,
                    StatusName = o.Status != null ? o.Status.StatusName : null
                })
                .ToListAsync();
        }

        public async Task<OrderDto?> GetOrderByIdWithDetailsAsync(string orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Status)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null) return null;

            return new OrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                CustomerId = order.CustomerId,
                CustomerName = order.Customer != null ? order.Customer.LastName + " " + order.Customer.FirstName : null,
                StatusId = order.StatusId,
                StatusName = order.Status != null ? order.Status.StatusName : null,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDto
                {
                    ProductId = od.ProductId,
                    ProductName = od.Product?.ProductName,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice
                }).ToList()
            };
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(string customerId)
        {
            return await _context.Orders
                .Include(o => o.Status)
                .Where(o => o.CustomerId == customerId)
                .Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    CustomerId = o.CustomerId,
                    StatusId = o.StatusId,
                    StatusName = o.Status != null ? o.Status.StatusName : null
                })
                .ToListAsync();
        }
    }
}
