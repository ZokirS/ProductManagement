using ProductManagement.DAL.Helpers;
using ProductManagement.DAL.Models;

namespace ProductManagement.DAL.Repositories.Abstract;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(long id);
    Task<User?> GetUserByLoginAsync(string login);
    Task<User> AddUserAsync(User user);
    Task<User?> UpdateUserAsync(User user);
    Task<User?> DeleteUserAsync(long id);
    Task<List<User>> GetUsersAsync();
    Task<List<User>> GetUsersByRoleAsync(UserRole role);
}
