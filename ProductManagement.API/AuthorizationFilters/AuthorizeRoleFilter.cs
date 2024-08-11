using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.DAL.Repositories.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductManagement.API.AuthorizationFilters
{
    public class AuthorizeRoleFilter(IConfiguration configuration, IUserRepository userRepository, params string[] roles) : IAuthorizationFilter
    {
        private readonly string _jwtSecret = configuration["Jwt:Secret"] ?? string.Empty;
        private readonly string _jwtIssuer = configuration["Jwt:Issuer"] ?? string.Empty;
        private readonly string _jwtAudience = configuration["Jwt:Audience"] ?? string.Empty;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _jwtIssuer,
                    ValidAudience = _jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                if (validatedToken.ValidTo < DateTime.UtcNow)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var role = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role is null || !roles.Contains(role))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var login = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
                if (login is null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var user = userRepository.GetUserByLoginAsync(login).Result;
                if (user is null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                context.HttpContext.Items["User"] = user;
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
