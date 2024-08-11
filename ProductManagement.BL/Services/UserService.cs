using ProductManagement.BL.Models.User;
using ProductManagement.BL.Services.Abstract;
using ProductManagement.DAL.Models;
using ProductManagement.DAL.Repositories.Abstract;
using ProductManagement.Common.Exceptions.CustomExceptions;
using ProductManagement.Common.Helpers;
using ProductManagement.DAL.Helpers.Extensions;

namespace ProductManagement.BL.Services;

public class UserService(IUserRepository userRepository, ITokenService tokenService) : IUserService
{

    public async Task<UserSession> AuthenticateAsync(AuthenticateRequest request)
    {
        var user = await userRepository.GetUserByLoginAsync(request.Username);
        if (user is null)
            throw new UserNotFoundException(request.Username);

        if (!Cryptography.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            throw new UserPasswordIncorrectException(request.Username);

        var token = tokenService.GenerateToken(request.Username, user.Role);
        return new UserSession
        {
            Token = token,
            User = user
        };
    }

    public async Task<User> GetUserByLoginAsync(UserLoginRequest request)
    {
        var user = await userRepository.GetUserByLoginAsync(request.Username);
        if (user is null)
            throw new UserNotFoundException(request.Username);

        return user;
    }

    public Task<User> RegisterUserAsync(UserAddRequest request)
    {
        var user = new User();
        user.CopyPropertiesFrom(request);
        Cryptography.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        return userRepository.AddUserAsync(user);
    }

    public async Task<User> UpdateUserAsync(UserUpdateRequest request)
    {
        var user = new User();
        user.CopyPropertiesFrom(request);
        Cryptography.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        var updatedUser = await userRepository.UpdateUserAsync(user);
        if (updatedUser is null)
            throw new UserNotFoundException(user.Login);

        return updatedUser;
    }

    public async Task<User> DeleteUserAsync(UserIdRequest request)
    {
        var user = await userRepository.DeleteUserAsync(request.Id);
        if (user is null)
            throw new UserNotFoundException(request.Id);

        return user;
    }

    public Task<List<User>> GetUsersAsync() =>
        userRepository.GetUsersAsync();
}

