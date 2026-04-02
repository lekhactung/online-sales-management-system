using DAL.Data;
using DAL.Repositories;
using Model.Entities;

using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.DAL.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<CustomerDto>> SearchByNameAsync(string name)
        {
            return await _context.Customers
                .Where(c => c.FirstName.Contains(name) || c.LastName.Contains(name))
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    LastName = c.LastName,
                    FirstName = c.FirstName,
                    Phone = c.Phone,
                    Email = c.Email,
                    Address = c.Address
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CustomerDto>> SearchByPhoneAsync(string phone)
        {
            return await _context.Customers
                .Where(c => c.Phone != null && c.Phone.Contains(phone))
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    LastName = c.LastName,
                    FirstName = c.FirstName,
                    Phone = c.Phone,
                    Email = c.Email,
                    Address = c.Address
                })
                .ToListAsync();
        }
    }
}
