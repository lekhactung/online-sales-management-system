using BLL.Services;
using Model.DTOs;
using Model.Entities;
using OnlineShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;

        public OrderServices(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            return await _orderRepository.GetAllOrdersWithDetailsAsync();
        }

        public async Task<OrderDto?> GetByIdAsync(string id)
        {
            return await _orderRepository.GetOrderByIdWithDetailsAsync(id);
        }

        public async Task<string> CreateOrderAsync(CreateOrderDto createDto)
        {
            decimal totalAmount = 0;
            var orderDetails = new List<OrderDetail>();

            foreach (var item in createDto.OrderDetails)
            {
                totalAmount += item.Quantity * item.UnitPrice;
                orderDetails.Add(new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            var order = new Order
            {
                OrderId = Guid.NewGuid().ToString(),
                OrderDate = DateTime.Now,
                CustomerId = createDto.CustomerId,
                StatusId = "PENDING", // Giả định trạng thái mặc định
                TotalAmount = totalAmount,
                OrderDetails = orderDetails
            };

            await _orderRepository.CreateAsync(order);
            return order.OrderId;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(string customerId)
        {
            return await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
        }
    }
}
