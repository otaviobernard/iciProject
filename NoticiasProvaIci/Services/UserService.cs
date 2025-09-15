using Microsoft.EntityFrameworkCore;
using NoticiasProvaIci.Data;
using NoticiasProvaIci.Models;
using NoticiasProvaIci.Services.Interfaces;

namespace NoticiasProvaIci.Services;

public class UserService(AppDbContext context, PasswordService passwordService) : IUserService
{
    public async Task<List<User>> GetAllAsync() =>
        await context.Users.ToListAsync();

    public async Task<User?> GetByIdAsync(Guid id) =>
        await context.Users.FindAsync(id);

    public async Task CreateAsync(User user)
    {
        user.Password = passwordService.HashPassword(user.Password);
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, User user)
    {
        var userDb = await context.Users.FindAsync(id);
        if (userDb == null) return;
        userDb.Name = user.Name;
        userDb.Email = user.Email;
        userDb.Password = passwordService.HashPassword(user.Password);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await context.Users.FindAsync(id);
        if (user != null)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
    }
}