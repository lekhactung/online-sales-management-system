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
        private readonly IProductRepository _productRepository;

        public OrderServices(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
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
            var orderDetails = new List<OrderDetail>();

            foreach (var item in createDto.OrderDetails)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new Exception($"Sản phẩm mã {item.ProductId} không tồn tại.");

                if (product.StockQuantity < item.Quantity)
                    throw new Exception($"Sản phẩm '{product.ProductName}' không đủ tồn kho (Tồn: {product.StockQuantity}, Cần: {item.Quantity}).");

                orderDetails.Add(new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            string newId = await _orderRepository.GenerateNextIdAsync("DH", o => o.OrderId);
            
            foreach (var od in orderDetails)
            {
                od.OrderId = newId;
            }

            var order = new Order
            {
                OrderId = newId,
                OrderDate = DateTime.Now,
                CustomerId = createDto.CustomerId,
                StatusId = string.IsNullOrEmpty(createDto.StatusId) ? "TT01" : createDto.StatusId,
                TotalAmount = orderDetails.Sum(d => d.Quantity * d.UnitPrice),
                OrderDetails = orderDetails
            };

            await _orderRepository.CreateAsync(order);

            return order.OrderId;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(string customerId)
        {
            return await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
        }

        public async Task<bool> UpdateOrderStatusAsync(string id, string statusId)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return false;

            order.StatusId = statusId;
            var updated = await _orderRepository.UpdateAsync(order);
            return updated != null;
        }

        public async Task<bool> UpdateOrderAsync(string id, UpdateOrderDto dto)
        {
            var order = await _orderRepository.GetOrderEntityWithDetailsAsync(id);
            if (order == null) return false;

            await _orderRepository.ClearOrderDetailsAsync(id);

            order.CustomerId = dto.CustomerId;
            if (!string.IsNullOrEmpty(dto.StatusId))
                order.StatusId = dto.StatusId;

            var newDetails = new List<OrderDetail>();

            foreach (var item in dto.OrderDetails)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null) throw new Exception($"Sản phẩm {item.ProductId} không tồn tại.");
                
                // Trigger after deletion of previous order restored original stock. 
                // We check if it is enough for the new requested quantity.
                if (product.StockQuantity < item.Quantity)
                    throw new Exception($"Sản phẩm '{product.ProductName}' không đủ tồn kho (Tồn: {product.StockQuantity}, Yêu cầu: {item.Quantity}).");

                newDetails.Add(new OrderDetail
                {
                    OrderId   = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity  = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            order.TotalAmount = newDetails.Sum(d => d.Quantity * d.UnitPrice);

            var updated = await _orderRepository.UpdateOrderWithDetailsAsync(order, newDetails);
            return updated;
        }

        public async Task<bool> DeleteOrderAsync(string id)
        {
            var order = await _orderRepository.GetOrderEntityWithDetailsAsync(id);
            if (order == null) return false;

            // Remove OrderDetails first to trigger stock restore and avoid FK constraint 
            await _orderRepository.ClearOrderDetailsAsync(id);

            // Remove Order
            var result = await _orderRepository.DeleteAsync(id);
            return result;
        }
    }
}
