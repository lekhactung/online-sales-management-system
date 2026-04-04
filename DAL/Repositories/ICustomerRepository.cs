using DAL.Repositories;
using Model.Entities;
using System.Threading.Tasks;

using System.Collections.Generic;
using Model.DTOs;

namespace OnlineShop.DAL.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<CustomerDto>> SearchByNameAsync(string name);
        Task<IEnumerable<CustomerDto>> SearchByPhoneAsync(string phone);
    }
}
