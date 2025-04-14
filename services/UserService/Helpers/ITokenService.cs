using UserService.Models;

namespace UserService.Helpers
{
    public interface ITokenService
    {
        public string GeneratToken(User user);
    }
}
