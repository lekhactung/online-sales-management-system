using BLL.Services;
using Model.DTOs;
using Model.Entities;
using OnlineShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerServices(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
                LastName = c.LastName,
                FirstName = c.FirstName,
                Phone = c.Phone,
                Email = c.Email,
                Address = c.Address
            });
        }

        public async Task<CustomerDto?> GetByIdAsync(string id)
        {
            var c = await _customerRepository.GetByIdAsync(id);
            if (c == null) return null;

            return new CustomerDto
            {
                CustomerId = c.CustomerId,
                LastName = c.LastName,
                FirstName = c.FirstName,
                Phone = c.Phone,
                Email = c.Email,
                Address = c.Address
            };
        }

        public async Task<string> CreateAsync(CreateCustomerDto createDto)
        {
            var customer = new Customer
            {
                CustomerId = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                LastName = createDto.LastName,
                FirstName = createDto.FirstName,
                Phone = createDto.Phone,
                Email = createDto.Email,
                Address = createDto.Address
            };

            await _customerRepository.CreateAsync(customer);
            return customer.CustomerId;
        }

        public async Task<bool> UpdateAsync(string id, CustomerDto updateDto)
        {
            var existing = await _customerRepository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.LastName = updateDto.LastName;
            existing.FirstName = updateDto.FirstName;
            existing.Phone = updateDto.Phone;
            existing.Email = updateDto.Email;
            existing.Address = updateDto.Address;

            await _customerRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var existing = await _customerRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _customerRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<CustomerDto>> SearchByNameAsync(string name)
        {
            return await _customerRepository.SearchByNameAsync(name);
        }

        public async Task<IEnumerable<CustomerDto>> SearchByPhoneAsync(string phone)
        {
            return await _customerRepository.SearchByPhoneAsync(phone);
        }
    }
}
