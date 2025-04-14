using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Data;
using UserService.DTOs;
using UserService.Models;
using UserService.Helpers;

namespace UserService.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        public AuthService(AppDbContext db, IConfiguration config, ITokenService tokenService)
        {
            _db = db;
            _config = config;
            _tokenService = tokenService;

        }

        public async Task<AuthResult> LoginUserAsync(LoginDTO dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if(user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return new AuthResult { Success = false, Message = "Invalid Credentials." };
            }

            var token = _tokenService.GeneratToken(user);

            return new AuthResult { Success=true, Message ="Login Successful.", Token = token };
        }

        public async Task<AuthResult> RegisterUserAsync(RegisterDTO dto)
        {
            if(await _db.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return new AuthResult { Success = false, Message = "Email already exist." };
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return new AuthResult { Success = true, Message = "User Registered Successfully." };
        }
    }
}
