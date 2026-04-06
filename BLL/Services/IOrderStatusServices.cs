using Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IOrderStatusServices
    {
        Task<IEnumerable<OrderStatusDto>> GetAllAsync();
    }
}
