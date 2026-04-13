using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid payload.");
            }

            var result = await _authService.AuthenticateAsync(loginDto);
            if (result == null)
            {
                return Unauthorized(new { Message = "Đăng nhập thất bại. Kiểm tra lại username và password!" });
            }

            return Ok(result);
        }
    }
}
