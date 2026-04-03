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
            decimal totalAmount = 0;
            var orderDetails = new List<OrderDetail>();

            // 1. Kiểm tra tồn kho trước và tính thành tiền
            foreach (var item in createDto.OrderDetails)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new Exception($"Sản phẩm mã {item.ProductId} không tồn tại.");

                if (product.StockQuantity < item.Quantity)
                    throw new Exception($"Sản phẩm '{product.ProductName}' không đủ tồn kho (Tồn: {product.StockQuantity}, Cần: {item.Quantity}).");

                totalAmount += item.Quantity * item.UnitPrice;

                orderDetails.Add(new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            // 2. Tạo Order
            var order = new Order
            {
                OrderId = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                OrderDate = DateTime.Now,
                CustomerId = createDto.CustomerId,
                StatusId = "PENDING",
                TotalAmount = totalAmount,
                OrderDetails = orderDetails
            };

            // 3. Tiến hành trừ lùi tồn kho (Cập nhật Db)
            foreach (var item in createDto.OrderDetails)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product != null)
                {
                    product.StockQuantity -= item.Quantity;
                    await _productRepository.UpdateAsync(product);
                }
            }

            // 4. Lưu đơn hàng
            await _orderRepository.CreateAsync(order);

            return order.OrderId;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(string customerId)
        {
            return await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
        }
    }
}
