using ProductManagement.DAL.Helpers;

namespace ProductManagement.BL.Services.Abstract;

public interface ITokenService
{
    string GenerateToken(string login, UserRole role);
}

