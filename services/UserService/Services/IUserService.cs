using UserService.DTOs;

namespace UserService.Services
{
    public interface IUserService
    {
        Task<UserProfileDTO> GetUserProfileAsync(Guid userId);
        Task<bool> UpdateUserProfileAsync(Guid userId, UpdateUserDTO updateUserDTO);
    }
}
