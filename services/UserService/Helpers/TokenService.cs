using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.DTOs;
using UserService.Models;

namespace UserService.Helpers
{
    public class TokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GeneratToken(User user)
        {
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email!=null?user.Email:string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString() !=null?user.Id.ToString():Guid.Empty.ToString()),
                    new Claim(JwtRegisteredClaimNames.NameId, user.FullName!=null?user.FullName:string.Empty),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString() !=null?user.Id.ToString():Guid.Empty.ToString())
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.IssuerSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtSettings.ValidIssuer,
                _jwtSettings.ValidAudience,
                claims,
                null,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
