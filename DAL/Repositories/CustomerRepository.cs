using DAL.Data;
using DAL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.DAL.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context) { }

        public new async Task<Customer> CreateAsync(Customer customer)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC spAddCustomer @CustomerID, @LastName, @FirstName, @Phone, @Email, @Address",
                new SqlParameter("@CustomerID", customer.CustomerId),
                new SqlParameter("@LastName", customer.LastName ?? (object)System.DBNull.Value),
                new SqlParameter("@FirstName", customer.FirstName ?? (object)System.DBNull.Value),
                new SqlParameter("@Phone", customer.Phone ?? (object)System.DBNull.Value),
                new SqlParameter("@Email", customer.Email ?? (object)System.DBNull.Value),
                new SqlParameter("@Address", customer.Address ?? (object)System.DBNull.Value)
            );
            return customer;
        }

        public new async Task<Customer> UpdateAsync(Customer customer)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC spUpdateCustomer @CustomerID, @LastName, @FirstName, @Phone, @Email, @Address",
                new SqlParameter("@CustomerID", customer.CustomerId),
                new SqlParameter("@LastName", customer.LastName ?? (object)System.DBNull.Value),
                new SqlParameter("@FirstName", customer.FirstName ?? (object)System.DBNull.Value),
                new SqlParameter("@Phone", customer.Phone ?? (object)System.DBNull.Value),
                new SqlParameter("@Email", customer.Email ?? (object)System.DBNull.Value),
                new SqlParameter("@Address", customer.Address ?? (object)System.DBNull.Value)
            );
            return customer;
        }

        public new async Task<bool> DeleteAsync(string customerId)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC spDeleteCustomer @CustomerID",
                    new SqlParameter("@CustomerID", customerId)
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

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
