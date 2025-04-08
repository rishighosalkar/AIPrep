using UserService.DTOs;

namespace UserService.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterUserAsync(RegisterDTO registerDTO);
        Task<AuthResult> LoginUserAsync(LoginDTO loginDTO);
    }
}
