using ProductManagement.BL.Models.User;
using ProductManagement.DAL.Models;

namespace ProductManagement.BL.Services.Abstract;

public interface IUserService
{
    Task<UserSession> AuthenticateAsync(AuthenticateRequest request);
    Task<User> GetUserByLoginAsync(UserLoginRequest request);
    Task<User> RegisterUserAsync(UserAddRequest request);
    Task<User> UpdateUserAsync(UserUpdateRequest request);
    Task<User> DeleteUserAsync(UserIdRequest request);
    Task<List<User>> GetUsersAsync();
}

