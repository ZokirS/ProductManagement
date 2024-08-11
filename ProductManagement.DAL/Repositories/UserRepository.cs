using Microsoft.EntityFrameworkCore;
using ProductManagement.DAL.Helpers;
using ProductManagement.DAL.Helpers.Extensions;
using ProductManagement.DAL.Models;
using ProductManagement.DAL.Repositories.Abstract;
using System;

namespace ProductManagement.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext context;

    public UserRepository(AppDbContext context)
    {
        this.context = context;
    }
    public Task<User?> GetUserByIdAsync(long id) =>
        context.Users.FirstOrDefaultAsync(u => u.Id == id);

    public Task<User?> GetUserByLoginAsync(string login) =>
        context.Users.FirstOrDefaultAsync(u => u.Login == login);

    public async Task<User> AddUserAsync(User user)
    {
        user.CreatedAt = DateTime.UtcNow;
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateUserAsync(User user)
    {
        var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        if (existingUser is null) return null;

        existingUser.CopyProperties(user);
        existingUser.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
        return existingUser;
    }

    public async Task<User?> DeleteUserAsync(long id)
    {
        var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (existingUser is null) return null;

        context.Users.Remove(existingUser);
        await context.SaveChangesAsync();
        return existingUser;
    }

    public Task<List<User>> GetUsersAsync() =>
        context.Users.ToListAsync();

    public Task<List<User>> GetUsersByRoleAsync(UserRole role) =>
        context.Users.Where(u => u.Role == role).ToListAsync();

}

