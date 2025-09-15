using NoticiasProvaIci.Models;

namespace NoticiasProvaIci.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task CreateAsync(User user);
    Task UpdateAsync(Guid id, User user);
    Task DeleteAsync(Guid id);
}