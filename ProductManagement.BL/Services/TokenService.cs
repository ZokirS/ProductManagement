using ProductManagement.BL.Services.Abstract;
using ProductManagement.DAL.Helpers;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ProductManagement.BL.Services
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        private readonly string _jwtSecret = configuration["Jwt:Secret"] ?? string.Empty;
        private readonly string _jwtIssuer = configuration["Jwt:Issuer"] ?? string.Empty;
        private readonly string _jwtAudience = configuration["Jwt:Audience"] ?? string.Empty;

        public string GenerateToken(string login, UserRole role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Sid, login),
                new Claim(ClaimTypes.Role, role.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtIssuer,
                Audience = _jwtAudience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
