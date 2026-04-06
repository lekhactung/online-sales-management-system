using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderStatusServices : IOrderStatusServices
    {
        private readonly AppDbContext _context;

        public OrderStatusServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderStatusDto>> GetAllAsync()
        {
            return await _context.OrderStatuses
                .Select(s => new OrderStatusDto
                {
                    StatusId   = s.StatusId,
                    StatusName = s.StatusName
                })
                .ToListAsync();
        }
    }
}
