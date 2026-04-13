using Model.DTOs;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> AuthenticateAsync(LoginDto loginDto);
    }
}
