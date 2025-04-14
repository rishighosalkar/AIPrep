using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.DTOs;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<UserProfileDTO> GetUserProfileAsync(Guid userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return null;

            return new UserProfileDTO { Id = user.Id, Email = user.Email, FullName = user.FullName };
        }

        public async Task<bool> UpdateUserProfileAsync(Guid userId, UpdateUserDTO updateUserDTO)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return false;

            user.FullName = updateUserDTO.FullName;
            user.Email = updateUserDTO.Email;
            if(!string.IsNullOrEmpty(updateUserDTO.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateUserDTO.Password);
            }
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
