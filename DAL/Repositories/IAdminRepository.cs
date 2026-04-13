using Model.Entities;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IAdminRepository : IRepository<AdminAccount>
    {
        Task<AdminAccount?> GetByUsernameAsync(string username);
        Task<AdminAccount?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
