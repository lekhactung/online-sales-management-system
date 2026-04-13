using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AdminRepository : Repository<AdminAccount>, IAdminRepository
    {
        public AdminRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<AdminAccount?> GetByUsernameAsync(string username)
        {
            return await _context.Set<AdminAccount>().FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<AdminAccount?> GetByIdAsync(int id)
        {
            return await _context.Set<AdminAccount>().FindAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var admin = await GetByIdAsync(id);
                if (admin == null) return false;
                _context.Set<AdminAccount>().Remove(admin);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
