using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAdminRepository adminRepository, IConfiguration configuration)
        {
            _adminRepository = adminRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto?> AuthenticateAsync(LoginDto loginDto)
        {
            var admin = await _adminRepository.GetByUsernameAsync(loginDto.Username);

            if (admin == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, admin.PasswordHash))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var keyString = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(keyString))
            {
                throw new InvalidOperationException("JWT Secret Key is not configured properly in appsettings.");
            }
            var key = Encoding.ASCII.GetBytes(keyString);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.Username),
                new Claim(ClaimTypes.Role, admin.Role),
                new Claim("FullName", admin.FullName ?? string.Empty)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                Username = admin.Username,
                FullName = admin.FullName,
                Role = admin.Role
            };
        }
    }
}
