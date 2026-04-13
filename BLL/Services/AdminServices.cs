using DAL.Repositories;
using Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IAdminServices
    {
        Task<IEnumerable<AdminAccount>> GetAllAsync();
        Task<AdminAccount?> GetByIdAsync(int id);
        Task<AdminAccount> CreateAsync(AdminAccount admin);
        Task<bool> UpdateAsync(int id, AdminAccount admin);
        Task<bool> DeleteAsync(int id);
    }

    public class AdminServices : IAdminServices
    {
        private readonly IAdminRepository _adminRepository;

        public AdminServices(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<IEnumerable<AdminAccount>> GetAllAsync()
        {
            return await _adminRepository.GetAllAsync();
        }

        public async Task<AdminAccount?> GetByIdAsync(int id)
        {
            return await _adminRepository.GetByIdAsync(id);
        }

        public async Task<AdminAccount> CreateAsync(AdminAccount admin)
        {
            return await _adminRepository.CreateAsync(admin);
        }

        public async Task<bool> UpdateAsync(int id, AdminAccount admin)
        {
            var updated = await _adminRepository.UpdateAsync(admin);
            return updated != null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _adminRepository.DeleteAsync(id);
        }
    }
}
